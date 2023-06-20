using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class HeroPanelUI : PanelUI
    {
        [Header("Toggle")]
        [SerializeField] ToggleGroup PanelToggleGroup;
        [SerializeField] Toggle unitStatusToggle;
        [SerializeField] Toggle unitCompositionToggle;

        [Header("Panel")]
        [SerializeField] UnitListUI unitListUI;
        [SerializeField] UnitStatusUI unitStatusUI;
        [SerializeField] UnitEquipmentUI unitEquipmentUI;
        [SerializeField] UnitSkillPanelUI unitSkillPanelUI;
        [SerializeField] UnitCompositionPanelUI unitCompositionPanelUI;

        [Header("UnitEquipmentPopup")]
        [SerializeField] EquipmentPopupUI equipmentPopupUI;
        [SerializeField] EquipmentReinforcePopupUI reinforcePopupUI;
        [SerializeField] EquipmentListPopupUI equipmentListPopupUI;
        [SerializeField] EquipmentTooltip equipmentTooltipUI;

        [Header("UnitSkillLevelUPPopup")]
        [SerializeField] SkillLevelUpPopupUI skillLevelUpPopupUI;

        private static Unit selectUnit;
        public static Unit SelectUnit
        {
            get
            {
                return selectUnit;
            }
            set
            {
                selectUnit = value;
                LobbyManager.UIManager.OnUnitChanged();
            }
        }
        private static EquipmentItemData selectEquipmentItem;
        public static EquipmentItemData SelectEquipmentItem
        {
            get
            {
                return selectEquipmentItem;
            }
            set
            {
                selectEquipmentItem = value;
                LobbyManager.UIManager.OnEquipmentItemChanged();
            }
        }
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
        public static int SelectSkillLevel
        {
            get
            {
                return SelectUnit.GetSkillLevel(SelectSkillType);
            }
        }
        private void Awake()
        {
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
            SelectUnit = GameManager.CurrentUser.userUnitList[0];
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
            unitStatusToggle.isOn = true;
            GameManager.Instance.SaveUser();
            
        }

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
        public void ShowEquipmentUI()
        {
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

        public void ShowSkillUI()
        {
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

        public void ShowEquipmentPopupUI()
        {
            equipmentPopupUI.gameObject.SetActive(true);
            reinforcePopupUI.gameObject.SetActive(false);
            equipmentListPopupUI.gameObject.SetActive(false);
            LobbyManager.UIManager.OnEquipmentItemChanged();
        }

        public void ShowReinforcePopup()
        {
            reinforcePopupUI.gameObject.SetActive(true);
            equipmentListPopupUI.gameObject.SetActive(false);
        }

        public void ShowEquipmentListPopup()
        {
            equipmentListPopupUI.gameObject.SetActive(true);
            reinforcePopupUI.gameObject.SetActive(false);
        }

        public void ShowSkillLevelUPPopup()
        {
            skillLevelUpPopupUI.gameObject.SetActive(true);
            skillLevelUpPopupUI.ShowPopup();
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
            var releaseItem = selectUnit.ReleaseEquipment(selectEquipmentItemType);
            GameManager.CurrentUser.userEquipmentItemDataList.Add(releaseItem);
            selectEquipmentItem = null;
            choiceEquipmentItem = null;

            equipmentListPopupUI.UnChoiceList();
            ReShow();
            GameManager.Instance.SaveUser();
        }

        public void ChangeEquipment()
        {
            GameManager.CurrentUser.userEquipmentItemDataList.Remove(choiceEquipmentItem);
            var releaseItem = selectUnit.ChangeEquipment(selectEquipmentItemType, choiceEquipmentItem);
            if (releaseItem != null)
            {
                GameManager.CurrentUser.userEquipmentItemDataList.Add(selectEquipmentItem);
            }

            selectEquipmentItem = choiceEquipmentItem;
            choiceEquipmentItem = null;
            equipmentListPopupUI.UnChoiceList();
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
    }
}
