using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 인벤토리에서 아이템 슬롯을 툴팁을 보여줄 셀렉터 클래스
 * 아이템 슬롯 컴포넌트가 부착된 오브젝트에만 부착할 수 있다.
 */

namespace Portfolio.Lobby.Inventory
{
    [RequireComponent(typeof(ItemSlotUI))]
    public class ItemSlotSelector_InventoryPanel : MonoBehaviour
    {
        ItemSlotUI itemSlotUI; // 아이템 슬롯 UI

        private void Awake()
        {
            itemSlotUI = GetComponent<ItemSlotUI>();
        }

        // 인벤토리에서 툴팁을 보여준다.
        public void TRIGGER_OnPointerEnter_ShowTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.ShowTooltip(itemSlotUI.ItemData, this.transform as RectTransform);
        }

        // 툴팁을 숨겨준다.
        public void TRIGGER_OnPointerExit_HideTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.HideTooltip();
        }
    }
}