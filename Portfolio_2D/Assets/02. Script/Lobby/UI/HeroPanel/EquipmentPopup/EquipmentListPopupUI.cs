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

        public void ShowEquipmentList(EquipmentItemData equipmentItemData)
        {
            if (equipmentItemData == null) return;

            var listOrdered = (from item in GameManager.CurrentUser.userEquipmentItemDataList
                        orderby (item.equipmentType == equipmentItemData.equipmentType) descending
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
                var selectedUI = equipmentSlotList[i].GetComponent<EquipmentSelectUI>();
                selectedUI.Init(equipmentItemData);
                //selectedUI.HideSelectedUI();
            }

            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }

        public void ShowEquipmentList(EquipmentItemType itemType)
        {
            var listOrdered = (from item in GameManager.CurrentUser.userEquipmentItemDataList
                               orderby (item.equipmentType == itemType) descending
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
                var selectedUI = equipmentSlotList[i].GetComponent<EquipmentSelectUI>();
                selectedUI.Init(itemType);
                //selectedUI.HideSelectedUI();
            }

            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }
    }
}