using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private List<BattleUnit> unitList = new List<BattleUnit>();

        private BattleState battleState = BattleState.NONE;



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
        }

        private void Start()
        {
            battleState = BattleState.PLAY;
        }

        //===========================================================
        // UnitList
        //===========================================================

        public void AddUnitinUnitList(BattleUnit unit) => unitList.Add(unit);
        public void RemoveUnit(BattleUnit unit)
        {
            unitList.Remove(unit);
        }
        public void ClearUnitinUnitList()
        {
            unitList.Clear();
        }
        public List<BattleUnit> GetUnitList() => unitList;

        public IEnumerable<BattleUnit> GetUnitList(Func<BattleUnit, bool> predicate)
        {
            return unitList.Where(predicate);
        }

        public int GetUnitListCount(IEnumerable<BattleUnit> list)
        {
            return list.Count();
        }

        private IEnumerable<BattleUnit> GetUnitList(bool isEnemy)
        {
            return unitList.Where(battleUnit => (battleUnit.IsEnemy == isEnemy) && !battleUnit.IsDead);
        }

        public void CheckUnitList()
        {
            if (GetUnitList(false).Count() == 0)
                // 살아있는 플레이어 유닛이 0명일 경우
            {
                Win();
            }
            
            if (GetUnitList(true).Count() == 0)
                // 살아있는 적 유닛이 0명일 경우
            {
                Defeat();
            }
        }

        //===========================================================
        // SetState
        //===========================================================
        public void SwitchBattleState(BattleState state)
        {
            battleState = state;
            InvokeStateEvent(state);
        }

        public void Play()
        {
            SwitchBattleState(BattleState.PLAY);
        }

        public void SetStage(int stageID)
        {
            SwitchBattleState(BattleState.SETSTAGE);
        }

        public void BattleStart()
        {
            SwitchBattleState(BattleState.BATTLESTART);
            foreach (var unit in unitList.Where(unit => !unit.IsDead))
            {
                unit.BattleStart();
            }
        }

        public void Pause()
        {
            SwitchBattleState(BattleState.PAUSE);
        }

        public void Win()
        {
            SwitchBattleState(BattleState.WIN);
        }

        public void Defeat()
        {
            SwitchBattleState(BattleState.DEFEAT);
        }

        //===========================================================
        // StateEvent
        //===========================================================
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

        public void SetAutomaticBattle()
        {
            var list = this.unitList.Where(battleUnit => !battleUnit.IsEnemy);
            foreach (var unit in list)
            {
                unit.CheckAutoBattle();
            }
        }
    }

}