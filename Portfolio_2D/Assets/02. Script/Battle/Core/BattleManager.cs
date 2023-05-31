using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio.Battle
{
    public class BattleManager : MonoBehaviour
    {
        private static BattleUIManager battleUI;
        private static BattleFactory battleFactory;
        private static TurnBaseSystem turnBaseSystem;
        private static ActionSystem actionSystem;
        private static ManaSystem manaSystem;

        private List<BattleUnit> unitList = new List<BattleUnit>();
        private Dictionary<BattleState, UnityEvent> StateEventHandlerDic = new Dictionary<BattleState, UnityEvent>();

        [SerializeField] private BattleState battleState = BattleState.NONE;
        public Queue<StageData> stageDatas = new Queue<StageData>();
        public StageData currentStageData;
        public bool isTest = false;

        //===========================================================
        // SceneLoaderData
        //===========================================================
        public List<Unit> userChoiceUnits;   // 유저가 설정한 유닛
        public MapData currentMapData;  // 유적가 선택한 맵

        //===========================================================
        // Property & Singleton
        //===========================================================
        public static BattleManager Instance { get; private set; }
        public static BattleUIManager BattleUIManager { get => battleUI; }
        public static BattleFactory BattleFactory { get => battleFactory; }
        public static TurnBaseSystem TurnBaseSystem { get => turnBaseSystem; }
        public static ActionSystem ActionSystem { get => actionSystem; }
        public static ManaSystem ManaSystem { get => manaSystem; }
        public BattleState BattleState { get => battleState; }

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
            SetMapData();
            SetUserUnit();
            currentStageData = stageDatas.Dequeue();

            battleUI.Initialize(currentMapData);
            SetStage();
        }

        //===========================================================
        // BattleInit
        //===========================================================
        private void SetMapData()
        {
            currentMapData = SceneLoader.userChocieMapData;
            if (currentMapData.stage_1_ID != -1)
            {
                GameManager.Instance.TryGetData(currentMapData.stage_1_ID, out StageData stageData);
                stageDatas.Enqueue(stageData);
            }
            if (currentMapData.stage_2_ID != -1)
            {
                GameManager.Instance.TryGetData(currentMapData.stage_2_ID, out StageData stageData);
                stageDatas.Enqueue(stageData);
            }
            if (currentMapData.stage_3_ID != -1)
            {
                GameManager.Instance.TryGetData(currentMapData.stage_3_ID, out StageData stageData);
                stageDatas.Enqueue(stageData);
            }
            if (currentMapData.stage_4_ID != -1)
            {
                GameManager.Instance.TryGetData(currentMapData.stage_4_ID, out StageData stageData);
                stageDatas.Enqueue(stageData);
            }
            if (currentMapData.stage_5_ID != -1)
            {
                GameManager.Instance.TryGetData(currentMapData.stage_5_ID, out StageData stageData);
                stageDatas.Enqueue(stageData);
            }
        }

        private void SetUserUnit()
        {
            userChoiceUnits = SceneLoader.userChoiceUnits;
            foreach (Unit userUnit in userChoiceUnits)
            {
                if (battleFactory.TryCreateBattleUnit(userUnit, false, out BattleUnit battleUnit))
                {
                    AddUnitinUnitList(battleUnit);
                }
            }
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
            if (GetUnitList(true).Count() == 0)
            // 살아있는 적 유닛이 0명일 경우
            {
                Win();
            }

            if (GetUnitList(false).Count() == 0)
            // 살아있는 플레이어 유닛이 0명일 경우
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

        public void SetStage()
        {
            SwitchBattleState(BattleState.SETSTAGE);
            Debug.Log(currentStageData.ID);
            BattleFactory.CreateStage(currentStageData);
            BattleStart();
        }

        public void BattleStart()
        {
            SwitchBattleState(BattleState.BATTLESTART);
            Play();
        }

        public void Pause()
        {
            SwitchBattleState(BattleState.PAUSE);
        }

        public void Win()
        {
            SwitchBattleState(BattleState.WIN);
            if (this.stageDatas.Count() >= 1)
            {
                BattleUIManager.ShowNextStageUI();
                currentStageData = stageDatas.Dequeue();
                SetStage();
            }
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