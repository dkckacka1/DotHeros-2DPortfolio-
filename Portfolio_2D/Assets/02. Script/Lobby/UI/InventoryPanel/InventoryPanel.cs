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

        private void Awake()
        {
            equipmentInventory.Init();
            consumableItemInventory.Init();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            equipmentInventory.gameObject.SetActive(true);
            consumableItemInventory.gameObject.SetActive(false);
        }

        public void ToggleShowEquipmentInventory(bool isActive)
        {
            equipmentInventory.gameObject.SetActive(isActive);
        }

        public void ToggleShowConsumableItemInventory(bool isActive)
        {
            consumableItemInventory.gameObject.SetActive(isActive);
        }
    }
}