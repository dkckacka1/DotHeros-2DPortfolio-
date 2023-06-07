using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Portfolio.Lobby
{
    public class EquipmentListPopupUI : MonoBehaviour
    {
        EquipmentItemData selectedData;
        EquipmentItemData listSelectedData;

        [SerializeField] List<UnitEquipmentSlotUI> equipmentSlotList;
        [SerializeField] ScrollRect equipmentListScrollView;
        [SerializeField] EquipmentTooltip equipmentTooltipUI;
        [SerializeField] Button equipmentChangeBtn;
        [SerializeField] TextMeshProUGUI notingText;

        private void Awake()
        {
            equipmentSlotList = new List<UnitEquipmentSlotUI>();
            foreach (var slot in equipmentListScrollView.content.GetComponentsInChildren<UnitEquipmentSlotUI>())
            {
                equipmentSlotList.Add(slot);
            }
            //Debug.Log(equipmentSlotList.Count);
        }

        public void Init(EquipmentItemData equipmentData)
        {
            this.selectedData = equipmentData;
            equipmentChangeBtn.interactable = false;
            equipmentTooltipUI.gameObject.SetActive(false);
            ShowEquipmentList();
        }

        public void ShowEquipmentList()
        {
            Debug.Log(GameManager.CurrentUser.userEquipmentItemDataList.Count);
            Debug.Log(selectedData == null);
            Debug.Log(selectedData.equipmentType);

            var listOrdered = (from item in GameManager.CurrentUser.userEquipmentItemDataList
                        orderby (item.equipmentType == selectedData.equipmentType) descending
                        select item)
                        .ToList();


            foreach (var item in listOrdered)
            {
                Debug.Log(item.equipmentType);
            }

            //Debug.Log(equipmentSlotList.Count);

            for (int i = 0; i < equipmentSlotList.Count; i++)
            {
                if (listOrdered.Count <= i)
                {
                    equipmentSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                var equipmentData = listOrdered[i];
                equipmentSlotList[i].Init(equipmentData);
                equipmentSlotList[i].gameObject.SetActive(true);
                equipmentSlotList[i].GetComponent<EquipmentSelectUI>().Init(selectedData);
            }

            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }
    }
}