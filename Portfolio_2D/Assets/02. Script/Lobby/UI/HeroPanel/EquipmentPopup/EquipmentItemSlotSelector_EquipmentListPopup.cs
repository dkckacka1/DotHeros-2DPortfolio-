using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ����â�� ��� ����Ʈ �˾�â�� ��� ���� UI ������ Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    [RequireComponent(typeof(EquipmentItemSlot))]
    public class EquipmentItemSlotSelector_EquipmentListPopup : MonoBehaviour
    {
        private EquipmentItemSlot equipmentSlot;

        [SerializeField] Image selectedImage;
        [SerializeField] Image impossibleSelectImage;

        private bool isSelect;
        private bool isSameEuqipmentType;

        public bool IsSelect
        {
            get => isSelect;
            set
            {
                isSelect = value;
                selectedImage.gameObject.SetActive(isSelect);
            }
        }

        public bool IsSameEuqipmentType
        {
            get => isSameEuqipmentType;
            set
            {
                isSameEuqipmentType = value;
                impossibleSelectImage.gameObject.SetActive(!isSameEuqipmentType);
            }
        }

        public EquipmentItemSlot EquipmentSlot { get => equipmentSlot; }

        public void Awake()
        {
            equipmentSlot = GetComponent<EquipmentItemSlot>();
        }

        // �����͸� ���� ���� �÷����� ������ �����ش�.
        public void TRIGGER_OnPointerEnter_ShowTooltip(EquipmentTooltip equipmentTooltipUI)
        {
            if (!isSameEuqipmentType) return;

            equipmentTooltipUI.ShowEquipmentTooltip(EquipmentSlot.EquipmentData);

            Vector2 tooltipPosition = new Vector2((this.transform as RectTransform).position.x, (this.transform as RectTransform).position.y);
            (equipmentTooltipUI.transform as RectTransform).position = tooltipPosition;
            Vector2 slotAnchoredPosition = new Vector2(-(this.transform as RectTransform).sizeDelta.x / 2, (this.transform as RectTransform).sizeDelta.y / 2);
            (equipmentTooltipUI.transform as RectTransform).anchoredPosition += slotAnchoredPosition;
            equipmentTooltipUI.gameObject.SetActive(true);
        }

        // �����͸� ���� ���� �÷����� ������ �����.
        public void TRIGGER_OnPointerExit_HideTooltip(EquipmentTooltip equipmentTooltipUI)
        {
            if (!isSameEuqipmentType) return;

            equipmentTooltipUI.gameObject.SetActive(false);
        }

        // ������ Ŭ�������� �����Ѵ�.
        public void TRIGGER_OnClick_ChoiceItem(EquipmentListPopupUI equipmentListPopupUI)
        {
            if (!isSameEuqipmentType) return;

            equipmentListPopupUI.ChoiceItem(this);

        }
    }
}