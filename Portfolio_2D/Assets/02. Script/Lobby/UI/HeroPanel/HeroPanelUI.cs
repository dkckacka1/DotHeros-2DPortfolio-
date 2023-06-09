using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby.Hero
{
    public class HeroPanelUI : PanelUI
    {
        [SerializeField] UnitListUI unitListUI;
        [SerializeField] UnitStatusUI unitStatusUI;
        [SerializeField] UnitEquipmentUI unitEquipmentUI;
        [SerializeField] UnitSkillPanelUI unitSkillPanelUI;
        [SerializeField] EquipmentPopupUI equipmentPopupUI;
        [SerializeField] EquipmentReinforcePopupUI reinforcePopupUI;
        [SerializeField] EquipmentListPopupUI equipmentListPopupUI;
        [SerializeField] EquipmentTooltip equipmentTooltipUI;
        [SerializeField] RectTransform potionSlotPanel;

        Unit selectUnit;
        EquipmentItemData selectEquipmentItem;
        EquipmentItemType selectEquipmentItemType;
        EquipmentItemData choiceEquipmentItem;

        private void Awake()
        {
            unitListUI.Init();
            equipmentListPopupUI.Init();
        }

        private void Start()
        {
            selectUnit = GameManager.CurrentUser.userUnitList[0];
            ShowUnit(selectUnit);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            unitEquipmentUI.gameObject.SetActive(false);
            unitSkillPanelUI.gameObject.SetActive(false);
            equipmentPopupUI.gameObject.SetActive(false);
            reinforcePopupUI.gameObject.SetActive(false);
            equipmentListPopupUI.gameObject.SetActive(false);
            equipmentTooltipUI.gameObject.SetActive(false);
            potionSlotPanel.gameObject.SetActive(false);
        }
        public void ShowUnit(Unit unit)
        {
            unitStatusUI.ShowStat(unit);
            unitEquipmentUI.ShowEquipment(unit);
            unitSkillPanelUI.ShowSkill(unit);
            potionSlotPanel.gameObject.SetActive(false);
        }

        public void ShowEquipmentItem(EquipmentItemData itemData, EquipmentItemType equipmentItemType)
        {
            if (itemData != null)
            {
                equipmentPopupUI.ShowEquipment(itemData);
                reinforcePopupUI.ShowReinforce(itemData);
                equipmentListPopupUI.ShowEquipmentList(itemData);
            }
            else
            {
                equipmentPopupUI.ShowEquipment(equipmentItemType);
                equipmentListPopupUI.ShowEquipmentList(equipmentItemType);
            }
        }

        public void ReShow()
        {
            unitStatusUI.ShowStat(selectUnit);
            unitEquipmentUI.ShowEquipment(selectUnit);
            unitSkillPanelUI.ShowSkill(selectUnit);
            if (selectEquipmentItem != null)
            {
                equipmentPopupUI.ShowEquipment(selectEquipmentItem);
                reinforcePopupUI.ShowReinforce(selectEquipmentItem);
                equipmentListPopupUI.ShowEquipmentList(selectEquipmentItem);
            }
            else
            {
                equipmentPopupUI.ShowEquipment(selectEquipmentItemType);
                equipmentListPopupUI.ShowEquipmentList(selectEquipmentItemType);
            }
        }

        public void SelectUnit(Unit unit)
        {
            selectUnit = unit;
            ShowUnit(selectUnit);
        }

        public void SelectEquipmentItem(EquipmentItemData equipmentItemData, EquipmentItemType equipmentItemType)
        {
            selectEquipmentItem = equipmentItemData;
            selectEquipmentItemType = equipmentItemType;

            ShowEquipmentItem(selectEquipmentItem, selectEquipmentItemType);

        }
        public void ChoiceListEquipmentItem(EquipmentItemData equipmentItemData)
        {
            choiceEquipmentItem = equipmentItemData;

            ShowEquipmentItem(selectEquipmentItem, selectEquipmentItemType);
        }

        //===========================================================
        // ButtonPlugin
        //===========================================================
        public void ShowEquipmentUI()
        {
            if (LobbyManager.UIManager.UndoCount() >= 2)
            {
                LobbyManager.UIManager.Undo();
            }

            unitEquipmentUI.gameObject.SetActive(true);
        }

        public void ShowSkillUI()
        {
            if (LobbyManager.UIManager.UndoCount() >= 2)
            {
                LobbyManager.UIManager.Undo();
            }

            unitSkillPanelUI.gameObject.SetActive(true);
        }

        public void ShowEquipmentPopupUI()
        {
            equipmentPopupUI.gameObject.SetActive(true);
            reinforcePopupUI.gameObject.SetActive(false);
            equipmentListPopupUI.gameObject.SetActive(false);
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

        public void Reinforce()
        {
            GameManager.CurrentUser.userData.gold -= Constant.reinforceConsumeGoldValues[selectEquipmentItem.reinforceCount];

            if (Random.Range(0f, 1f) <= Constant.reinforceProbabilitys[selectEquipmentItem.reinforceCount])
            {
                GameManager.ItemCreator.ReinforceEquipment(selectEquipmentItem);
                ReShow();
            }

            LobbyManager.UIManager.ShowUserResource();
            GameManager.Instance.SaveUser();
        }

        public void ChoiceItem(ref bool isChoice, EquipmentItemData choiceData)
        {
            equipmentListPopupUI.UnChoiceList();
            isChoice = true;
            ChoiceListEquipmentItem(choiceData);
        }

        public void ReleaseEquipment()
        {
            GameManager.CurrentUser.userEquipmentItemDataList.Add(selectEquipmentItem);
            selectEquipmentItem = null;
            choiceEquipmentItem = null;

            switch (selectEquipmentItemType)
            {
                case EquipmentItemType.Weapon:
                    selectUnit.weaponData = null;
                    break;
                case EquipmentItemType.Helmet:
                    selectUnit.helmetData = null;
                    break;
                case EquipmentItemType.Armor:
                    selectUnit.armorData = null;
                    break;
                case EquipmentItemType.Amulet:
                    selectUnit.amuletData = null;
                    break;
                case EquipmentItemType.Ring:
                    selectUnit.ringData = null;
                    break;
                case EquipmentItemType.Shoe:
                    selectUnit.shoeData = null;
                    break;
            }

            equipmentListPopupUI.UnChoiceList();
            ReShow();
            GameManager.Instance.SaveUser();
        }

        public void ChangeEquipment()
        {
            GameManager.CurrentUser.userEquipmentItemDataList.Remove(choiceEquipmentItem);
            switch (selectEquipmentItemType)
            {
                case EquipmentItemType.Weapon:
                    selectUnit.weaponData = choiceEquipmentItem as WeaponData;
                    break;
                case EquipmentItemType.Helmet:
                    selectUnit.helmetData = choiceEquipmentItem as HelmetData;
                    break;
                case EquipmentItemType.Armor:
                    selectUnit.armorData = choiceEquipmentItem as ArmorData;
                    break;
                case EquipmentItemType.Amulet:
                    selectUnit.amuletData = choiceEquipmentItem as AmuletData;
                    break;
                case EquipmentItemType.Ring:
                    selectUnit.ringData = choiceEquipmentItem as RingData;
                    break;
                case EquipmentItemType.Shoe:
                    selectUnit.shoeData = choiceEquipmentItem as ShoeData;
                    break;
            }

            if (selectEquipmentItem != null)
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

        public void ShowExperiencePotionSlot()
        {
            potionSlotPanel.gameObject.SetActive(!potionSlotPanel.gameObject.activeInHierarchy);
        }
    }
}
