using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
 * 전투 매니저
 */

namespace Portfolio.Battle
{
    public class BattleManager : MonoBehaviour
    {
        private static BattleUIManager uiManager;        // 전투 UI
        private static BattleFactory battleFactory;     // 전투유닛 생성기
        private static TurnBaseSystem turnBaseSystem;   // 턴제 시스템
        private static ActionSystem actionSystem;       // 액션 시스템
        private static ManaSystem manaSystem;           // 마나 시스템
        private static ObjectPool objectPool;           // 오브젝트 풀

        private List<BattleUnit> unitList = new List<BattleUnit>(30);             // 현재 유닛
        private Dictionary<eBattleState, UnityEvent> StateEventHandlerDic = new Dictionary<eBattleState, UnityEvent>(); // 전투 상태 이벤트 Dic

        [SerializeField] private eBattleState currentBattleState = eBattleState.None; // 현재 전투 상태

        public Queue<Stage> stageDatas = new Queue<Stage>(); // 스테이지 정보 큐
        public Stage currentStage; // 현재 스테이지
        public float stageOutputTime = 2f;  // 스테이지 출력 대기시간

        public List<EquipmentItemData> getEquipmentItemList = new List<EquipmentItemData>(5);    // 전투 승리 후 얻은 장비아이템 리스트
        public Dictionary<int, int> getConsumableItemDic = new Dictionary<int, int>(5); // 전투 승리 후 얻은 소비아이템 Dic

        //===========================================================
        // SceneLoaderData
        //===========================================================
        public List<Unit> userChoiceUnits;   // 유저가 설정한 유닛
        private Map currentMap;  // 유적가 선택한 맵

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
        public eBattleState BattleState { get => currentBattleState; }
        public Map CurrentMap
        {
            get => currentMap;
            set => currentMap = value;
        }
        public List<BattleUnit> UnitList => unitList;

