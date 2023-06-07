using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby
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

        Unit selectUnit;
        EquipmentItemData selectEquipmentItem;
        EquipmentItemData selectedNewEquipmentItem;

        private void Start()
        {
            selectUnit = GameManager.CurrentUser.userUnitList[0];
            LobbyManager.Instance.userSelectedUnit = selectUnit;
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
        }
        public void ShowUnit(Unit unit)
        {
            unitStatusUI.ShowStat(unit);
            unitEquipmentUI.ShowEquipment(unit);
            unitSkillPanelUI.ShowSkill(unit);
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
            equipmentPopupUI.ShowEquipment(selectEquipmentItem);
            reinforcePopupUI.ShowReinforce(selectEquipmentItem);
            equipmentListPopupUI.ShowEquipmentList(selectEquipmentItem);

        }

        public void SelectUnit(Unit unit)
        {
            selectUnit = unit;
            ShowUnit(selectUnit);
        }

        public void SelectEquipmentItem(EquipmentItemData equipmentItemData, EquipmentItemType equipmentItemType)
        {
            selectEquipmentItem = equipmentItemData;

            ShowEquipmentItem(selectEquipmentItem, equipmentItemType);

        }
        public void SelectListEquipmentItem(EquipmentItemData equipmentItemData)
        {
            selectedNewEquipmentItem = equipmentItemData;
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

        public void ChoiceItem()
        {
        }

        //public void ReleaseEquipment()
        //{
        //    LobbyManager.Instance.userSelectedUnit.ReleaseEquipment(selectedType);
        //    LobbyManager.UIManager.ReShowPanel();
        //}
    }
}
