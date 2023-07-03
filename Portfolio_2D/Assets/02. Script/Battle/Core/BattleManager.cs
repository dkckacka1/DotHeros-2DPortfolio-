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
        private static ObjectPool objectPool;

        private List<BattleUnit> unitList = new List<BattleUnit>();
        private Dictionary<BattleState, UnityEvent> StateEventHandlerDic = new Dictionary<BattleState, UnityEvent>();

        [SerializeField] private BattleState battleState = BattleState.NONE;
        public Queue<Stage> stageDatas = new Queue<Stage>();
        public Stage currentStage;
        public Dictionary<int, int> GetItemDic = new Dictionary<int, int>();
        public float stageOutputTime = 2f;

        //===========================================================
        // SceneLoaderData
        //===========================================================
        public List<Unit> userChoiceUnits;   // 유저가 설정한 유닛
        private Map currentMap;  // 유적가 선택한 맵

        //===========================================================
        // TestValue
        //===========================================================
        [Header("TestValue")]
        public bool isTest = false;
        public int CallMapID = 500;
        public int userUnitTakeCount = 5;
            
        //===========================================================
        // Property & Singleton
        //===========================================================
        public static BattleManager Instance { get; private set; }
        public static BattleUIManager BattleUIManager { get => battleUI; }
        public static BattleFactory BattleFactory { get => battleFactory; }
        public static TurnBaseSystem TurnBaseSystem { get => turnBaseSystem; }
        public static ActionSystem ActionSystem { get => actionSystem; }
        public static ManaSystem ManaSystem { get => manaSystem; }
        public static ObjectPool ObjectPool { get => objectPool; }
        public BattleState BattleState { get => battleState; }
        public Map CurrentMap
        {
            get => currentMap;
            set => currentMap = value;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("BattleManager is already created");
                Destroy(this.gameObject);
                return;
            }

            Instance = this;

            battleUI = GetComponentInChildren<BattleUIManager>();
            battleFactory = GetComponentInChildren<BattleFactory>();
            turnBaseSystem = GetComponentInChildren<TurnBaseSystem>();
            actionSystem = GetComponentInChildren<ActionSystem>();
            manaSystem = GetComponentInChildren<ManaSystem>();
            objectPool = GetComponentInChildren<ObjectPool>();
        }

        private void Start()
        {
            if (!GameManager.Instance.isTest)
            {
                SetMap();
                SetUserUnit();
                battleUI.Initialize(CurrentMap);
                StartCoroutine(SetStartStage());
            }
            else
            {
                GameManager.Instance.TryGetMap(CallMapID, out currentMap);
                for (int i = 0; i < currentMap.StageList.Count; i++)
                {
                    stageDatas.Enqueue(currentMap.StageList[i]);
                }

                userChoiceUnits = GameManager.CurrentUser.userUnitList.OrderByDescending(GameLib.UnitBattlePowerSort).Take(userUnitTakeCount).ToList();
                battleFactory.CreateUserUnit(userChoiceUnits);

                battleUI.Initialize(currentMap);
                StartCoroutine(SetStartStage());
            }
        }

        //===========================================================
        // BattleInit
        //===========================================================

        private void SetMap()
        {
            this.CurrentMap = SceneLoader.userChocieMap;
            for (int i = 0; i < CurrentMap.StageList.Count; i++)
            {
                stageDatas.Enqueue(CurrentMap.StageList[i]);
            }
        }

        private void SetUserUnit()
        {
            userChoiceUnits = SceneLoader.userChoiceUnits;
            battleFactory.CreateUserUnit(userChoiceUnits);
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
        public void GetItem(int id, int count)
        {
            if (GetItemDic.ContainsKey(id))
            {
                GetItemDic[id] += count;
            }
            else
            {
                GetItemDic.Add(id, count);
            }
        }
        private void ClearDeadUnit()
        {
            var deadUnitList = unitList.Where(unit => unit.IsDead).ToList();
            foreach(var unit in deadUnitList)
            {
                RemoveUnit(unit);
                Destroy(unit.gameObject);
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

        public IEnumerator SetStartStage()
        {
            SwitchBattleState(BattleState.SETSTAGE);
            BattleUIManager.ShowStageUI(CurrentMap);
            currentStage = stageDatas.Dequeue();
            BattleFactory.CreateStage(currentStage);
            battleUI.SetStartStageDirect();
            yield return new WaitForSecondsRealtime(stageOutputTime);
            battleUI.SetBattleStartDirect();
            BattleStart();
        }

        public void SetNextStage()
        {
            SwitchBattleState(BattleState.SETSTAGE);
            StartCoroutine(SetStageProcedure());
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
            if (stageDatas.Count() >= 1)
            {
                SetNextStage();
            }
            else
            {
                BattleUIManager.Win();
                UesrGetItem();
                UnitGetExperience();
                GameManager.CurrentUser.ClearMap(currentMap.MapID);
            }
        }

        public void Defeat()
        {
            SwitchBattleState(BattleState.DEFEAT);
            BattleUIManager.Defeat();
        }

        private void UesrGetItem()
        {
            foreach (var itemKV in GetItemDic.ToList())
            {
                GameManager.CurrentUser.AddConsumableItem(itemKV.Key, itemKV.Value);
            }
        }

        private void UnitGetExperience()
        {
            var experienceValue = currentMap.MapExperience;
            foreach (var unit in userChoiceUnits)
            {
                unit.CurrentExperience += experienceValue;
            }
        }

        private IEnumerator SetStageProcedure()
        {
            yield return new WaitForSeconds(stageOutputTime);
            ClearDeadUnit();
            BattleUIManager.ShowStageUI(CurrentMap);
            currentStage = stageDatas.Dequeue();
            BattleFactory.CreateStage(currentStage);
            turnBaseSystem.ResetAllUnitTurn();
            yield return new WaitForSeconds(stageOutputTime);
            BattleStart();
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

        //===========================================================
        // BtnPlugin
        //===========================================================
        public void SetAutomaticBattle()
        {
            var list = unitList.Where(battleUnit => !battleUnit.IsEnemy);
            foreach (var unit in list)
            {
                unit.CheckAutoBattle();
            }
        }
    }

}