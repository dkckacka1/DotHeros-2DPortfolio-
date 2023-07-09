using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �κ��丮���� ������ ������ ������ ������ ������ Ŭ����
 * ������ ���� ������Ʈ�� ������ ������Ʈ���� ������ �� �ִ�.
 */

namespace Portfolio.Lobby.Inventory
{
    [RequireComponent(typeof(ItemSlotUI))]
    public class ItemSlotSelector_InventoryPanel : MonoBehaviour
    {
        ItemSlotUI itemSlotUI; // ������ ���� UI

        private void Awake()
        {
            itemSlotUI = GetComponent<ItemSlotUI>();
        }

        // �κ��丮���� ������ �����ش�.
        public void TRIGGER_OnPointerEnter_ShowTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.ShowTooltip(itemSlotUI.ItemData, this.transform as RectTransform);
        }

        // ������ �����ش�.
        public void TRIGGER_OnPointerExit_HideTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.HideTooltip();
        }
    }
}