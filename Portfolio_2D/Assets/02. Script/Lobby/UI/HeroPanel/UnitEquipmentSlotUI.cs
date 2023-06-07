using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class UnitEquipmentSlotUI : MonoBehaviour
    {
        private EquipmentItemData currentEquipmentData;

        [SerializeField] EquipmentItemType equipmentItemType;
        [SerializeField] Button popupButton;
        [SerializeField] Image equipmentImage;
        [SerializeField] TextMeshProUGUI reinforceCountText;

        public EquipmentItemData EquipmentData { get => currentEquipmentData; }

        public void Init(EquipmentItemData equipmentData)
        {
            this.currentEquipmentData = equipmentData;
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

        public void SelcetEquipmentItemData(HeroPanelUI heroPanelUI)
        {
            heroPanelUI.SelectEquipmentItem(currentEquipmentData, equipmentItemType);
        }
    }
}