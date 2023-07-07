using Portfolio.Lobby.Hero.Composition;
using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 영웅창을 보여주는 UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class HeroPanelUI : PanelUI
    {
        [Header("Toggle")]
        [SerializeField] ToggleGroup PanelToggleGroup;      // 토글 그룹
        [SerializeField] Toggle unitStatusToggle;           // 유닛 스탯창을 보여주는 토글
        [SerializeField] Toggle unitCompositionToggle;      // 유닛 합성창을 보여주는 토글

        [Header("Panel")]
        [SerializeField] UnitListUI unitListUI;                             // 유닛 리스트 UI 패널
        [SerializeField] UnitStatusUI unitStatusUI;                         // 유닛 스탯창 UI 패널
        [SerializeField] UnitEquipmentPanelUI unitEquipmentUI;              // 유닛 장비창 UI 패널
        [SerializeField] UnitSkillPanelUI unitSkillPanelUI;                 // 유닛 스킬창 UI 패널
        [SerializeField] UnitCompositionPanelUI unitCompositionPanelUI;     // 유닛 합성창 UI 패널

        [Header("UnitEquipmentPopup")]
        [SerializeField] EquipmentPopupUI equipmentPopupUI;         // 장비창 팝업 UI
        [SerializeField] EquipmentReinforcePopupUI reinforcePopupUI;// 장비 강화창 팝업 UI
        [SerializeField] EquipmentListPopupUI equipmentListPopupUI; // 장비 리스트 팝업 UI
        [SerializeField] EquipmentTooltip equipmentTooltipUI;       // 장비 툴팁 UI

        [Header("UnitSkillLevelUPPopup")]
        [SerializeField] SkillLevelUpPopupUI skillLevelUpPopupUI;   // 유닛 스킬 레벨업 팝업 UI

        // 현재 유저가 선택한 유닛
        private static Unit selectUnit;
        public static Unit SelectUnit
        {
            get
            {
                return selectUnit;
            }
            set
            {
                // 선택 유닛이 바뀌었다면 유닛 변경 이벤트를 호출한다.
                selectUnit = value;
                LobbyManager.UIManager.OnUnitChanged();
            }
        }
        // 현재 유저가 선택한 장비 아이템
        private static EquipmentItemData selectEquipmentItem;
        public static EquipmentItemData SelectEquipmentItem
        {
            get
            {
                return selectEquipmentItem;
            }
            set
            {
                // 선택 장비 아이템이 바뀌었다면 유닛 변경 이벤트를 호출한다.
                selectEquipmentItem = value;
                LobbyManager.UIManager.OnEquipmentItemChanged();
            }
        }
        
        // 현재 유저가 선택한 장비 타입
        private static EquipmentItemType selectEquipmentItemType = EquipmentItemType.Weapon;
        public static EquipmentItemType SelectEquipmentItemType
        {
            get
            {
                return selectEquipmentItemType;
            }
            set
            {
                selectEquipmentItemType = value;
            }
        }
        
        // 장비 리스트 팝업창에서 유저가 선택한 장비
        private static EquipmentItemData choiceEquipmentItem;
        public static EquipmentItemData ChoiceEquipmentItem
        {
            get
            {
                return choiceEquipmentItem;
            }
            set
            {
                choiceEquipmentItem = value;
                LobbyManager.UIManager.OnEquipmentItemChanged();
            }
        }

        // 현재 유저가 선택한 스킬
        private static Skill selectSkill;
        public static Skill SelectSkill
        {
            get
            {
                return selectSkill;
            }
            set
            {
                selectSkill = value;
            }
        }

        // 현재 유저가 선택한 스킬 타입
        private static UnitSkillType selectSkillType;
        public static UnitSkillType SelectSkillType
        {
            get
            {
                return selectSkillType;
            }
            set
            {
                selectSkillType = value;
            }
        }

        // 현재 유저가 선택한 유닛의 스킬 레벨을 불러온다.
        public static int SelectSkillLevel
        {
            get
            {
                return SelectUnit.GetSkillLevel(SelectSkillType);
            }
        }
        private void Awake()
        {
            // 모든 패널, 팝업 UI를 초기화 해준다.
            unitListUI.Init();
            unitStatusUI.Init();
            unitEquipmentUI.Init();
            unitSkillPanelUI.Init();
            equipmentPopupUI.Init();
            reinforcePopupUI.Init();
            equipmentListPopupUI.Init();
        }

        private void Start()
        {
            // 처음 창을 킬 경우 유저의 첫번째유닛을 보여주도록 만든다.
            SelectUnit = GameManager.CurrentUser.UserUnitList[0];
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        private void OnDisable()
        {
            // 초기 상태로 되돌리기
            equipmentPopupUI.gameObject.SetActive(false);
            reinforcePopupUI.gameObject.SetActive(false);
            equipmentListPopupUI.gameObject.SetActive(false);
            equipmentTooltipUI.gameObject.SetActive(false);
            skillLevelUpPopupUI.gameObject.SetActive(false);
            unitCompositionToggle.onValueChanged.Invoke(false);
            unitStatusToggle.onValueChanged.Invoke(true);
            unitStatusToggle.isOn = true;
            GameManager.Instance.SaveUser();
            
        }

        // 모든 영웅창의 UI를 업데이트 해준다.
        public void ReShow()
        {
            unitListUI.ShowUnitList();
            LobbyManager.UIManager.OnUnitChanged();
            LobbyManager.UIManager.OnEquipmentItemChanged();
        }

        //===========================================================
        // TogglePlugin
        //===========================================================
        public void ResetPanel()
        {
            
            if (!unitEquipmentUI.gameObject.activeInHierarchy || !unitSkillPanelUI.gameObject.activeInHierarchy)
            {
                if (LobbyManager.UIManager.UndoCount() >= 2)
                {
                    LobbyManager.UIManager.Undo();
                }
            }
        }

        //===========================================================
        // ShowUI
        //===========================================================
        // TODO : 주석 마저 달고 버튼 메서드 이름 변경해주어야함
        // 스테이터스 패널 창을 보여준다.
        public void ShowStatus()
        {
            unitListUI.SetStatus();
            SelectUnit = GameManager.CurrentUser.UserUnitList[0];
        }

        // 유닛 합성창을 보여준다.
        public void ShowComposition()
        {
            unitListUI.SetComposition();
        }


        // 유닛 장비창을 보여준다.
        public void ShowEquipmentUI()
        {
            // 스킬창이 켜저있는 상태라면 언두를 실행한다.
            if (!unitEquipmentUI.gameObject.activeInHierarchy)
            {
                if (LobbyManager.UIManager.UndoCount() >= 2)
                {
                    LobbyManager.UIManager.Undo();
                }

                unitEquipmentUI.gameObject.SetActive(true);
                LobbyManager.UIManager.AddUndo(unitEquipmentUI);
            }
        }

        // 유닛 스킬창을 보여준다.
        public void ShowSkillUI()
        {
            // 장비창이 켜져있는 상태라면 언두를 실행한다.
            if (!unitSkillPanelUI.gameObject.activeInHierarchy)
            {
                if (LobbyManager.UIManager.UndoCount() >= 2)
                {
                    LobbyManager.UIManager.Undo();
                }

                unitSkillPanelUI.gameObject.SetActive(true);
                LobbyManager.UIManager.AddUndo(unitSkillPanelUI);
            }
        }

        // 장비 팝업창을 보여준다.
        public void ShowEquipmentPopupUI()
        {
            equipmentPopupUI.gameObject.SetActive(true);
            reinforcePopupUI.gameObject.SetActive(false);
            equipmentListPopupUI.gameObject.SetActive(false);
            LobbyManager.UIManager.OnEquipmentItemChanged();
        }

        // 장비 강화 팝업창을 보여준다.
        public void ShowReinforcePopup()
        {
            reinforcePopupUI.gameObject.SetActive(true);
            equipmentListPopupUI.gameObject.SetActive(false);
        }

        // 장비 리스트 팝업창을 보여준다.
        public void ShowEquipmentListPopup()
        {
            equipmentListPopupUI.gameObject.SetActive(true);
            reinforcePopupUI.gameObject.SetActive(false);
        }

        // 스킬 레벨업 팝업창을 보여준다.
        public void ShowSkillLevelUPPopup()
        {
            skillLevelUpPopupUI.gameObject.SetActive(true);
            skillLevelUpPopupUI.Show();
        }

        //===========================================================
        // BtnPlugin
        //===========================================================
        public void Reinforce()
        {
            GameManager.CurrentUser.Gold-= Constant.reinforceConsumeGoldValues[selectEquipmentItem.reinforceCount];

            if (Random.Range(0f, 1f) <= Constant.reinforceProbabilitys[selectEquipmentItem.reinforceCount])
            {
                GameManager.ItemCreator.ReinforceEquipment(selectEquipmentItem);
            }

            ReShow();
            GameManager.Instance.SaveUser();
        }

        public void ReleaseEquipment()
        {
            if (GameManager.CurrentUser.IsMaxEquipmentCount)
                // 장비 인벤토리에 넣을 공간이 없다면
            {
                GameManager.UIManager.ShowAlert("장비를 넣을 공간이 없습니다.\n장비 인벤토리에서 장비를 버려주세요.");
            }
            else
                // 장비 인벤토리에 넣을 공간이 있다면
            {
                var releaseItem = selectUnit.ReleaseEquipment(selectEquipmentItemType);
                GameManager.CurrentUser.AddEquipmentItem(releaseItem);
                selectEquipmentItem = null;
                choiceEquipmentItem = null;

                equipmentListPopupUI.ChoiceItemReset();
                ReShow();
                GameManager.Instance.SaveUser();
            }
        }

        public void ChangeEquipment()
        {
            GameManager.CurrentUser.GetInventoryEquipmentItem.Remove(choiceEquipmentItem);
            var releaseItem = selectUnit.ChangeEquipment(selectEquipmentItemType, choiceEquipmentItem);
            if (releaseItem != null)
            {
                GameManager.CurrentUser.AddEquipmentItem(selectEquipmentItem);
            }

            selectEquipmentItem = choiceEquipmentItem;
            choiceEquipmentItem = null;
            equipmentListPopupUI.ChoiceItemReset();
            ReShow();
            GameManager.Instance.SaveUser();
        }

        public void GetExperience(float getValue)
        {
            selectUnit.CurrentExperience += getValue;
            ReShow();
        }

        public void SkillLevelUp()
        {
            skillLevelUpPopupUI.SkillLevelUP();
            ReShow();
        }

        public void SizeUPUintList()
        {
            Debug.Log("사이즈업 버튼 누름");
            if (GameManager.CurrentUser.MaxUnitListCount >= Constant.unitListMaxSizeCount)
            {
                GameManager.UIManager.ShowAlert("이미 최대 사이즈에 도달했습니다!");
            }
            else
            {
                int consumeDia = Constant.unitListSizeUPDiaConsumeValue;
                GameManager.UIManager.ShowConfirmation("최대 유닛 개수 증가", $"최대 유닛 개수를 증가 시키겠습니까?\n{consumeDia} 다이아가 소비되며\n{Constant.unitListSizeUpCount}칸이 늘어납니다.\n(최대 100칸)", SizeUp);
                Debug.Log("사이즈업 버튼 확인 누름");
            }
        }

        private void SizeUp()
        {
            if (GameManager.CurrentUser.CanDIamondConsume(Constant.unitListSizeUPDiaConsumeValue))
            {
                GameManager.CurrentUser.Diamond -= Constant.unitListSizeUPDiaConsumeValue;
                GameManager.CurrentUser.MaxUnitListCount += Constant.unitListSizeUpCount;
                unitListUI.ShowUnitListCountText();
                Debug.Log("증가됨");
            }
            else
            {
                GameManager.UIManager.ShowAlert("다이아몬드가 부족합니다!");
            }
        }
    }
}
