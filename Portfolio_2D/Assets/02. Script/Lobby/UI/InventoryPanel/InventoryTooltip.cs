using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �κ��丮���� �������� ������ ǥ�����ִ� UI Ŭ����
 */

namespace Portfolio.Lobby.Inventory
{
    public class InventoryTooltip : MonoBehaviour
    {
        [SerializeField] EquipmentTooltip equipmentTooltipUI;               // ��� ������ ����
        [SerializeField] ConsumableItemTooltip consumableItemTooltipUI;     // �Һ� ������ ����

        // ���� �������� ������ �����ݴϴ�.
        public void ShowTooltip(ItemData data)
        {
            // ���� ������ �����Ͱ� � �����ͳĿ� ���� ǥ���� ������ �����ݴϴ�.
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

        // �������� ������ ǥ���մϴ�.
        public void ShowEquipmentTooltip(EquipmentItemData data)
        {
            equipmentTooltipUI.ShowEquipmentTooltip(data);
        }

        // �Һ������ ������ ǥ���մϴ�.
        public void ShowConsumableTooltip(ConsumableItemData data)
        {
            consumableItemTooltipUI.ShowConsumableTooltip(data);
        }
    }
}