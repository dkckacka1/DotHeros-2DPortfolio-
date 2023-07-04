using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/*
 * ���� �Ŵ���
 */

namespace Portfolio.Battle
{
    public class BattleManager : MonoBehaviour
    {
        private static BattleUIManager battleUI;        // ���� UI
        private static BattleFactory battleFactory;     // �������� ������
        private static TurnBaseSystem turnBaseSystem;   // ���� �ý���
        private static ActionSystem actionSystem;       // �׼� �ý���
        private static ManaSystem manaSystem;           // ���� �ý���
        private static ObjectPool objectPool;           // ������Ʈ Ǯ

        private List<BattleUnit> unitList = new List<BattleUnit>();             // ���� ����
        private Dictionary<BattleState, UnityEvent> StateEventHandlerDic = new Dictionary<BattleState, UnityEvent>(); // ���� ���� �̺�Ʈ Dic

        [SerializeField] private BattleState currentBattleState = BattleState.NONE; // ���� ���� ����
        public Queue<Stage> stageDatas = new Queue<Stage>(); // �������� ���� ť
        public Stage currentStage; // ���� ��������
        public Dictionary<int, int> GetItemDic = new Dictionary<int, int>(); // ������ ���� ������ Dic
        public float stageOutputTime = 2f;  // �������� ��� ���ð�

        //===========================================================
        // SceneLoaderData
        //===========================================================
        public List<Unit> userChoiceUnits;   // ������ ������ ����
        private Map currentMap;  // ������ ������ ��

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
        public BattleState BattleState { get => currentBattleState; }
        public Map CurrentMap
        {
            get => currentMap;
            set => currentMap = value;
        }
        public List<BattleUnit> UnitList => unitList;

