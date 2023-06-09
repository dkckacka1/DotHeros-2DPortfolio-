using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class EquipmentSelectUI : MonoBehaviour
    {
        private UnitEquipmentSlotUI equipmentSlot;

        [SerializeField] Image selectedImage;
        [SerializeField] Image impossibleSelectImage;

        public bool isChoice;
        private bool isSameEuqipmentType;

        public void Init()
        {
            equipmentSlot = GetComponent<UnitEquipmentSlotUI>();
        }

        private void OnEnable()
        {
            selectedImage.gameObject.SetActive(false);
        }

        public void ShowImpossibleSelectImage(EquipmentItemData selectedData)
        {
            if (equipmentSlot.EquipmentData.equipmentType == selectedData.equipmentType)
                // 기 선택한 아이템 데이타가 현재 슬롯의 아이템과 같을 경우
            {
                isSameEuqipmentType = true;
                impossibleSelectImage.gameObject.SetActive(false);
            }
            else
                // 기 선택한 아이템 데이타가 현재 슬롯의 아이템과 다를 경우
            {
                isSameEuqipmentType = false;
                impossibleSelectImage.gameObject.SetActive(true);
            }
        }

        public void ShowImpossibleSelectImage(EquipmentItemType itemType)
        {
            if (equipmentSlot.EquipmentData.equipmentType == itemType)
            // 기 선택한 아이템 데이타가 현재 슬롯의 아이템과 같을 경우
            {
                isSameEuqipmentType = true;
                impossibleSelectImage.gameObject.SetActive(false);
            }
            else
            // 기 선택한 아이템 데이타가 현재 슬롯의 아이템과 다를 경우
            {
                isSameEuqipmentType = false;
                impossibleSelectImage.gameObject.SetActive(true);
            }
        }

        public void ShowTooltip(EquipmentTooltip equipmentTooltipUI)
        {
            if (!isSameEuqipmentType) return;

            equipmentTooltipUI.ShowEquipmentTooltip(equipmentSlot.EquipmentData);

            Vector2 tooltipPosition = new Vector2((this.transform as RectTransform).position.x - (this.transform as RectTransform).sizeDelta.x / 2, (this.transform as RectTransform).position.y + (this.transform as RectTransform).sizeDelta.y / 2);
            (equipmentTooltipUI.transform as RectTransform).position = tooltipPosition;
            //Debug.Log(this.transform.position + " : " + this.transform.localPosition);
            equipmentTooltipUI.gameObject.SetActive(true);
        }

        public void HideTooltip(EquipmentTooltip equipmentTooltipUI)
        {
            if (!isSameEuqipmentType) return;

            equipmentTooltipUI.gameObject.SetActive(false);
        }

        public void ChoiceItem(HeroPanelUI heroPanelUI)
        {
            if (!isSameEuqipmentType) return;

            isChoice = true;
            heroPanelUI.ChoiceItem(ref isChoice, equipmentSlot.EquipmentData);
        }

        public void ShowSelectedUI()
        {
            selectedImage.gameObject.SetActive(true);
        }

        public void HideSelectedUI()
        {
            selectedImage.gameObject.SetActive(false);
        }
    }
}