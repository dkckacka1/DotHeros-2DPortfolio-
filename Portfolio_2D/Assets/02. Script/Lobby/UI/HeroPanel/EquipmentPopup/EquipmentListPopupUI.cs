using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class EquipmentListPopupUI : MonoBehaviour
    {
        EquipmentItemData selectData;

        [SerializeField] List<UnitEquipmentSlotUI> equipmentSlotList;
        [SerializeField] EquipmentTooltip equipmentTooltipUI;
        [SerializeField] Button equipmentChangeBtn;
        [SerializeField] TextMeshProUGUI notingText;

        private void Awake()
        {
            equipmentSlotList = new List<UnitEquipmentSlotUI>();
            foreach (var slot in GetComponentsInChildren<UnitEquipmentSlotUI>())
            {
                equipmentSlotList.Add(slot);
            }
        }

        public void Init()
        {
            equipmentChangeBtn.interactable = false;
            equipmentTooltipUI.gameObject.SetActive(false);
            ShowEquipmentList();
        }

        public void ShowEquipmentList()
        {
            var userEquipmentList = GameManager.CurrentUser.userData.equipmentItemDataList;

            for (int i = 0; i < equipmentSlotList.Count; i++)
            {
                if (userEquipmentList.Count <= i)
                {
                    equipmentSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                var equipmentData = userEquipmentList[i];
                equipmentSlotList[i].Init(equipmentData);
                equipmentSlotList[i].gameObject.SetActive(true);
            }

            notingText.gameObject.SetActive(userEquipmentList.Count == 0);
        }
    }
}