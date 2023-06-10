using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby.Inventory
{
    public class InventoryTooltip : MonoBehaviour
    {
        [SerializeField] EquipmentTooltip equipmentTooltipUI;
        [SerializeField] ConsumableItemTooltip consumableItem;


        public void ShowTooltip(ItemData data)
        {
            equipmentTooltipUI.gameObject.SetActive(data is EquipmentItemData);
            consumableItem.gameObject.SetActive(data is ConsumableItemData);

            if (data is EquipmentItemData)
            {
                ShowEquipmentTooltip(data as EquipmentItemData);
            }
            else if (data is ConsumableItemData)
            {
                ShowConsumableTooltip(data as ConsumableItemData);
            }
        }

        public void ShowEquipmentTooltip(EquipmentItemData data)
        {
            equipmentTooltipUI.ShowEquipmentTooltip(data);
        }

        public void ShowConsumableTooltip(ConsumableItemData data)
        {
            consumableItem.ShowConsumableTooltip(data);
        }
    }
}