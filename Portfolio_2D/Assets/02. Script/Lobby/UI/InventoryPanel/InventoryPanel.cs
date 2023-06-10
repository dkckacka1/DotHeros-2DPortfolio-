using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Inventory
{
    public class InventoryPanel : PanelUI
    {
        [SerializeField] EquipmentInventory equipmentInventory;
        [SerializeField] ConsumableItemInventory consumableItemInventory;
        [SerializeField] Toggle equipmentInventoryToggle;
        [SerializeField] Toggle consumableItemInventoryToggle;
        [SerializeField] InventoryTooltip tooltip;
        private void Awake()
        {
            equipmentInventory.Init();
            consumableItemInventory.Init();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            equipmentInventoryToggle.isOn = true;
            equipmentInventoryToggle.Select();
            equipmentInventory.gameObject.SetActive(true);
            consumableItemInventoryToggle.isOn = false;
            consumableItemInventory.gameObject.SetActive(false);
            tooltip.gameObject.SetActive(false);
        }

        public void ToggleShowEquipmentInventory(bool isActive)
        {
            equipmentInventory.gameObject.SetActive(isActive);
        }

        public void ToggleShowConsumableItemInventory(bool isActive)
        {
            consumableItemInventory.gameObject.SetActive(isActive);
        }

        public void ShowTooltip(ItemData data, RectTransform slotTransform)
        {
            tooltip.gameObject.SetActive(true);
            tooltip.ShowTooltip(data);
            Debug.Log((tooltip.transform as RectTransform).anchoredPosition);
        }

        public void HideTooltip()
        {
            tooltip.gameObject.SetActive(false);
        }
    }
}