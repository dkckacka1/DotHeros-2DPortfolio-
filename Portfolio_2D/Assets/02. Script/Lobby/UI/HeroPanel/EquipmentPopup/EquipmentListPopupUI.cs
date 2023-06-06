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
        [SerializeField] Button equipmentChangeBtn;
        [SerializeField] TextMeshProUGUI notingText;

        public void Init()
        {
            equipmentChangeBtn.interactable = false;
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