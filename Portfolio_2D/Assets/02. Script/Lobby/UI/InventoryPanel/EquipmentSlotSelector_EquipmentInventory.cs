using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 인벤토리 패널에서 장비 슬롯 UI와 상호작용 하는 클래스
 * UnitEquipmentSlotUI 컴포넌트가 부착된 오브젝트에만 부작할 수 있다.
 */

namespace Portfolio.Lobby.Inventory
{
    [RequireComponent(typeof(EquipmentItemSlot))]
    public class EquipmentSlotSelector_EquipmentInventory : MonoBehaviour
    {
        [SerializeField] Image userSelectImage; // 유저가 선택했을 때 표시할 이미지
        private bool isSelect; // 유저가 선택했는지 여부
        [HideInInspector] public EquipmentItemSlot unitEquipmentSlotUI;    // 장비 아이템 슬롯

        private void Awake()
        {
            unitEquipmentSlotUI = GetComponent<EquipmentItemSlot>();
        }

        public bool IsSelect 
        {
            get => isSelect; 
            set
            {
                isSelect = value;
                // 선택 여부에 따라 이미지 출력
                userSelectImage.gameObject.SetActive(isSelect);
            }
        }

        // 이 장비 슬롯 선택한 것을 장비 인벤토리에 알려준다.
        public void TRIGGER_OnClick_EquipmentItemSelect(EquipmentInventory equipmentInventory)
        {
            equipmentInventory.EquipmentSlotSelect(this);
        }

        // 인벤토리 패널에서 툴팁을 보여준다.
        public void TRIGGER_OnPointerEnter_ShowTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.ShowTooltip(unitEquipmentSlotUI.EquipmentData, transform as RectTransform);
        }

        // 인벤토리 패널에서 툴팁을 숨긴다.
        public void TRIGGER_OnPointerExit_HideTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.HideTooltip();
        }
    }
}