        private void Awake()
        {
            // �̱��� ����
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
                // �׽�Ʈ ���°� �ƴϸ�
            {
                // �� ����
                SetMap();
                // ���� ���� ����
                SetUserUnit();
                // ���� UI ���� �� ������ ���ε�
                battleUI.Initialize(CurrentMap);
                // �������� ���� ����
                StartCoroutine(SetStartStage());
            }
            else
                // �׽�Ʈ ���̸�
            {
                // MapID ������ �� ����
                GameManager.Instance.TryGetMap(CallMapID, out currentMap);
                for (int i = 0; i < currentMap.StageList.Count; i++)
                {
                    stageDatas.Enqueue(currentMap.StageList[i]);
                }

                // ���� ������ ������ ������ ���� �� userUnitTakeCount ������ŭ �����ͼ� ����
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
            // ������ ������ �������� �����ͼ� ����
            this.CurrentMap = SceneLoader.userChocieMap;
            for (int i = 0; i < CurrentMap.StageList.Count; i++)
            {
                // �������� ť�� ���������� ���� ���������� �־��ֱ�
                stageDatas.Enqueue(CurrentMap.StageList[i]);
            }
        }

        private void SetUserUnit()
            // ������ ������ ���� ����Ʈ�� �����ͼ� ���� ���� ���� ���ֱ�
        {
            userChoiceUnits = SceneLoader.userChoiceUnits;
            battleFactory.CreateUserUnit(userChoiceUnits);
        }

        //===========================================================
        // UnitList
        //===========================================================

        // ���ָ���Ʈ�� ���� �ֱ�
        public void AddUnitinUnitList(BattleUnit unit) => unitList.Add(unit);
        // ���� ����Ʈ���� ���� ����
        public void RemoveUnit(BattleUnit unit)
        {
            unitList.Remove(unit);
        }
        // ���� ����Ʈ �ʱ�ȭ
        public void ClearUnitinUnitList()
        {
            unitList.Clear();
        }
        // Func�� �´� ���� ����Ʈ ����
        public IEnumerable<BattleUnit> GetUnitList(Func<BattleUnit, bool> predicate)
        {
            return unitList.Where(predicate);
        }

        // �÷��̾�� ���� Ȥ�� �� ���� ����Ʈ
        private IEnumerable<BattleUnit> GetUnitList(bool isEnemy) => unitList.Where(battleUnit => (battleUnit.IsEnemy == isEnemy) && !battleUnit.IsDead);
        public void CheckUnitList()
        {
            if (GetUnitList(true).Count() == 0)
            // ����ִ� �� ������ 0���� ���
            {
                //�¸�
                Win();
            }

            if (GetUnitList(false).Count() == 0)
            // ����ִ� �÷��̾� ������ 0���� ���
            {
                // �й�
                Defeat();
            }
        }
        public void GetItem(int id, int count)
            // ������ ȹ��
        {
            if (GetItemDic.ContainsKey(id))
                // �̹� ������ ���濡 �ִ� �������̸�
            {
                // ȹ�� ���ڸ� ����
                GetItemDic[id] += count;
            }
            else
                // ������ ���濡 ���ٸ�
            {
                // ������ �߰�
                GetItemDic.Add(id, count);
            }
        }
        private void ClearDeadUnit()
            // ���� ���� ����
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
            // ���� ���� ����
        {
            currentBattleState = state;
            // ���� ���� ���濡 ���� �̺�Ʈ�� ȣ�����ش�.
            InvokeStateEvent(state);
        }

        public void Play()
            // ���� ��
        {
            SwitchBattleState(BattleState.PLAY);
        }

        public IEnumerator SetStartStage()
            // ù��° �������� ����
        {
            SwitchBattleState(BattleState.SETSTAGE);
            BattleUIManager.ShowStageUI(CurrentMap);
            currentStage = stageDatas.Dequeue();
            BattleFactory.CreateStage(currentStage);
            battleUI.SetStartStageDirect();
            yield return new WaitForSecondsRealtime(stageOutputTime);
            // ���� ���� ����
            battleUI.SetBattleStartDirect();
            BattleStart();
        }

        public void SetNextStage()
            // ���� �������� ����
        {
            SwitchBattleState(BattleState.SETSTAGE);
            StartCoroutine(SetStageSequence());
        }
        private IEnumerator SetStageSequence()
            // �������� ��� ����
        {
            yield return new WaitForSeconds(stageOutputTime);
            // ���� ���� ����
            ClearDeadUnit();
            // �������� ���� ���ε�
            BattleUIManager.ShowStageUI(CurrentMap);
            // ���� �������� ���� ��������
            currentStage = stageDatas.Dequeue();
            // ���� �������� ���� ���� ���� ����
            BattleFactory.CreateStage(currentStage);
            // ��� �� �ʱ�ȭ ���ֱ�
            turnBaseSystem.ResetAllUnitTurn();
            yield return new WaitForSeconds(stageOutputTime);
            BattleStart();
        }
        public void BattleStart()
            // ���� ����
        {
            SwitchBattleState(BattleState.BATTLESTART);
            Play();
        }

        public void Pause()
            // ���� ����
        {
            SwitchBattleState(BattleState.PAUSE);
        }

        public void Win()
            // �¸�
        {
            SwitchBattleState(BattleState.WIN);
            if (stageDatas.Count() >= 1)
                // ���� ���������� �����ִٸ�
            {
                // ���� �������� ���
                SetNextStage();
            }
            else
                // ���� ���������� ���ٸ�
            {
                // ���� �¸� UI ���
                BattleUIManager.Win();
                // ���� ������ ���� ������ �־��ֱ�
                UesrGetItem();
                // ���ֵ� ����ġ ���������ֱ�
                UnitGetExperience();
                // ���� �� ���� �Ѱ��༭ �� Ŭ���� ���ֱ�
                GameManager.CurrentUser.ClearMap(currentMap.MapID);
            }
        }

        public void Defeat()
            // �й�
        {
            SwitchBattleState(BattleState.DEFEAT);
            // �й� UI ���
            BattleUIManager.Defeat();
        }

        private void UesrGetItem()
            // ���� �����۵� ���� ���濡 �־��ֱ�
        {
            foreach (var itemKV in GetItemDic.ToList())
            {
                GameManager.CurrentUser.AddConsumableItem(itemKV.Key, itemKV.Value);
            }
        }

        private void UnitGetExperience()
            // ���ֵ� ����ġ ȹ������ֱ�
        {
            var experienceValue = currentMap.MapExperience;
            foreach (var unit in userChoiceUnits)
            {
                unit.CurrentExperience += experienceValue;
            }
        }



        //===========================================================
        // StateEvent
        //===========================================================
        public void PublishEvent(BattleState state, UnityAction action)
            // ���� ���¿� �̺�Ʈ ����
        {
            if (StateEventHandlerDic.ContainsKey(state))
                // �̺�Ʈ Dic�� �ش� ���� ���� KEY�� �ִٸ�
            {
                //�̺�Ʈ ����
                StateEventHandlerDic[state].AddListener(action);
            }
            else
            {
                //KV ������ �̺�Ʈ ����
                StateEventHandlerDic.Add(state, new UnityEvent());
                StateEventHandlerDic[state].AddListener(action);
            }
        }

        public void UnPublishEvent(BattleState state, UnityAction action)
            // �̺�Ʈ ���� ����
        {
            if (StateEventHandlerDic.ContainsKey(state))
            {
                StateEventHandlerDic[state].RemoveListener(action);
            }
        }

        public void InvokeStateEvent(BattleState state)
            // ������ �̺�Ʈ ��� ȣ��
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
            // �ڵ� ���� ����
        {
            // ��� �÷��̾�� ���ֿ� ���� �ڵ� ���� üũ
            var list = unitList.Where(battleUnit => !battleUnit.IsEnemy);
            foreach (var unit in list)
            {
                unit.CheckAutoBattle();
            }
        }
    }

}