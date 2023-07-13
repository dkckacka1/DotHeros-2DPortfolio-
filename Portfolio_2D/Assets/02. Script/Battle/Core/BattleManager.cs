using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
 * ���� �Ŵ���
 */

namespace Portfolio.Battle
{
    public class BattleManager : MonoBehaviour
    {
        private static BattleUIManager uiManager;        // ���� UI
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
        public float stageOutputTime = 2f;  // �������� ��� ���ð�

        public List<EquipmentItemData> getEquipmentItemList = new List<EquipmentItemData>();    // ���� �¸� �� ���� �������� ����Ʈ
        public Dictionary<int, int> getConsumableItemDic = new Dictionary<int, int>(); // ���� �¸� �� ���� �Һ������ Dic

        //===========================================================
        // SceneLoaderData
        //===========================================================
        public List<Unit> userChoiceUnits;   // ������ ������ ����
        private Map currentMap;  // ������ ������ ��

        //===========================================================
        // TestValue
        //===========================================================
        [Header("TestValue")]
        public int CallMapID = 500;
        public int userUnitTakeCount = 5;

        //===========================================================
        // Property & Singleton
        //===========================================================
        public static BattleManager Instance { get; private set; }
        public static BattleUIManager UIManager { get => uiManager; }
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

        // ORDER : �̱��� Ŭ���� ���� ��
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

            uiManager = GetComponentInChildren<BattleUIManager>();
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
                uiManager.ShowMapInfo(CurrentMap);
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
                userChoiceUnits = GameManager.CurrentUser.UserUnitList.OrderByDescending(GameLib.UnitBattlePowerSort).Take(userUnitTakeCount).ToList();
                battleFactory.CreateUserUnit(userChoiceUnits);

