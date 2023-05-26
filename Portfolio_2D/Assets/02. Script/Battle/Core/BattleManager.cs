using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public class BattleManager : MonoBehaviour
    {
        private static BattleUIManager battleUI;
        private static BattleFactory battleFactory;
        private static TurnBaseSystem turnBaseSystem;
        private static ActionSystem actionSystem;
        private static ManaSystem manaSystem;
         
        private List<UnitTurnBase> unitList;

        private BattleState battleState = BattleState.SETTING;



        private Dictionary<BattleState, UnityEvent> StateEventHandlerDic = new Dictionary<BattleState, UnityEvent>();

        public static BattleManager Instance { get; private set; }

        public BattleState BattleState { get => battleState; }
        public static BattleUIManager BattleUIManager { get => battleUI; }
        public static BattleFactory BattleFactory { get => battleFactory; }
        public static TurnBaseSystem TurnBaseSystem { get => turnBaseSystem;}
        public static ActionSystem ActionSystem { get => actionSystem;}
        public static ManaSystem ManaSystem { get => manaSystem; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("BattleManager is already created");
                return;
            }

            Instance = this;

            battleUI = GetComponentInChildren<BattleUIManager>();
            battleFactory = GetComponentInChildren<BattleFactory>();
            turnBaseSystem = GetComponentInChildren<TurnBaseSystem>();
            actionSystem = GetComponentInChildren<ActionSystem>();
            manaSystem = GetComponentInChildren<ManaSystem>();

            unitList = new List<UnitTurnBase>();
        }

        private void Start()
        {
            battleState = BattleState.PLAY;
        }

        public void AddUnitinUnitList(UnitTurnBase unit) => unitList.Add(unit);
        public void RemoveUnitinUnitList(BattleUnit unit)
        {
            unitList.Remove(unitList.Find((findunit) => findunit.unit == unit));
        }
        public void ClearUnitinUnitList()
        {
            unitList.Clear();
        }
        public UnitTurnBase FindUnitinUnitList(BattleUnit unit) => unitList.Find((findunit) => findunit.unit == unit);
        public List<UnitTurnBase> GetUnitList() => unitList;

        public void UnitListCycleMethod(UnityAction<BattleUnit> action)
        {
            foreach (var unitBase in unitList)
            {
                action?.Invoke(unitBase.unit);
            }
        }

        public void SwitchBattleState(BattleState state)
        {
            battleState = state;
            InvokeStateEvent(state);
        }

        public void PublishEvent(BattleState state, UnityAction action)
        {
            if (StateEventHandlerDic.ContainsKey(state))
            {
                StateEventHandlerDic[state].AddListener(action);
            }
            else
            {
                StateEventHandlerDic.Add(state, new UnityEvent());
                StateEventHandlerDic[state].AddListener(action);
            }
        }

        public void UnPublishEvent(BattleState state, UnityAction action)
        {
            if (StateEventHandlerDic.ContainsKey(state))
            {
                StateEventHandlerDic[state].RemoveListener(action);
            }
        }

        public void InvokeStateEvent(BattleState state)
        {
            if (StateEventHandlerDic.ContainsKey(state))
            {
                StateEventHandlerDic[state]?.Invoke();
            }
        }
    }

}