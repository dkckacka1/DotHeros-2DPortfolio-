using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 인벤토리들을 관리하는 패널 UI 클래스
 */

namespace Portfolio.Lobby.Inventory
{
    public class InventoryPanel : PanelUI
    {
        [SerializeField] EquipmentInventory equipmentInventory;             // 장비 아이템 인벤토리
        [SerializeField] ConsumableItemInventory consumableItemInventory;   // 소비 아이템 인벤토리
        [SerializeField] Toggle equipmentInventoryToggle;                   // 장비 인벤토리 토글
        [SerializeField] Toggle consumableItemInventoryToggle;              // 소비 인벤토리 토글
        [SerializeField] InventoryTooltip tooltip;                          // 툴팁
        private Vector2 pos;

        private void Awake()
        {
            // 인벤토리를 초기화 합니다
            equipmentInventory.Init();
            consumableItemInventory.Init();
        }

        private void Start()
        {
            // 스크롤 뷰의 중앙 좌표를 얻습니다.
            pos = equipmentInventory.GetScrollViewMiddlePoint();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        // 창이 꺼질 때 패널을 초기화 합니다.
        private void OnDisable()
        {
            equipmentInventoryToggle.isOn = true;
            equipmentInventoryToggle.Select();
            equipmentInventory.gameObject.SetActive(true);
            consumableItemInventoryToggle.isOn = false;
            consumableItemInventory.gameObject.SetActive(false);
            tooltip.gameObject.SetActive(false);
        }

        // 장비 인벤토리를 보여줍니다.
        public void TOGGLE_OnValueChanged_ShowEquipmentInventory(bool isActive)
        {
            equipmentInventory.gameObject.SetActive(isActive);
        }

        // 소비 아이템 인벤토리를 보여줍니다.
        public void TOGGLE_OnValueChanged_ShowConsumableItemInventory(bool isActive)
        {
            consumableItemInventory.gameObject.SetActive(isActive);
        }

        // 툴팁을 보여줍니다.
        public void ShowTooltip(ItemData data, RectTransform slotTransform)
        {
            tooltip.gameObject.SetActive(true);
            // 툴팁의 데이터를 표시해줍니다.
            tooltip.ShowTooltip(data);
            // 툴팁의 정보에 따라 툴팁의 크기를 재조정합니다.
            LayoutRebuilder.ForceRebuildLayoutImmediate(tooltip.transform as RectTransform);

            // 툴팁 위치를 조정합니다.
            float tooltipPivotX = 0;
            float tooltipPivotY = 0;
            float deltaX = 0;
            float deltaY = 0;
            // 현재 슬롯이 스크롤뷰의 어디에 위치하냐에 따라 툴팁 위치와 피봇을 조정합니다.
            if (slotTransform.position.x <= pos.x)
                // 슬롯이 스크롤뷰 좌측에 있다면
            {
                // 툴팁을 슬롯의 우측에 표시하도록 합니다.
                tooltipPivotX = 0;
                deltaX = slotTransform.sizeDelta.x / 2;
            }
            else
                // 슬롯이 스크롤뷰 우측에 있다면
            {
                // 툴팁을 슬롯의 좌측에 표시하도록 합니다.
                tooltipPivotX = 1;
                deltaX = slotTransform.sizeDelta.x / 2 * -1;
            }

            if (slotTransform.position.y <= pos.y)
                // 슬롯이 스크롤뷰의 하단에 있다면
            {
                // 툴팁을 슬롯의 하단에 자리잡게 합니다.
                tooltipPivotY = 0;
                deltaY = slotTransform.sizeDelta.y / 2 * -1;
            }
            else
                // 툴팁이 스크롤뷰의 상단에 있다면
            {
                // 툴팁을 슬롯의 상단에 자리잡게 합니다.
                tooltipPivotY = 1;
                deltaY = slotTransform.sizeDelta.y / 2;
            }

            // 슬롯의 위치에 따라 툴팁의 피봇값을 조정합니다.
            (tooltip.transform as RectTransform).pivot = new Vector2(tooltipPivotX, tooltipPivotY);

            // 슬롯의 중앙 위치값에 자리잡게 한 후 슬롯의 가로 세로 크기만큼 추가 이동합니다.
            Vector2 tooltipPosition = new Vector2(slotTransform.position.x, slotTransform.position.y);
            (tooltip.transform as RectTransform).position = tooltipPosition;
            (tooltip.transform as RectTransform).anchoredPosition += new Vector2(deltaX, deltaY);

            Vector3[] corners = new Vector3[4];
            (tooltip.transform as RectTransform).GetWorldCorners(corners);
            if (corners[0].y < 0)
                // 만약 툴팁의 좌측하단 좌표값이 스크린 밑으로 내려간다면
            {
                // 스크린 안에 표시될 수 있도록 y의 절대값만큼 더해줍니다.
                float plusYValue = Mathf.Abs(corners[0].y);
                (tooltip.transform as RectTransform).anchoredPosition += new Vector2(0, plusYValue);
            }
        }

        // 툴팁을 숨겨줍니다.
        public void HideTooltip()
        {
            tooltip.gameObject.SetActive(false);
        }
    }
}