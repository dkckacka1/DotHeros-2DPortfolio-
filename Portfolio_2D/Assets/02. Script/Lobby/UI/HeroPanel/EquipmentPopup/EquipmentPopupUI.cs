using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Portfolio.Lobby
{
    public class EquipmentPopupUI : MonoBehaviour
    {
        EquipmentItemData equipmentData;
        EquipmentItemType equipmentItemType;

        [SerializeField] EquipmentReinforcePopupUI reinforcePopup;
        [SerializeField] EquipmentListPopupUI equipmentListPopup;
        [Header("Buttons")]
        [SerializeField] Button reinforceBtn;

        [Header("Equipment Data")]
        [SerializeField] Image equipmentImage;
        [SerializeField] Image equipmentDefaultImage;
        [SerializeField] TextMeshProUGUI equipmentNameText;
        [SerializeField] TextMeshProUGUI equipmentSetText;
        [SerializeField] Sprite[] defualtEquipmentSprits;

        [Header("Defualt Status")]
        [SerializeField] TextMeshProUGUI defaultStat_1_Lable;
        [SerializeField] TextMeshProUGUI defaultStat_1_Value;
        [SerializeField] TextMeshProUGUI defaultStat_2_Lable;
        [SerializeField] TextMeshProUGUI defaultStat_2_Value;

        [Header("Option Status")]
        [SerializeField] TextMeshProUGUI optionExplanationText;
        [SerializeField] TextMeshProUGUI optionStat_1_Lable;
        [SerializeField] TextMeshProUGUI optionStat_1_Value;
        [SerializeField] TextMeshProUGUI optionStat_2_Lable;
        [SerializeField] TextMeshProUGUI optionStat_2_Value;
        [SerializeField] TextMeshProUGUI optionStat_3_Lable;
        [SerializeField] TextMeshProUGUI optionStat_3_Value;
        [SerializeField] TextMeshProUGUI optionStat_4_Lable;
        [SerializeField] TextMeshProUGUI optionStat_4_Value;

        private void Awake()
        {
            reinforcePopup.gameObject.SetActive(false);
            equipmentListPopup.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            reinforcePopup.gameObject.SetActive(false);
            equipmentListPopup.gameObject.SetActive(false);
        }

        public void Init(EquipmentItemData data, EquipmentItemType equipmentItemType)
        {
            this.equipmentData = data;
            this.equipmentItemType = equipmentItemType;
            ShowEquipment(data);
            reinforcePopup.gameObject.SetActive(false);
        }

        public void ReShow()
        {
            if (this.equipmentData == null) return;

            ShowEquipment(this.equipmentData);
        }

        private void ShowEquipment(EquipmentItemData equipmentData)
        {
            if (equipmentData != null)
            {
                equipmentDefaultImage.gameObject.SetActive(false);
                equipmentImage.gameObject.SetActive(true);

                string reinforceCountText = (equipmentData.reinforceCount > 0) ? " +" + equipmentData.reinforceCount : "";
                equipmentNameText.text = $"{GameLib.GetGradeTypeText(equipmentData.equipmentGrade)} {GameLib.GetEquipmentTypeText(equipmentData.equipmentType)}{reinforceCountText}";
                equipmentSetText.gameObject.SetActive(true);
                equipmentSetText.text = GameLib.GetSetTypeText(equipmentData.setType);



                defaultStat_1_Lable.gameObject.SetActive(true);
                defaultStat_1_Value.gameObject.SetActive(true);
                defaultStat_2_Lable.gameObject.SetActive(equipmentData is AmuletData || equipmentData is RingData);
                defaultStat_2_Value.gameObject.SetActive(equipmentData is AmuletData || equipmentData is RingData);


                if (equipmentData is WeaponData)
                {
                    defaultStat_1_Lable.text = "공격력";
                    defaultStat_1_Value.text = (equipmentData as WeaponData).attackPoint.ToString();
                }
                else if (equipmentData is HelmetData)
                {
                    defaultStat_1_Lable.text = "생명력";
                    defaultStat_1_Value.text = (equipmentData as HelmetData).healthPoint.ToString();
                }
                else if (equipmentData is ArmorData)
                {
                    defaultStat_1_Lable.text = "방어력";
                    defaultStat_1_Value.text = (equipmentData as ArmorData).defencePoint.ToString();
                }
                else if (equipmentData is ShoeData)
                {
                    defaultStat_1_Lable.text = "속도";
                    defaultStat_1_Value.text = (equipmentData as ShoeData).speed.ToString();
                }
                else if (equipmentData is AmuletData)
                {
                    defaultStat_1_Lable.text = "치명타 적중";
                    defaultStat_1_Value.text = ((equipmentData as AmuletData).criticalPercent * 100).ToString("F1") + "%";
                    defaultStat_2_Lable.text = "치명타 피해";
                    defaultStat_2_Value.text = ((equipmentData as AmuletData).criticalDamage * 100).ToString("F1") + "%";
                }
                else if (equipmentData is RingData)
                {
                    defaultStat_1_Lable.text = "효과 적중";
                    defaultStat_1_Value.text = ((equipmentData as RingData).effectHit * 100).ToString("F1") + "%";
                    defaultStat_2_Lable.text = "효과 저항";
                    defaultStat_2_Value.text = ((equipmentData as RingData).effectResistance * 100).ToString("F1") + "%";
                }

                optionExplanationText.gameObject.SetActive(equipmentData.optionStat_1_Type == EquipmentOptionStat.NONE);

                InitOptionStat(equipmentData.optionStat_1_Type, equipmentData.optionStat_1_value, optionStat_1_Lable, optionStat_1_Value);
                InitOptionStat(equipmentData.optionStat_2_Type, equipmentData.optionStat_2_value, optionStat_2_Lable, optionStat_2_Value);
                InitOptionStat(equipmentData.optionStat_3_Type, equipmentData.optionStat_3_value, optionStat_3_Lable, optionStat_3_Value);
                InitOptionStat(equipmentData.optionStat_4_Type, equipmentData.optionStat_4_value, optionStat_4_Lable, optionStat_4_Value);

                reinforceBtn.interactable = true;
            }
            else
            {
                equipmentDefaultImage.gameObject.SetActive(true);
                equipmentDefaultImage.sprite = defualtEquipmentSprits[(int)equipmentItemType];
                equipmentImage.gameObject.SetActive(false);

                equipmentImage.gameObject.SetActive(false);
                equipmentNameText.text = $"장착한 장비가 없습니다.";
                equipmentSetText.gameObject.SetActive(false);

                defaultStat_1_Lable.gameObject.SetActive(false);
                defaultStat_1_Value.gameObject.SetActive(false);
                defaultStat_2_Lable.gameObject.SetActive(false);
                defaultStat_2_Value.gameObject.SetActive(false);

                optionExplanationText.gameObject.SetActive(false);
                optionStat_1_Lable.gameObject.SetActive(false);
                optionStat_1_Value.gameObject.SetActive(false);
                optionStat_2_Lable.gameObject.SetActive(false);
                optionStat_2_Value.gameObject.SetActive(false);
                optionStat_3_Lable.gameObject.SetActive(false);
                optionStat_3_Value.gameObject.SetActive(false);
                optionStat_4_Lable.gameObject.SetActive(false);
                optionStat_4_Value.gameObject.SetActive(false);

                reinforceBtn.interactable = false;
            }

        }

        private void InitOptionStat(EquipmentOptionStat optionStat, float value, TextMeshProUGUI labelText, TextMeshProUGUI valueText)
        {
            if (optionStat == EquipmentOptionStat.NONE)
            {
                labelText.gameObject.SetActive(false);
                valueText.gameObject.SetActive(false);
                return;
            }

            labelText.gameObject.SetActive(true);
            valueText.gameObject.SetActive(true);

            labelText.text = GameLib.GetOptionStatusText(optionStat);

            switch (optionStat)
            {
                case EquipmentOptionStat.AttackPoint:
                case EquipmentOptionStat.HealthPoint:
                case EquipmentOptionStat.DefencePoint:
                case EquipmentOptionStat.Speed:
                    valueText.text = value.ToString();
                    break;
                case EquipmentOptionStat.AttackPercent:
                case EquipmentOptionStat.HealthPercent:
                case EquipmentOptionStat.DefencePercent:
                case EquipmentOptionStat.CriticalPercent:
                case EquipmentOptionStat.CriticalDamagePercent:
                case EquipmentOptionStat.EffectHitPercent:
                case EquipmentOptionStat.EffectResistancePercent:
                    valueText.text = (value * 100f).ToString("F1") + "%";
                    break;
            }
        }

        public void ShowReinforcePopup()
        {
            reinforcePopup.Init(this.equipmentData);
            reinforcePopup.gameObject.SetActive(true);
        }

        public void ShowEquipmentListPopup()
        {
            equipmentListPopup.Init();
            equipmentListPopup.gameObject.SetActive(true);
        }

    }
}