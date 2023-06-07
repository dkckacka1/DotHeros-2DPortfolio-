using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class EquipmentSelectUI : MonoBehaviour
    {
        private UnitEquipmentSlotUI equipmentSlot;

        [SerializeField] Image selectedImage;
        [SerializeField] Image impossibleSelectImage;

        private bool isSameEuqipmentType;

        private void Awake()
        {
            equipmentSlot = GetComponent<UnitEquipmentSlotUI>();
        }

        private void OnEnable()
        {
            selectedImage.gameObject.SetActive(false);
        }

        public void Init(EquipmentItemData selectedData)
        {
            if (equipmentSlot.EquipmentData.equipmentType == selectedData.equipmentType)
                // �� ������ ������ ����Ÿ�� ���� ������ �����۰� ���� ���
            {
                isSameEuqipmentType = true;
                impossibleSelectImage.gameObject.SetActive(false);
            }
            else
                // �� ������ ������ ����Ÿ�� ���� ������ �����۰� �ٸ� ���
            {
                isSameEuqipmentType = false;
                impossibleSelectImage.gameObject.SetActive(true);
            }
        }

        public void Init(EquipmentItemType itemType)
        {
            if (equipmentSlot.EquipmentData.equipmentType == itemType)
            // �� ������ ������ ����Ÿ�� ���� ������ �����۰� ���� ���
            {
                isSameEuqipmentType = true;
                impossibleSelectImage.gameObject.SetActive(false);
            }
            else
            // �� ������ ������ ����Ÿ�� ���� ������ �����۰� �ٸ� ���
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

        //public void SelectItem(HeroPanelUI heroPanelUI)
        //{
        //    if (!isSameEuqipmentType) return;

        //    heroPanelUI.ChoiceItem(this, equipmentSlot.EquipmentData);
        //}

        //public void ShowSelectedUI()
        //{
        //    selectedImage.gameObject.SetActive(true);
        //}

        //public void HideSelectedUI()
        //{
        //    selectedImage.gameObject.SetActive(false);
        //}
    }
}