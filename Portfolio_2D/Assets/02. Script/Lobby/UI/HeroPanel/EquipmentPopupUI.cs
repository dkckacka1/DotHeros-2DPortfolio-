using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Portfolio.Lobby
{
    public class EquipmentPopupUI : MonoBehaviour
    {
        EquipmentItemData data;

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

        public void Init(EquipmentItemData data)
        {
            this.data = data;
            string reinforceCountText = (data.reinforceCount > 0) ? " +" + data.reinforceCount : "";
            equipmentNameText.text = $"{GameLib.GetGradeTypeText(data.equipmentGrade)} {GameLib.GetEquipmentTypeText(data.equipmentType)}{reinforceCountText}";
            equipmentSetText.text = GameLib.GetSetTypeText(data.setType);

            defaultStat_2_Lable.gameObject.SetActive(data is AmuletData || data is RingData);
            defaultStat_2_Value.gameObject.SetActive(data is AmuletData || data is RingData);


            if (data is WeaponData)
            {
                defaultStat_1_Lable.text = "공격력";
                defaultStat_1_Value.text = (data as WeaponData).attackPoint.ToString();
            }
            else if (data is HelmetData)
            {
                defaultStat_1_Lable.text = "생명력";
                defaultStat_1_Value.text = (data as HelmetData).healthPoint.ToString();
            }
            else if (data is ArmorData)
            {
                defaultStat_1_Lable.text = "방어력";
                defaultStat_1_Value.text = (data as ArmorData).defencePoint.ToString();
            }
            else if (data is ShoeData)
            {
                defaultStat_1_Lable.text = "속도";
                defaultStat_1_Value.text = (data as ShoeData).speed.ToString();
            }
            else if (data is AmuletData)
            {
                defaultStat_1_Lable.text = "치명타 적중";
                defaultStat_1_Value.text = ((data as AmuletData).criticalPercent * 100).ToString() + "%";
                defaultStat_2_Lable.text = "치명타 피해";
                defaultStat_2_Value.text = ((data as AmuletData).criticalDamage * 100).ToString() + "%";
            }
            else if (data is RingData)
            {
                defaultStat_1_Lable.text = "효과 적중";
                defaultStat_1_Value.text = ((data as RingData).effectHit * 100).ToString() + "%";
                defaultStat_2_Lable.text = "효과 저항";
                defaultStat_2_Value.text = ((data as RingData).effectResistance * 100).ToString() + "%";
            }

            optionExplanationText.gameObject.SetActive(data.optionStat_1_Type == EquipmentOptionStat.NONE);

            InitOptionStat(data.optionStat_1_Type, data.optionStat_1_value, optionStat_1_Lable, optionStat_1_Value);
            InitOptionStat(data.optionStat_2_Type, data.optionStat_2_value, optionStat_2_Lable, optionStat_2_Value);
            InitOptionStat(data.optionStat_3_Type, data.optionStat_3_value, optionStat_3_Lable, optionStat_3_Value);
            InitOptionStat(data.optionStat_4_Type, data.optionStat_4_value, optionStat_4_Lable, optionStat_4_Value);
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