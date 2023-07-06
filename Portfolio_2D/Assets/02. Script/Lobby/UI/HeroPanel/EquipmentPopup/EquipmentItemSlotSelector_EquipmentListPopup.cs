using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 영웅창의 장비 리스트 팝업창의 장비 슬롯 UI 셀렉터 클래스
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

        // 포인터를 슬롯 위에 올렸을때 툴팁을 보여준다.
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

        // 포인터를 슬롯 위에 올렸을때 툴팁을 숨긴다.
        public void TRIGGER_OnPointerExit_HideTooltip(EquipmentTooltip equipmentTooltipUI)
        {
            if (!isSameEuqipmentType) return;

            equipmentTooltipUI.gameObject.SetActive(false);
        }

        // 슬롯을 클릭했을때 선택한다.
        public void TRIGGER_OnClick_ChoiceItem(EquipmentListPopupUI equipmentListPopupUI)
        {
            if (!isSameEuqipmentType) return;

            equipmentListPopupUI.ChoiceItem(this);

        }
    }
}