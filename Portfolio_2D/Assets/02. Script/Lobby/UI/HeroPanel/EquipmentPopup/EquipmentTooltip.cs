using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class EquipmentTooltip : MonoBehaviour
    {
        [Header("Equipment Data")]
        [SerializeField] private TextMeshProUGUI equipmentNameText;
        [SerializeField] private TextMeshProUGUI equipmentSetText;

        [Header("Default Status")]
        [SerializeField] private TextMeshProUGUI defaultStat_1_Lable;
        [SerializeField] private TextMeshProUGUI defaultStat_1_Value;
        [SerializeField] private TextMeshProUGUI defaultStat_2_Lable;
        [SerializeField] private TextMeshProUGUI defaultStat_2_Value;

        [Header("Option Status")]
        [SerializeField] private TextMeshProUGUI optionStat_1_Lable;
        [SerializeField] private TextMeshProUGUI optionStat_1_Value;
        [SerializeField] private TextMeshProUGUI optionStat_2_Lable;
        [SerializeField] private TextMeshProUGUI optionStat_2_Value;
        [SerializeField] private TextMeshProUGUI optionStat_3_Lable;
        [SerializeField] private TextMeshProUGUI optionStat_3_Value;
        [SerializeField] private TextMeshProUGUI optionStat_4_Lable;
        [SerializeField] private TextMeshProUGUI optionStat_4_Value;

        public void ShowEquipmentTooltip(EquipmentItemData equipmentData)
        {
            string reinforceCountText = (equipmentData.reinforceCount > 0) ? " +" + equipmentData.reinforceCount : "";
            equipmentNameText.text = $"{GameLib.GetGradeTypeText(equipmentData.equipmentGrade)} {GameLib.GetEquipmentTypeText(equipmentData.equipmentType)}{reinforceCountText}";
            equipmentSetText.gameObject.SetActive(true);
            equipmentSetText.text = GameLib.GetSetTypeText(equipmentData.setType);

            bool isAmuletOrRing = equipmentData is AmuletData || equipmentData is RingData;
            defaultStat_2_Lable.gameObject.SetActive(isAmuletOrRing);
            defaultStat_2_Value.gameObject.SetActive(isAmuletOrRing);

            if (equipmentData is WeaponData)
            {
                //Debug.Log("WeaponData");
                defaultStat_1_Lable.text = "공격력";
                defaultStat_1_Value.text = (equipmentData as WeaponData).attackPoint.ToString();
            }
            else if (equipmentData is HelmetData)
            {
                //Debug.Log("HelmetData");
                defaultStat_1_Lable.text = "생명력";
                defaultStat_1_Value.text = (equipmentData as HelmetData).healthPoint.ToString();
            }
            else if (equipmentData is ArmorData)
            {
                //Debug.Log("ArmorData");
                defaultStat_1_Lable.text = "방어력";
                defaultStat_1_Value.text = (equipmentData as ArmorData).defencePoint.ToString();
            }
            else if (equipmentData is ShoeData)
            {
                //Debug.Log("ShoeData");
                defaultStat_1_Lable.text = "속도";
                defaultStat_1_Value.text = (equipmentData as ShoeData).speed.ToString();
            }
            else if (equipmentData is AmuletData)
            {
                //Debug.Log("AmuletData");
                defaultStat_1_Lable.text = "치명타 적중";
                defaultStat_1_Value.text = ((equipmentData as AmuletData).criticalPercent * 100).ToString("F1") + "%";
                defaultStat_2_Lable.text = "치명타 피해";
                defaultStat_2_Value.text = ((equipmentData as AmuletData).criticalDamage * 100).ToString("F1") + "%";
            }
            else if (equipmentData is RingData)
            {
                //Debug.Log("RingData");
                defaultStat_1_Lable.text = "효과 적중";
                defaultStat_1_Value.text = ((equipmentData as RingData).effectHit * 100).ToString("F1") + "%";
                defaultStat_2_Lable.text = "효과 저항";
                defaultStat_2_Value.text = ((equipmentData as RingData).effectResistance * 100).ToString("F1") + "%";
            }
            else
            {
                //Debug.Log("notData");
            }

            InitOptionStat(equipmentData.optionStat_1_Type, equipmentData.optionStat_1_value, optionStat_1_Lable, optionStat_1_Value);
            InitOptionStat(equipmentData.optionStat_2_Type, equipmentData.optionStat_2_value, optionStat_2_Lable, optionStat_2_Value);
            InitOptionStat(equipmentData.optionStat_3_Type, equipmentData.optionStat_3_value, optionStat_3_Lable, optionStat_3_Value);
            InitOptionStat(equipmentData.optionStat_4_Type, equipmentData.optionStat_4_value, optionStat_4_Lable, optionStat_4_Value);
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
    }
}