using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ����â�� ��� ����Ʈ �˾�â�� ��� ���� UI ������ Ŭ����
 * ��� ���� UI�� ������ ������Ʈ���� ������ �� �ִ�.
 */

namespace Portfolio.Lobby.Hero
{
    [RequireComponent(typeof(EquipmentItemSlot))]
    public class EquipmentItemSlotSelector_EquipmentListPopup : MonoBehaviour
    {
        private EquipmentItemSlot equipmentSlot;        // ��� ���� UI

        [SerializeField] Image selectedImage;           // ��� ���� �̹���
        [SerializeField] Image impossibleSelectImage;   // ���� �Ұ� �׸��� �̹���

        private bool isSelect;              // ���� �Ǿ����� ����
        private bool isSameEuqipmentType;   // ���� ��� ������ �Ǵ� Ÿ���� ���� ��� ���԰� �������� ����

        public bool IsSelect
        {
            get => isSelect;
            set
            {
                // ���� ���ο� ���� ���� �̹��� ���
                isSelect = value;
                selectedImage.gameObject.SetActive(isSelect);
            }
        }

        public bool IsSameEuqipmentType
        {
            get => isSameEuqipmentType;
            set
            {
                // ���� Ÿ������ ���ο� ���� ���� �Ұ� �׸��� �̹��� ���
                isSameEuqipmentType = value;
                impossibleSelectImage.gameObject.SetActive(!isSameEuqipmentType);
            }
        }

        // ���� ��� ���� ��ȯ
        public EquipmentItemSlot EquipmentSlot { get => equipmentSlot; }

        public void Awake()
        {
            equipmentSlot = GetComponent<EquipmentItemSlot>();
        }

        // �����͸� ���� ���� �÷����� ������ �����ش�.
        public void TRIGGER_OnPointerEnter_ShowTooltip(EquipmentTooltip equipmentTooltipUI)
        {
            if (!isSameEuqipmentType) return;

            // ��� ������ �����͸� �ִ´�.
            equipmentTooltipUI.ShowEquipmentTooltip(EquipmentSlot.EquipmentData);

            // ���� ��ġ�� �ڽ��� ������� �̵��ϰ� �Ѵ�.
            Vector2 tooltipPosition = new Vector2((this.transform as RectTransform).position.x, (this.transform as RectTransform).position.y);
            (equipmentTooltipUI.transform as RectTransform).position = tooltipPosition;

            // ������ ���� ��ܿ� ��ġ�ϵ��� �ڽ� ���� ������ ����, ���α����� ������ŭ �߰��� �̵� �����ش�.
            Vector2 slotAnchoredPosition = new Vector2(-(this.transform as RectTransform).sizeDelta.x / 2, (this.transform as RectTransform).sizeDelta.y / 2);
            (equipmentTooltipUI.transform as RectTransform).anchoredPosition += slotAnchoredPosition;

            // ������ �����ش�.
            equipmentTooltipUI.gameObject.SetActive(true);
        }

        // �����͸� ���� ���� �÷����� ������ �����.
        public void TRIGGER_OnPointerExit_HideTooltip(EquipmentTooltip equipmentTooltipUI)
        {
            // ���� ��� Ÿ���� ���� ����
            if (!isSameEuqipmentType) return;

            equipmentTooltipUI.gameObject.SetActive(false);
        }

        // ������ Ŭ�������� �����Ѵ�.
        public void TRIGGER_OnClick_ChoiceItem(EquipmentListPopupUI equipmentListPopupUI)
        {
            // ���� ��� Ÿ���� ���� ����
            if (!isSameEuqipmentType) return;

            equipmentListPopupUI.ChoiceItem(this);

        }
    }
}