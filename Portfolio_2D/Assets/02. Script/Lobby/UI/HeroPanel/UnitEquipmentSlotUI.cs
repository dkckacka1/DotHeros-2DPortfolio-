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
        private EquipmentItemType equipmentItemType;

        [SerializeField] Button popupButton;
        [SerializeField] Image equipmentImage;
        [SerializeField] TextMeshProUGUI reinforceCountText;

        public void Init(EquipmentItemData equipmentData)
        {
            if (equipmentData == null)
            {
                popupButton.interactable = false;
                equipmentImage.gameObject.SetActive(false);
            }
            else
            {
                popupButton.interactable = true;
                equipmentImage.gameObject.SetActive(true);
                this.equipmentData = equipmentData;
                this.equipmentItemType = equipmentData.equipmentType;
                reinforceCountText.gameObject.SetActive(equipmentData.reinforceCount != 0);
                reinforceCountText.text = $"+{equipmentData.reinforceCount}";
            }
        }

        public void ShowEquipmentPopup(EquipmentPopupUI popupUI)
        {
            if (equipmentData == null) return;

            popupUI.Init(equipmentData);
            popupUI.gameObject.SetActive(true);
        }
    }
}