                uiManager.ShowMapInfo(currentMap);
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
            if (getConsumableItemDic.ContainsKey(id))
            // �̹� ������ ���濡 �ִ� �������̸�
            {
                // ȹ�� ���ڸ� ����
                getConsumableItemDic[id] += count;
            }
            else
            // ������ ���濡 ���ٸ�
            {
                // ������ �߰�
                getConsumableItemDic.Add(id, count);
            }
        }
        private void ClearDeadUnit()
        // ���� ���� ����
        {
            var deadUnitList = unitList.Where(unit => unit.IsDead).ToList();
            foreach (var unit in deadUnitList)
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
            UIManager.ShowStageInfo(CurrentMap);
            currentStage = stageDatas.Dequeue();
            BattleFactory.CreateStage(currentStage);
            uiManager.SetStartStageDirect();
            yield return new WaitForSecondsRealtime(stageOutputTime);
            // ���� ���� ����
            uiManager.SetBattleStartDirect();
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
            UIManager.ShowStageInfo(CurrentMap);
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
                // �ʿ� �ִ� ���� �������� ȹ�� ���濡 �־��ݴϴ�.
                AddLootingItem();
                if (currentMap.MapID == Constant.lastMapID)
                // ������ ���̶��
                {
                    // 1�ʵڿ� ���� �� �����ֱ�
                    StartCoroutine(GameLib.WaitMethodCall(1f, () => { SceneLoader.LoadEndingScene(); }));
                }
                else
                // ������ ���� �ƴ϶��
                {
                    // ���� �¸� UI ���
                    UIManager.Win();
                }
                // ���� ������ ���� ������ �־��ֱ�
                UesrGetItem();
                // ���ֵ� ����ġ ���������ֱ�
                UserUnitGetExperience();
                // ���� ����ġ ���������ֱ�
                UserGetExperience();
                // ���� �� ���� �Ѱ��༭ �� Ŭ���� ���ֱ�
                GameManager.CurrentUser.ClearMap(currentMap.MapID);
            }
        }


        public void Defeat()
        // �й�
        {
            SwitchBattleState(BattleState.DEFEAT);
            // �й� UI ���
            UIManager.Defeat();
        }
        private void AddLootingItem()
        {
            // ���� �������̺��� ���ٸ� ����
            if (currentMap.lootItemTable == null) return;

            // ���� ���� �������̺��� ���þ����� ����Ʈ�� �����մϴ�.
            foreach (var ILootingItem in currentMap.lootItemTable.lootItemList)
            {
                if (ILootingItem is LootItemTable.LootingEquipmentItem)
                // ���� �������� ��� �������� ���
                {
                    var lootitem = ILootingItem.GetLootingItem() as LootItemTable.LootingEquipmentItem;
                    if (GameLib.ProbabilityCalculation(lootitem.lootingPercent, 1f))
                    // ������ ȹ�� ������ �������� ���
                    {
                        // ������ Ÿ���� ���������� ��޿� �°� �����մϴ�.
                        EquipmentItemData newEquipmentItem = null;
                        EquipmentItemType type = (EquipmentItemType)UnityEngine.Random.Range(0, 6);
                        switch (type)
                        {
                            case EquipmentItemType.Weapon:
                                newEquipmentItem = GameManager.ItemCreator.CreateEquipmentItemData<WeaponData>(lootitem.gradeType);
                                break;
                            case EquipmentItemType.Helmet:
                                newEquipmentItem = GameManager.ItemCreator.CreateEquipmentItemData<HelmetData>(lootitem.gradeType);
                                break;
                            case EquipmentItemType.Armor:
                                newEquipmentItem = GameManager.ItemCreator.CreateEquipmentItemData<ArmorData>(lootitem.gradeType);
                                break;
                            case EquipmentItemType.Amulet:
                                newEquipmentItem = GameManager.ItemCreator.CreateEquipmentItemData<AmuletData>(lootitem.gradeType);
                                break;
                            case EquipmentItemType.Ring:
                                newEquipmentItem = GameManager.ItemCreator.CreateEquipmentItemData<RingData>(lootitem.gradeType);
                                break;
                            case EquipmentItemType.Shoe:
                                newEquipmentItem = GameManager.ItemCreator.CreateEquipmentItemData<ShoeData>(lootitem.gradeType);
                                break;
                        }

                        if (newEquipmentItem == null) continue;

                        // ȹ�� �������� ���濡 �־��ݴϴ�.
                        getEquipmentItemList.Add(newEquipmentItem);
                    }
                }
                else if (ILootingItem is LootItemTable.LootingConsumableItem)
                // ���� �������� �Һ� �������� ���
                {
                    var lootitem = ILootingItem.GetLootingItem() as LootItemTable.LootingConsumableItem;
                    if (GameLib.ProbabilityCalculation(lootitem.lootingPercent, 1f))
                    // ������ ȹ�� ������ �������� ���
                    // ȹ�� ������ ���濡 �Һ������ ������ �־��ݴϴ�.
                    {
                        // ���� ������ ������ �����ݴϴ�.
                        int count = UnityEngine.Random.Range(lootitem.minCount, lootitem.maxCount + 1);

                        if (count > 0)
                        // ��� ������ 1�� �̻��̸� ȹ�� ���濡 �־��ݴϴ�.
                        {
                            if (!getConsumableItemDic.ContainsKey(lootitem.ID))
                            // ó�� ��� �Һ�������� ��� KV�� �߰��մϴ�.
                            {
                                getConsumableItemDic.Add(lootitem.ID, UnityEngine.Random.Range(lootitem.minCount, lootitem.maxCount + 1));
                            }
                            else
                            // �̹� ȹ�� ���濡 ���� ������ ID�� �ִ� ��� ������ ������Ų��.
                            {
                                getConsumableItemDic[lootitem.ID] += count;
                            }
                        }
                    }
                }
            }
        }

        private void UesrGetItem()
        // ���� �����۵� ���� ���濡 �־��ֱ�
        {
            foreach (var item in getEquipmentItemList)
            {
                GameManager.CurrentUser.TryAddEquipmentItem(item);
            }

            foreach (var itemKV in getConsumableItemDic.ToList())
            {
                GameManager.CurrentUser.AddConsumableItem(itemKV.Key, itemKV.Value);
            }
        }

        // ������ ����ġ ����
        private void UserGetExperience()
        {
            GameManager.CurrentUser.UserCurrentExperience += currentMap.MapUserExperience;
        }

        private void UserUnitGetExperience()
        // ���ֵ� ����ġ ȹ������ֱ�
        {
            var experienceValue = currentMap.MapExperience;
            foreach (var unit in userChoiceUnits)
            {
                if (unit == null) continue;
                unit.CurrentExperience += experienceValue;
            }
        }



        //===========================================================
        // StateEvent
        //===========================================================
        // ORDER : �̺�Ʈ ������ �̿��ؼ� ���� ���� ���¿� ���� �̺�Ʈ ���� �ý���
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

        // �ڵ� ���� ����
        public void TOGGLE_OnChangedValue_SetAutoBattle()
        {
            // ��� �÷��̾�� ���ֿ� ���� �ڵ� ���� üũ
            var list = unitList.Where(battleUnit => !battleUnit.IsEnemy);
            foreach (var unit in list)
            {
                unit.CheckAutoBattle();
            }
        }

        public void BTN_OnClick_ShowConfigurePopup()
        {
            uiManager.ShowConfigurePopup();
            Pause();
        }

        public void BTN_OnClick_DungeonRetry()
        {
            GameManager.UIManager.ShowConfirmation("���� �絵��", "������ ���� ���ֵ�� �絵���Ͻðڽ��ϱ�?\n�������� �Һ���� �ʽ��ϴ�.", () =>
            {
                SceneLoader.LoadBattleScene(userChoiceUnits, CurrentMap);
            });
        }

        public void BTN_OnClick_DungeonExit()
        {
            GameManager.UIManager.ShowConfirmation("���� ������", "������ �����ðڽ��ϱ�?\n�Һ�� �������� �������� �ʽ��ϴ�. .", () =>
            {
                SceneLoader.LoadLobbyScene();
            });
        }
    }

}