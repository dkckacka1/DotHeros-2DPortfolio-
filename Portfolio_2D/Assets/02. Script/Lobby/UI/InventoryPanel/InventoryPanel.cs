using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * �κ��丮���� �����ϴ� �г� UI Ŭ����
 */

namespace Portfolio.Lobby.Inventory
{
    public class InventoryPanel : PanelUI
    {
        [SerializeField] EquipmentInventory equipmentInventory;             // ��� ������ �κ��丮
        [SerializeField] ConsumableItemInventory consumableItemInventory;   // �Һ� ������ �κ��丮
        [SerializeField] Toggle equipmentInventoryToggle;                   // ��� �κ��丮 ���
        [SerializeField] Toggle consumableItemInventoryToggle;              // �Һ� �κ��丮 ���
        [SerializeField] InventoryTooltip tooltip;                          // ����
        private Vector2 pos;

        private void Awake()
        {
            // �κ��丮�� �ʱ�ȭ �մϴ�
            equipmentInventory.Init();
            consumableItemInventory.Init();
        }

        private void Start()
        {
            // ��ũ�� ���� �߾� ��ǥ�� ����ϴ�.
            pos = equipmentInventory.GetScrollViewMiddlePoint();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        // â�� ���� �� �г��� �ʱ�ȭ �մϴ�.
        private void OnDisable()
        {
            equipmentInventoryToggle.isOn = true;
            equipmentInventoryToggle.Select();
            equipmentInventory.gameObject.SetActive(true);
            consumableItemInventoryToggle.isOn = false;
            consumableItemInventory.gameObject.SetActive(false);
            tooltip.gameObject.SetActive(false);
        }

        // ��� �κ��丮�� �����ݴϴ�.
        public void TOGGLE_OnValueChanged_ShowEquipmentInventory(bool isActive)
        {
            equipmentInventory.gameObject.SetActive(isActive);
        }

        // �Һ� ������ �κ��丮�� �����ݴϴ�.
        public void TOGGLE_OnValueChanged_ShowConsumableItemInventory(bool isActive)
        {
            consumableItemInventory.gameObject.SetActive(isActive);
        }

        // ������ �����ݴϴ�.
        public void ShowTooltip(ItemData data, RectTransform slotTransform)
        {
            tooltip.gameObject.SetActive(true);
            // ������ �����͸� ǥ�����ݴϴ�.
            tooltip.ShowTooltip(data);
            // ������ ������ ���� ������ ũ�⸦ �������մϴ�.
            LayoutRebuilder.ForceRebuildLayoutImmediate(tooltip.transform as RectTransform);

            // ���� ��ġ�� �����մϴ�.
            float tooltipPivotX = 0;
            float tooltipPivotY = 0;
            float deltaX = 0;
            float deltaY = 0;
            // ���� ������ ��ũ�Ѻ��� ��� ��ġ�ϳĿ� ���� ���� ��ġ�� �Ǻ��� �����մϴ�.
            if (slotTransform.position.x <= pos.x)
                // ������ ��ũ�Ѻ� ������ �ִٸ�
            {
                // ������ ������ ������ ǥ���ϵ��� �մϴ�.
                tooltipPivotX = 0;
                deltaX = slotTransform.sizeDelta.x / 2;
            }
            else
                // ������ ��ũ�Ѻ� ������ �ִٸ�
            {
                // ������ ������ ������ ǥ���ϵ��� �մϴ�.
                tooltipPivotX = 1;
                deltaX = slotTransform.sizeDelta.x / 2 * -1;
            }

            if (slotTransform.position.y <= pos.y)
                // ������ ��ũ�Ѻ��� �ϴܿ� �ִٸ�
            {
                // ������ ������ �ϴܿ� �ڸ���� �մϴ�.
                tooltipPivotY = 0;
                deltaY = slotTransform.sizeDelta.y / 2 * -1;
            }
            else
                // ������ ��ũ�Ѻ��� ��ܿ� �ִٸ�
            {
                // ������ ������ ��ܿ� �ڸ���� �մϴ�.
                tooltipPivotY = 1;
                deltaY = slotTransform.sizeDelta.y / 2;
            }

            // ������ ��ġ�� ���� ������ �Ǻ����� �����մϴ�.
            (tooltip.transform as RectTransform).pivot = new Vector2(tooltipPivotX, tooltipPivotY);

            // ������ �߾� ��ġ���� �ڸ���� �� �� ������ ���� ���� ũ�⸸ŭ �߰� �̵��մϴ�.
            Vector2 tooltipPosition = new Vector2(slotTransform.position.x, slotTransform.position.y);
            (tooltip.transform as RectTransform).position = tooltipPosition;
            (tooltip.transform as RectTransform).anchoredPosition += new Vector2(deltaX, deltaY);

            Vector3[] corners = new Vector3[4];
            (tooltip.transform as RectTransform).GetWorldCorners(corners);
            if (corners[0].y < 0)
                // ���� ������ �����ϴ� ��ǥ���� ��ũ�� ������ �������ٸ�
            {
                // ��ũ�� �ȿ� ǥ�õ� �� �ֵ��� y�� ���밪��ŭ �����ݴϴ�.
                float plusYValue = Mathf.Abs(corners[0].y);
                (tooltip.transform as RectTransform).anchoredPosition += new Vector2(0, plusYValue);
            }
        }

        // ������ �����ݴϴ�.
        public void HideTooltip()
        {
            tooltip.gameObject.SetActive(false);
        }
    }
}