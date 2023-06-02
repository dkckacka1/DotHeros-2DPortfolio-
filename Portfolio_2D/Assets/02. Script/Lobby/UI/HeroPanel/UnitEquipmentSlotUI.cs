using System.Collections;
using System.Collections.Generic;
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
            }
        }
    }
}