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

        [SerializeField] EquipmentReinforcePopupUI reinforcePopup;

        [Header("Equipment Data")]
        [SerializeField] Image equipmentImage;
        [SerializeField] TextMeshProUGUI equipmentNameText;
        [SerializeField] TextMeshProUGUI equipmentSetText;

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
        }

        public void Init(EquipmentItemData data)
        {
            this.equipmentData = data;
            ShowEquipment(data);
        }

        public void ReShow()
        {
            if (this.equipmentData == null) return;

            ShowEquipment(this.equipmentData);
        }

        private void ShowEquipment(EquipmentItemData equipmentData)
        {
            string reinforceCountText = (equipmentData.reinforceCount > 0) ? " +" + equipmentData.reinforceCount : "";
            equipmentNameText.text = $"{GameLib.GetGradeTypeText(equipmentData.equipmentGrade)} {GameLib.GetEquipmentTypeText(equipmentData.equipmentType)}{reinforceCountText}";
            equipmentSetText.text = GameLib.GetSetTypeText(equipmentData.setType);

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
                defaultStat_1_Value.text = ((equipmentData as AmuletData).criticalPercent * 100).ToString() + "%";
                defaultStat_2_Lable.text = "치명타 피해";
                defaultStat_2_Value.text = ((equipmentData as AmuletData).criticalDamage * 100).ToString() + "%";
            }
            else if (equipmentData is RingData)
            {
                defaultStat_1_Lable.text = "효과 적중";
                defaultStat_1_Value.text = ((equipmentData as RingData).effectHit * 100).ToString() + "%";
                defaultStat_2_Lable.text = "효과 저항";
                defaultStat_2_Value.text = ((equipmentData as RingData).effectResistance * 100).ToString() + "%";
            }

            optionExplanationText.gameObject.SetActive(equipmentData.optionStat_1_Type == EquipmentOptionStat.NONE);

            InitOptionStat(equipmentData.optionStat_1_Type, equipmentData.optionStat_1_value, optionStat_1_Lable, optionStat_1_Value);
            InitOptionStat(equipmentData.optionStat_2_Type, equipmentData.optionStat_2_value, optionStat_2_Lable, optionStat_2_Value);
            InitOptionStat(equipmentData.optionStat_3_Type, equipmentData.optionStat_3_value, optionStat_3_Lable, optionStat_3_Value);
            InitOptionStat(equipmentData.optionStat_4_Type, equipmentData.optionStat_4_value, optionStat_4_Lable, optionStat_4_Value);
        }

        public void ShowReinforcePopup()
        {
            reinforcePopup.Init(this.equipmentData);
            reinforcePopup.gameObject.SetActive(true);
        }

        private void InitOptionStat(EquipmentOptionStat optionStat, float value, TextMeshProUGUI labelText, TextMeshProUGUI valueText)
        {
            if (optionStat == EquipmentOptionStat.NONE)
            {
                labelText.gameObject.SetActive(false);
                valueText.gameObject.SetActive(false);
                return;
            }

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
                    valueText.text = (value * 100f).ToString() + "%";
                    break;
            }
        }
    }
}