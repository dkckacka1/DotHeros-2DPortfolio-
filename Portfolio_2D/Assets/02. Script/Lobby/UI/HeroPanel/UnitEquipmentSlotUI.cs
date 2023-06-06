using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class UnitEquipmentSlotUI : MonoBehaviour
    {
        private EquipmentItemData equipmentData;

        [SerializeField] EquipmentItemType equipmentItemType;
        [SerializeField] Button popupButton;
        [SerializeField] Image equipmentImage;
        [SerializeField] TextMeshProUGUI reinforceCountText;

        public void Init(EquipmentItemData equipmentData)
        {
            this.equipmentData = equipmentData;
            if (equipmentData != null)
            {
                equipmentImage.gameObject.SetActive(true);
                reinforceCountText.gameObject.SetActive(equipmentData.reinforceCount != 0);
                reinforceCountText.text = $"+{equipmentData.reinforceCount}";
            }
            else
            {
                equipmentImage.gameObject.SetActive(false);
                reinforceCountText.gameObject.SetActive(false);
            }
        }

        public void ShowEquipmentPopup(EquipmentPopupUI popupUI)
        {
            popupUI.Init(equipmentData, equipmentItemType);
            popupUI.gameObject.SetActive(true);
        }
    }
}