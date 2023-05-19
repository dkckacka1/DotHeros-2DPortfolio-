using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public class BattleManager : MonoBehaviour
    {

        [SerializeField] private BattleUI battleUI;

        [SerializeField] private BattleFactory battleFactory;
         
        private List<UnitTurnBase> unitList;

        private BattleState battleState = BattleState.SETTING;



        private Dictionary<BattleState, UnityEvent> StateEventHandlerDic = new Dictionary<BattleState, UnityEvent>();

        public static BattleManager Instance { get; private set; }
        public BattleUI BattleUI { get => battleUI; }
        public BattleFactory BattleFactory { get => battleFactory; }

        public BattleState BattleState { get => battleState; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("BattleManager is already created");
                return;
            }

            Instance = this;
            unitList = new List<UnitTurnBase>();
        }

        private void Start()
        {
            battleState = BattleState.PLAY;
        }

        public void AddUnitinUnitList(UnitTurnBase unit) => unitList.Add(unit);
        public void RemoveUnitinUnitList(Unit unit)
        {
            unitList.Remove(unitList.Find((findunit) => findunit.unit == unit));
        }
        public void ClearUnitinUnitList()
        {
            unitList.Clear();
        }
        public UnitTurnBase FindUnitinUnitList(Unit unit) => unitList.Find((findunit) => findunit.unit == unit);
        public List<UnitTurnBase> GetUnitList() => unitList;

        public void UnitListCycleMethod(UnityAction<Unit> action)
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