using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Portfolio.UI;

namespace Portfolio.Lobby.Hero
{
    public class EquipmentListPopupUI : MonoBehaviour
    {
        [SerializeField] List<UnitEquipmentSlotUI> equipmentSlotList;
        [SerializeField] ScrollRect equipmentListScrollView;
        [SerializeField] EquipmentTooltip equipmentTooltipUI;
        [SerializeField] Button equipmentChangeBtn;
        [SerializeField] Button equipmentDiscardBtn;
        [SerializeField] TextMeshProUGUI notingText;

        public void Init()
        {
            equipmentSlotList = new List<UnitEquipmentSlotUI>();
            foreach (var slot in equipmentListScrollView.content.GetComponentsInChildren<UnitEquipmentSlotUI>())
            {
                equipmentSlotList.Add(slot);
                slot.GetComponent<EquipmentSelectUI>().Init();
            }
            LobbyManager.UIManager.equipmentItemDataChangeEvent += ShowList;
        }

        public void ShowList(object sender, EventArgs eventArgs)
        {
            if (HeroPanelUI.SelectEquipmentItem != null)
            {
                ShowEquipmentList(HeroPanelUI.SelectEquipmentItem);
            }
            else
            {
                ShowEquipmentList(HeroPanelUI.SelectEquipmentItemType);
            }
        }

        private void OnEnable()
        {
            UnChoiceList();
        }

        public void ShowEquipmentList(EquipmentItemData equipmentItemData)
        {
            var listOrdered = (from item in GameManager.CurrentUser.userEquipmentItemDataList
                                orderby (item.equipmentType == equipmentItemData.equipmentType) descending
                                select item)
                                .ToList();

            for (int i = 0; i < equipmentSlotList.Count; i++)
            {
                if (listOrdered.Count <= i)
                {
                    equipmentSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                var equipmentData = listOrdered[i];
                equipmentSlotList[i].ShowEquipment(equipmentData);
                equipmentSlotList[i].gameObject.SetActive(true);
                var selectedUI = equipmentSlotList[i].GetComponent<EquipmentSelectUI>();
                if (selectedUI.isChoice)
                {
                    selectedUI.ShowSelectedUI();
                    equipmentChangeBtn.interactable = true;
                }
                else
                {
                    selectedUI.HideSelectedUI();
                }
                selectedUI.ShowImpossibleSelectImage(equipmentItemData);
            }

            equipmentDiscardBtn.interactable = true;
            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }

        public void ShowEquipmentList(EquipmentItemType itemType)
        {
            var listOrdered = (from item in GameManager.CurrentUser.userEquipmentItemDataList
                               orderby (item.equipmentType == itemType) descending
                               select item)
                        .ToList();

            for (int i = 0; i < equipmentSlotList.Count; i++)
            {
                if (listOrdered.Count <= i)
                {
                    equipmentSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                var equipmentData = listOrdered[i];
                equipmentSlotList[i].ShowEquipment(equipmentData);
                equipmentSlotList[i].gameObject.SetActive(true);
                var selectedUI = equipmentSlotList[i].GetComponent<EquipmentSelectUI>();
                if (selectedUI.isChoice)
                {
                    selectedUI.ShowSelectedUI();
                    equipmentChangeBtn.interactable = true;
                }
                else
                {
                    selectedUI.HideSelectedUI();
                }
                selectedUI.ShowImpossibleSelectImage(itemType);
            }

            equipmentDiscardBtn.interactable = false;
            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }

        public void UnChoiceList()
        {
            foreach (var slot in equipmentSlotList)
            {
                slot.GetComponent<EquipmentSelectUI>().isChoice = false;
            }
            equipmentChangeBtn.interactable = false;
        }
    }
}