        // ORDER : 싱글톤 클래스 생성 예
        private void Awake()
        {
            // 싱글톤 생성
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
            // 음악을 새롭게 재생합니다.
            GameManager.AudioManager.PlayMusic("Music_BattleScene", true);

            if (!GameManager.Instance.isTest)
            // 테스트 상태가 아니면
            {
                // 맵 세팅
                SetMap();
                // 유저 유닛 세팅
                SetUserUnit();
                // 전투 UI 현재 맵 데이터 바인딩
                uiManager.ShowMapInfo(CurrentMap);
                // 스테이지 시작 연출
                StartCoroutine(SetStartStage());
            }
            else
            // 테스트 중이면
            {
                // MapID 값으로 맵 세팅
                GameManager.Instance.TryGetMap(CallMapID, out currentMap);
                for (int i = 0; i < currentMap.StageList.Count; i++)
                {
                    stageDatas.Enqueue(currentMap.StageList[i]);
                }

                // 유저 유닛을 전투력 순으로 정렬 후 userUnitTakeCount 갯수만큼 가져와서 세팅
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
            // 유저가 선택한 맵정보를 가져와서 세팅
            this.CurrentMap = SceneLoader.userChocieMap;
            for (int i = 0; i < CurrentMap.StageList.Count; i++)
            {
                // 스테이지 큐에 순차적으로 다음 스테이지를 넣어주기
                stageDatas.Enqueue(CurrentMap.StageList[i]);
            }
        }

        private void SetUserUnit()
        // 유저가 선택한 유닛 리스트를 가져와서 전투 유닛 생성 해주기
        {
            userChoiceUnits = SceneLoader.userChoiceUnits;
            battleFactory.CreateUserUnit(userChoiceUnits);
        }

        //===========================================================
        // UnitList
        //===========================================================

        // 유닛리스트에 유닛 넣기
        public void AddUnitinUnitList(BattleUnit unit) => unitList.Add(unit);
        // 유닛 리스트에서 유닛 제거
        public void RemoveUnit(BattleUnit unit)
        {
            unitList.Remove(unit);
        }
        // 유닛 리스트 초기화
        public void ClearUnitinUnitList()
        {
            unitList.Clear();
        }
        // Func에 맞는 유닛 리스트 리턴
        public IEnumerable<BattleUnit> GetUnitList(Func<BattleUnit, bool> predicate)
        {
            return unitList.Where(predicate);
        }

        // 플레이어블 유닛 혹은 적 유닛 리스트
        private IEnumerable<BattleUnit> GetUnitList(bool isEnemy) => unitList.Where(battleUnit => (battleUnit.IsEnemy == isEnemy) && !battleUnit.IsDead);
        public void CheckUnitList()
        {
            if (GetUnitList(true).Count() == 0)
            // 살아있는 적 유닛이 0명일 경우
            {
                //승리
                Win();
            }

            if (GetUnitList(false).Count() == 0)
            // 살아있는 플레이어 유닛이 0명일 경우
            {
                // 패배
                Defeat();
            }
        }
        public void GetItem(int id, int count)
        // 아이템 획득
        {
            if (getConsumableItemDic.ContainsKey(id))
            // 이미 아이템 가방에 있는 아이템이면
            {
                // 획득 숫자만 조절
                getConsumableItemDic[id] += count;
            }
            else
            // 아이템 가방에 없다면
            {
                // 새로이 추가
                getConsumableItemDic.Add(id, count);
            }
        }
        private void ClearDeadUnit()
        // 죽은 유닛 삭제
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
        public void SwitchBattleState(eBattleState state)
        // 전투 상태 변경
        {
            currentBattleState = state;
            // 전투 상태 변경에 따른 이벤트를 호출해준다.
            InvokeStateEvent(state);
        }

        public void Play()
        // 전투 중
        {
            SwitchBattleState(eBattleState.Play);
        }

        public IEnumerator SetStartStage()
        // 첫번째 스테이지 시작
        {
            SwitchBattleState(eBattleState.SetStage);
            UIManager.ShowStageInfo(CurrentMap);
            currentStage = stageDatas.Dequeue();
            BattleFactory.CreateStage(currentStage);
            // 전투 시작 효과음 재생
            GameManager.AudioManager.PlaySoundOneShot("Sound_BattleStart");
            uiManager.SetStartStageDirect();
            yield return new WaitForSecondsRealtime(stageOutputTime);
            // 전투 시작 연출
            uiManager.SetBattleStartDirect();
            BattleStart();
        }

        public void SetNextStage()
        // 다음 스테이지 시작
        {
            SwitchBattleState(eBattleState.SetStage);
            StartCoroutine(SetStageSequence());
        }
        private IEnumerator SetStageSequence()
        // 스테이지 출력 연출
        {
            yield return new WaitForSeconds(stageOutputTime);
            // 죽은 유닛 삭제
            ClearDeadUnit();
            // 스테이지 정보 바인딩
            UIManager.ShowStageInfo(CurrentMap);
            // 다음 스테이지 정보 가져오기
            currentStage = stageDatas.Dequeue();
            // 다음 스테이지 적군 전투 유닛 세팅
            BattleFactory.CreateStage(currentStage);
            // 모든 턴 초기화 해주기
            turnBaseSystem.ResetAllUnitTurn();
            yield return new WaitForSeconds(stageOutputTime);
            Play();
        }
        public void BattleStart()
        // 전투 시작
        {
            SwitchBattleState(eBattleState.BattleStart);

            Play();
        }

        public void Pause()
        // 전투 멈춤
        {
            SwitchBattleState(eBattleState.Pause);
        }

        public void Win()
        // 승리
        {
            SwitchBattleState(eBattleState.Win);
            if (stageDatas.Count() >= 1)
            // 다음 스테이지가 남아있다면
            {
                // 다음 스테이지 출력
                SetNextStage();
            }
            else
            // 다음 스테이지가 없다면
            {
                // 맵에 있는 루팅 아이템을 획득 가방에 넣어줍니다.
                AddLootingItem();
                if (currentMap.MapID == Constant.LastMapID)
                // 마지막 맵이라면
                {
                    // 1초뒤에 엔딩 씬 보여주기
                    StartCoroutine(GameLib.WaitMethodCall(1f, () => { SceneLoader.LoadEndingScene(); }));
                }
                else
                // 마지막 맵이 아니라면
                {
                    // 전투 승리 UI 출력
                    UIManager.Win();
                }
                // 유저 정보에 얻은 아이템 넣어주기
                UesrGetItem();
                // 유닛들 경험치 증가시켜주기
                UserUnitGetExperience();
                // 유저 경험치 증가시켜주기
                UserGetExperience();
                // 현재 맵 정보 넘겨줘서 맵 클리어 해주기
                GameManager.CurrentUser.ClearMap(currentMap.MapID);
            }
        }


        public void Defeat()
        // 패배
        {
            SwitchBattleState(eBattleState.Defeat);
            // 패배 UI 출력
            UIManager.Defeat();

        }
        private void AddLootingItem()
        {
            // 만약 루팅테이블이 없다면 리턴
            if (currentMap.lootItemTable == null) return;

            // 현재 맵의 루팅테이블에서 루팅아이템 리스트를 참조합니다.
            foreach (var ILootingItem in currentMap.lootItemTable.lootItemList)
            {
                if (ILootingItem is LootItemTable.LootingEquipmentItem)
                // 루팅 아이템이 장비 아이템일 경우
                {
                    var lootitem = ILootingItem.GetLootingItem() as LootItemTable.LootingEquipmentItem;
                    if (GameLib.ProbabilityCalculation(lootitem.lootingPercent, 1f))
                    // 아이템 획득 판정에 성공했을 경우
                    {
                        // 랜덤한 타입의 장비아이템을 등급에 맞게 생성합니다.
                        EquipmentItemData newEquipmentItem = null;
                        eEquipmentItemType type = (eEquipmentItemType)UnityEngine.Random.Range(0, 6);
                        switch (type)
                        {
                            case eEquipmentItemType.Weapon:
                                newEquipmentItem = GameManager.ItemCreator.CreateEquipmentItemData<WeaponData>(lootitem.gradeType);
                                break;
                            case eEquipmentItemType.Helmet:
                                newEquipmentItem = GameManager.ItemCreator.CreateEquipmentItemData<HelmetData>(lootitem.gradeType);
                                break;
                            case eEquipmentItemType.Armor:
                                newEquipmentItem = GameManager.ItemCreator.CreateEquipmentItemData<ArmorData>(lootitem.gradeType);
                                break;
                            case eEquipmentItemType.Amulet:
                                newEquipmentItem = GameManager.ItemCreator.CreateEquipmentItemData<AmuletData>(lootitem.gradeType);
                                break;
                            case eEquipmentItemType.Ring:
                                newEquipmentItem = GameManager.ItemCreator.CreateEquipmentItemData<RingData>(lootitem.gradeType);
                                break;
                            case eEquipmentItemType.Shoe:
                                newEquipmentItem = GameManager.ItemCreator.CreateEquipmentItemData<ShoeData>(lootitem.gradeType);
                                break;
                            default:
                                Debug.LogWarning("unknownType");
                                break;
                        }

                        if (newEquipmentItem == null) continue;

                        // 획득 장비아이템 가방에 넣어줍니다.
                        getEquipmentItemList.Add(newEquipmentItem);
                    }
                }
                else if (ILootingItem is LootItemTable.LootingConsumableItem)
                // 루팅 아이템이 소비 아이템일 경우
                {
                    var lootitem = ILootingItem.GetLootingItem() as LootItemTable.LootingConsumableItem;
                    if (GameLib.ProbabilityCalculation(lootitem.lootingPercent, 1f))
                    // 아이템 획득 판정에 성공했을 경우
                    // 획득 아이템 가방에 소비아이템 정보를 넣어줍니다.
                    {
                        // 얻을 아이템 갯수를 정해줍니다.
                        int count = UnityEngine.Random.Range(lootitem.minCount, lootitem.maxCount + 1);

                        if (count > 0)
                        // 얻는 갯수가 1개 이상이면 획득 가방에 넣어줍니다.
                        {
                            if (!getConsumableItemDic.ContainsKey(lootitem.ID))
                            // 처음 얻는 소비아이템인 경우 KV로 추가합니다.
                            {
                                getConsumableItemDic.Add(lootitem.ID, UnityEngine.Random.Range(lootitem.minCount, lootitem.maxCount + 1));
                            }
                            else
                            // 이미 획득 가방에 얻을 아이템 ID가 있는 경우 갯수만 증가시킨다.
                            {
                                getConsumableItemDic[lootitem.ID] += count;
                            }
                        }
                    }
                }
            }
        }

        private void UesrGetItem()
        // 얻은 아이템들 유저 가방에 넣어주기
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

        // 유저의 경험치 증가
        private void UserGetExperience()
        {
            GameManager.CurrentUser.UserCurrentExperience += currentMap.MapUserExperience;
        }

        private void UserUnitGetExperience()
        // 유닛들 경험치 획득시켜주기
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
        // ORDER : 이벤트 버스를 이용해서 만든 전투 상태에 따른 이벤트 구독 시스템
        public void PublishEvent(eBattleState state, UnityAction action)
        // 전투 상태에 이벤트 구독
        {
            if (StateEventHandlerDic.ContainsKey(state))
            // 이벤트 Dic에 해당 전투 상태 KEY가 있다면
            {
                //이벤트 구독
                StateEventHandlerDic[state].AddListener(action);
            }
            else
            {
                //KV 생성후 이벤트 구독
                StateEventHandlerDic.Add(state, new UnityEvent());
                StateEventHandlerDic[state].AddListener(action);
            }
        }

        public void UnPublishEvent(eBattleState state, UnityAction action)
        // 이벤트 구독 해제
        {
            if (StateEventHandlerDic.ContainsKey(state))
            {
                StateEventHandlerDic[state].RemoveListener(action);
            }
        }

        public void InvokeStateEvent(eBattleState state)
        // 구독한 이벤트 모두 호출
        {
            if (StateEventHandlerDic.ContainsKey(state))
            {
                StateEventHandlerDic[state]?.Invoke();
            }
        }

        //===========================================================
        // BtnPlugin
        //===========================================================

        // 자동 전투 설정
        public void TOGGLE_OnChangedValue_SetAutoBattle()
        {
            // 모든 플레이어블 유닛에 대한 자동 전투 체크
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
            GameManager.UIManager.ShowConfirmation("던전 재도전", "던전을 현재 유닛들로 재도전하시겠습니까?\n에너지는 소비되지 않습니다.", () =>
            {
                SceneLoader.LoadBattleScene(userChoiceUnits, CurrentMap);
            });
        }

        public void BTN_OnClick_DungeonExit()
        {
            GameManager.UIManager.ShowConfirmation("던전 나가기", "던전을 나가시겠습니까?\n소비된 에너지는 복구되지 않습니다. .", () =>
            {
                SceneLoader.LoadLobbyScene();
            });
        }
    }

}