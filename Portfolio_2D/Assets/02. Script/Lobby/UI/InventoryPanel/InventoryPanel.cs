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
        private Vector2 pos;

        private void Awake()
        {
            equipmentInventory.Init();
            consumableItemInventory.Init();
        }

        private void Start()
        {
            pos = equipmentInventory.GetScrollViewMiddlePoint();
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

            LayoutRebuilder.ForceRebuildLayoutImmediate(tooltip.transform as RectTransform);

            float tooltipPivotX = 0;
            float tooltipPivotY = 0;
            float deltaX = 0;
            float deltaY = 0;
            if (slotTransform.position.x <= pos.x)
            {
                //Debug.Log("슬롯은 왼쪽에 있습니다.");
                tooltipPivotX = 0;
                deltaX = slotTransform.sizeDelta.x / 2;
            }
            else
            {
                //Debug.Log("슬롯은 오른쪽에 있습니다.");
                tooltipPivotX = 1;
                deltaX = slotTransform.sizeDelta.x / 2 * -1;
            }

            if (slotTransform.position.y <= pos.y)
            {
                //Debug.Log("슬롯은 하단에 있습니다.");
                tooltipPivotY = 0;
                deltaY = slotTransform.sizeDelta.y / 2 * -1;
            }
            else
            {
                //Debug.Log("슬롯은 상단에 있습니다.");
                tooltipPivotY = 1;
                deltaY = slotTransform.sizeDelta.y / 2;
            }

            (tooltip.transform as RectTransform).pivot = new Vector2(tooltipPivotX, tooltipPivotY);

            Vector2 tooltipPosition = new Vector2(slotTransform.position.x, slotTransform.position.y);
            (tooltip.transform as RectTransform).position = tooltipPosition;
            (tooltip.transform as RectTransform).anchoredPosition += new Vector2(deltaX, deltaY);

            Vector3[] corners = new Vector3[4];
            (tooltip.transform as RectTransform).GetWorldCorners(corners);
            //Debug.Log((slotTransform as RectTransform).position);

            if (corners[0].y < 0)
            {
                float plusYValue = Mathf.Abs(corners[0].y);
                (tooltip.transform as RectTransform).anchoredPosition += new Vector2(0, plusYValue);
            }
        }

        public void HideTooltip()
        {
            tooltip.gameObject.SetActive(false);
        }
    }
}