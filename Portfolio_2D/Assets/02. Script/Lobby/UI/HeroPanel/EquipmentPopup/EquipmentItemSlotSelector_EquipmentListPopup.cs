using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 영웅창의 장비 리스트 팝업창의 장비 슬롯 UI 셀렉터 클래스
 * 장비 슬롯 UI가 부착된 오브젝트에만 부착할 수 있다.
 */

namespace Portfolio.Lobby.Hero
{
    [RequireComponent(typeof(EquipmentItemSlot))]
    public class EquipmentItemSlotSelector_EquipmentListPopup : MonoBehaviour
    {
        private EquipmentItemSlot equipmentSlot;        // 장비 슬롯 UI

        [SerializeField] Image selectedImage;           // 장비 선택 이미지
        [SerializeField] Image impossibleSelectImage;   // 선택 불가 그림자 이미지

        private bool isSelect;              // 선택 되었는지 여부
        private bool isSameEuqipmentType;   // 들어온 장비 데이터 또는 타입이 현재 장비 슬롯과 동일한지 여부

        public bool IsSelect
        {
            get => isSelect;
            set
            {
                // 선택 여부에 따라 선택 이미지 출력
                isSelect = value;
                selectedImage.gameObject.SetActive(isSelect);
            }
        }

        public bool IsSameEuqipmentType
        {
            get => isSameEuqipmentType;
            set
            {
                // 같은 타입인지 여부에 따라 선택 불가 그림자 이미지 출력
                isSameEuqipmentType = value;
                impossibleSelectImage.gameObject.SetActive(!isSameEuqipmentType);
            }
        }

        // 현재 장비 슬롯 반환
        public EquipmentItemSlot EquipmentSlot { get => equipmentSlot; }

        public void Awake()
        {
            equipmentSlot = GetComponent<EquipmentItemSlot>();
        }

        // 포인터를 슬롯 위에 올렸을때 툴팁을 보여준다.
        public void TRIGGER_OnPointerEnter_ShowTooltip(EquipmentTooltip equipmentTooltipUI)
        {
            if (!isSameEuqipmentType) return;

            // 장비 툴팁에 데이터를 넣는다.
            equipmentTooltipUI.ShowEquipmentTooltip(EquipmentSlot.EquipmentData);

            // 툴팁 위치를 자신의 정가운데로 이동하게 한다.
            Vector2 tooltipPosition = new Vector2((this.transform as RectTransform).position.x, (this.transform as RectTransform).position.y);
            (equipmentTooltipUI.transform as RectTransform).position = tooltipPosition;

            // 슬롯의 좌측 상단에 위치하도록 자신 가로 길이의 반절, 세로길이의 반절만큼 추가로 이동 시켜준다.
            Vector2 slotAnchoredPosition = new Vector2(-(this.transform as RectTransform).sizeDelta.x / 2, (this.transform as RectTransform).sizeDelta.y / 2);
            (equipmentTooltipUI.transform as RectTransform).anchoredPosition += slotAnchoredPosition;

            // 툴팁을 보여준다.
            equipmentTooltipUI.gameObject.SetActive(true);
        }

        // 포인터를 슬롯 위에 올렸을때 툴팁을 숨긴다.
        public void TRIGGER_OnPointerExit_HideTooltip(EquipmentTooltip equipmentTooltipUI)
        {
            // 같은 장비 타입일 때만 수행
            if (!isSameEuqipmentType) return;

            equipmentTooltipUI.gameObject.SetActive(false);
        }

        // 슬롯을 클릭했을때 선택한다.
        public void TRIGGER_OnClick_ChoiceItem(EquipmentListPopupUI equipmentListPopupUI)
        {
            // 같은 장비 타입일 때만 수행
            if (!isSameEuqipmentType) return;

            equipmentListPopupUI.ChoiceItem(this);

        }
    }
}