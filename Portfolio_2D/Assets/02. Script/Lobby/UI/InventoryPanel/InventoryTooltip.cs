using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 인벤토리에서 아이템의 툴팁을 표시해주는 UI 클래스
 */

namespace Portfolio.Lobby.Inventory
{
    public class InventoryTooltip : MonoBehaviour
    {
        [SerializeField] EquipmentTooltip equipmentTooltipUI;               // 장비 아이템 툴팁
        [SerializeField] ConsumableItemTooltip consumableItemTooltipUI;     // 소비 아이템 툴팁

        // 들어온 아이템의 툴팁을 보여줍니다.
        public void ShowTooltip(ItemData data)
        {
            // 들어온 아이템 데이터가 어떤 데이터냐에 따라 표시할 툴팁을 정해줍니다.
            equipmentTooltipUI.gameObject.SetActive(data is EquipmentItemData);
            consumableItemTooltipUI.gameObject.SetActive(data is ConsumableItemData);

            if (data is EquipmentItemData)
            {
                ShowEquipmentTooltip(data as EquipmentItemData);
            }
            else if (data is ConsumableItemData)
            {
                ShowConsumableTooltip(data as ConsumableItemData);
            }
        }

        // 장비아이템 툴팁을 표시합니다.
        public void ShowEquipmentTooltip(EquipmentItemData data)
        {
            equipmentTooltipUI.ShowEquipmentTooltip(data);
        }

        // 소비아이템 툴팁을 표시합니다.
        public void ShowConsumableTooltip(ConsumableItemData data)
        {
            consumableItemTooltipUI.ShowConsumableTooltip(data);
        }
    }
}