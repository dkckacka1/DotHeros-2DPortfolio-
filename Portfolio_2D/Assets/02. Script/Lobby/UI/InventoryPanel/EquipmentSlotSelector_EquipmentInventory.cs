using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * �κ��丮 �гο��� ��� ���� UI�� ��ȣ�ۿ� �ϴ� Ŭ����
 * UnitEquipmentSlotUI ������Ʈ�� ������ ������Ʈ���� ������ �� �ִ�.
 */

namespace Portfolio.Lobby.Inventory
{
    [RequireComponent(typeof(EquipmentItemSlot))]
    public class EquipmentSlotSelector_EquipmentInventory : MonoBehaviour
    {
        [SerializeField] Image userSelectImage; // ������ �������� �� ǥ���� �̹���
        private bool isSelect; // ������ �����ߴ��� ����
        [HideInInspector] public EquipmentItemSlot unitEquipmentSlotUI;    // ��� ������ ����

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
                // ���� ���ο� ���� �̹��� ���
                userSelectImage.gameObject.SetActive(isSelect);
            }
        }

        // �� ��� ���� ������ ���� ��� �κ��丮�� �˷��ش�.
        public void TRIGGER_OnClick_EquipmentItemSelect(EquipmentInventory equipmentInventory)
        {
            equipmentInventory.EquipmentSlotSelect(this);
        }

        // �κ��丮 �гο��� ������ �����ش�.
        public void TRIGGER_OnPointerEnter_ShowTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.ShowTooltip(unitEquipmentSlotUI.EquipmentData, transform as RectTransform);
        }

        // �κ��丮 �гο��� ������ �����.
        public void TRIGGER_OnPointerExit_HideTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.HideTooltip();
        }
    }
}