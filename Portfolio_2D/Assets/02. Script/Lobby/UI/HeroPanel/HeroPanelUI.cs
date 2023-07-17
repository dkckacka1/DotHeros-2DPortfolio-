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
        private static eEquipmentItemType selectEquipmentItemType = eEquipmentItemType.Weapon;
        public static eEquipmentItemType SelectEquipmentItemType
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
        private static eUnitSkillType selectSkillType;
        public static eUnitSkillType SelectSkillType
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
            unitCompositionToggle.isOn = false;
            unitCompositionToggle.onValueChanged?.Invoke(false);
            unitStatusToggle.isOn = true;
            unitStatusToggle.onValueChanged?.Invoke(true);
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
        public void TOGGLE_OnClick_ResetPanel()
        {

            if (!unitEquipmentUI.gameObject.activeInHierarchy || !unitSkillPanelUI.gameObject.activeInHierarchy)
            {
                if (LobbyManager.UIManager.UndoCount() >= 2)
                {
                    LobbyManager.UIManager.Undo();
                }
            }
        }

        // 스테이터스 패널 창을 보여준다.
        public void BTN_OnClick_ShowStatus()
        {
            unitListUI.SetStatus();
            SelectUnit = GameManager.CurrentUser.UserUnitList[0];
        }

        // 유닛 합성창을 보여준다.
        public void BTN_OnClick_ShowComposition()
        {
            unitListUI.SetComposition();
        }

        // 유닛 장비창을 보여준다.
        public void BTN_OnClick_ShowEquipmentUI()
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
        public void BTN_OnClick_ShowSkillUI()
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
        public void BTN_OnClick_ShowReinforcePopup()
        {
            reinforcePopupUI.gameObject.SetActive(true);
            equipmentListPopupUI.gameObject.SetActive(false);
        }

        // 장비 리스트 팝업창을 보여준다.
        public void BTN_OnClick_ShowEquipmentListPopup()
        {
            equipmentListPopupUI.gameObject.SetActive(true);
            reinforcePopupUI.gameObject.SetActive(false);
        }

        // 스킬 레벨업 팝업창을 보여준다.
        public void BTN_OnClick_ShowSkillLevelUPPopup()
        {
            skillLevelUpPopupUI.gameObject.SetActive(true);
            skillLevelUpPopupUI.Show();
        }


        // 선택한 장비를 강화한다.
        public void BTN_OnClick_Reinforce()
        {
            // 유저의 골드를 강화 비용만큼 감소시킨다.
            GameManager.CurrentUser.Gold -= Constant.ReinforceConsumeGoldValues[selectEquipmentItem.reinforceCount];

            if (Random.Range(0f, 1f) <= Constant.ReinforceProbabilitys[selectEquipmentItem.reinforceCount])
            // 강화 성공 여부를 확인해서 성공시 
            {
                // 선택한 장비를 강화한다.
                GameManager.AudioManager.PlaySoundOneShot("Sound_ReinforceSuccess");
                GameManager.ItemCreator.ReinforceEquipment(selectEquipmentItem);
            }
            else
            {
                GameManager.AudioManager.PlaySoundOneShot("Sound_ReinforceFailed");
            }

            // UI를 업데이트하고 유저 정보를 저장합니다.
            ReShow();
            GameManager.Instance.SaveUser();
        }

        // 장비를 해제한다.
        public void BTN_OnClick_ReleaseEquipment()
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
                // UI를 업데이트하고 유저 정보를 저장합니다.
                ReShow();
                GameManager.Instance.SaveUser();
            }
        }

        // 장비를 교체합니다.
        public void BTN_OnClick_ChangeEquipment()
        {
            // 현재 장비 인벤토리에서 선택한 장비를 제거합니다.
            GameManager.CurrentUser.GetInventoryEquipmentItem.Remove(choiceEquipmentItem);
            // 선택한 유닛의 바꾸고자 하는 장비 타입의 아이템을 인벤토리에서 선택한 아이템으로 교체합니다.
            var releaseItem = selectUnit.ChangeEquipment(selectEquipmentItemType, choiceEquipmentItem);
            if (releaseItem != null)
            // 기존에 장착중인 아이템이 있었다면
            {
                // 인벤토리에 넣어줍니다.
                GameManager.CurrentUser.AddEquipmentItem(selectEquipmentItem);
            }

            // 교체한 아이템을 현재 유저가 선택한 아이템으로 변경해줍니다.
            selectEquipmentItem = choiceEquipmentItem;
            // 선택한 아이템은 초기화
            choiceEquipmentItem = null;
            equipmentListPopupUI.ChoiceItemReset();
            // UI를 업데이트하고 유저 정보를 저장합니다.
            ReShow();
            GameManager.Instance.SaveUser();
        }

        // 경험치를 획득합니다.
        public void BTN_OnClick_GetExperience(float getValue)
        {
            if(!selectUnit.IsMaxLevel && selectUnit.CurrentExperience + getValue >= selectUnit.MaxExperience)
                // 유닛의 최대 레벨이 아니고, 획득 경험치와 현재 경험치의 합이 최대 경험치를 넘어설 경우
            {
                GameManager.AudioManager.PlaySoundOneShot("Sound_HeroPanelUnitLevelup");
            }
            else 
            {
                GameManager.AudioManager.PlaySoundOneShot("Sound_GetExperience");
            }

            // 선택한 유닛의 경험치를 증가 시킵니다.
            selectUnit.CurrentExperience += getValue;
            // UI를 업데이트하고 유저 정보를 저장합니다.
            ReShow();
            GameManager.Instance.SaveUser();
        }

        // 스킬을 레벨업합니다.
        public void BTN_OnClick_SkillLevelUp()
        {
            GameManager.AudioManager.PlaySoundOneShot("Sound_SkillLevelUp");
            skillLevelUpPopupUI.SkillLevelUP();
            // UI를 업데이트하고 유저 정보를 저장합니다.
            ReShow();
            GameManager.Instance.SaveUser();
        }

        // 유저의 유닛 슬롯의 최대갯수를 늘립니다.
        public void BTN_OnClick_SizeUPUintList()
        {
            if (GameManager.CurrentUser.MaxUnitListCount >= Constant.UnitListMaxSizeCount)
            // 이미 최대 사이즈에 도달한 상태라면 경고창을 표시합니다.
            {
                GameManager.UIManager.ShowAlert("이미 최대 사이즈에 도달했습니다!");
            }
            else
            // 최대 사이즈가 아니라면 확인 다이얼로그 창을 표시합니다.
            {
                int consumeDia = Constant.UnitListSizeUPDiaConsumeValue;
                GameManager.UIManager.ShowConfirmation("최대 유닛 개수 증가", $"최대 유닛 개수를 증가 시키겠습니까?\n{consumeDia} 다이아가 소비되며\n{Constant.UnitListSizeUpCount}칸이 늘어납니다.\n(최대 100칸)", SizeUp);
            }
        }

        // 유저의 유닛 슬롯의 최대 갯수를 늘립니다.
        private void SizeUp()
        {
            if (GameManager.CurrentUser.CanDIamondConsume(Constant.UnitListSizeUPDiaConsumeValue))
            // 유닛 슬롯 개수를 늘릴 다이아 양이 충분하다면 
            {
                GameManager.AudioManager.PlaySoundOneShot("Sound_ListSizeUP");
                //다이아양을 소비하고 최대 갯수를 증가 시킵니다.
                GameManager.CurrentUser.Diamond -= Constant.UnitListSizeUPDiaConsumeValue;
                GameManager.CurrentUser.MaxUnitListCount += Constant.UnitListSizeUpCount;
                // 유닛슬롯 텍스트를 업데이트 합니다.
                unitListUI.ShowUnitListCountText();
            }
            else
            // 다이아양이 불충분하다면
            {
                // 경고창 표시
                GameManager.UIManager.ShowAlert("다이아몬드가 부족합니다!");
            }
        }
    }
}
