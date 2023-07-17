using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 장비 아이템 툴팁을 보여주는 UI 클래스
 */

namespace Portfolio.Lobby
{
    public class EquipmentTooltip : MonoBehaviour
    {
        [Header("Equipment Data")]
        [SerializeField] private TextMeshProUGUI equipmentNameText;     // 장비 이름 텍스트
        [SerializeField] private TextMeshProUGUI equipmentSetText;      // 장비 세트 이름 텍스트

        [Header("Default Status")]
        [SerializeField] private TextMeshProUGUI defaultStat_1_Lable;   // 기본 스탯 1 종류 텍스트
        [SerializeField] private TextMeshProUGUI defaultStat_1_Value;   // 기본 스탯 1 값 텍스트
        [SerializeField] private TextMeshProUGUI defaultStat_2_Lable;   // 기본 스탯 2 종류 텍스트
        [SerializeField] private TextMeshProUGUI defaultStat_2_Value;   // 기본 스탯 2 값 텍스트

        [Header("Option Status")]
        [SerializeField] private TextMeshProUGUI optionStat_1_Lable;    // 옵션 스텟 1 종류 텍스트
        [SerializeField] private TextMeshProUGUI optionStat_1_Value;    // 옵션 스텟 1 값텍스트
        [SerializeField] private TextMeshProUGUI optionStat_2_Lable;    // 옵션 스텟 2 종류 텍스트
        [SerializeField] private TextMeshProUGUI optionStat_2_Value;    // 옵션 스텟 2 값텍스트
        [SerializeField] private TextMeshProUGUI optionStat_3_Lable;    // 옵션 스텟 3 종류 텍스트
        [SerializeField] private TextMeshProUGUI optionStat_3_Value;    // 옵션 스텟 3 값 텍스트
        [SerializeField] private TextMeshProUGUI optionStat_4_Lable;    // 옵션 스텟 4 종류 텍스트
        [SerializeField] private TextMeshProUGUI optionStat_4_Value;    // 옵션 스텟 4 값 텍스트

        public void ShowEquipmentTooltip(EquipmentItemData equipmentData)
        {
            // 장비 강화 수치가 0 이상이라면 '+' 텍스트를 붙여준다.
            string reinforceCountText = (equipmentData.reinforceCount > 0) ? " +" + equipmentData.reinforceCount : "";
            // 장비 등급과 장비 타입, 강화 수치 텍스트를 보여준다.
            equipmentNameText.text = $"{GameLib.GetGradeTypeText(equipmentData.equipmentGrade)} {GameLib.GetEquipmentTypeText(equipmentData.equipmentType)}{reinforceCountText}";
            //장비 세트 이름을 보여준다.
            equipmentSetText.gameObject.SetActive(true);
            equipmentSetText.text = GameLib.GetSetTypeText(equipmentData.setType);

            // 목걸이와 반지는 기본 스탯 종류가 2가지라 추가적으로 보여준다.
            bool isAmuletOrRing = equipmentData is AmuletData || equipmentData is RingData;
            defaultStat_2_Lable.gameObject.SetActive(isAmuletOrRing);
            defaultStat_2_Value.gameObject.SetActive(isAmuletOrRing);

            // 각 데이터에 맞도록 기본 스탯 이름과 값을 보여준다.
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

            // 옵션 스탯 종류에 따라 보여줄 텍스트를 입력해준다.
            InitOptionStat(equipmentData.optionStat_1_Type, equipmentData.optionStat_1_value, optionStat_1_Lable, optionStat_1_Value);
            InitOptionStat(equipmentData.optionStat_2_Type, equipmentData.optionStat_2_value, optionStat_2_Lable, optionStat_2_Value);
            InitOptionStat(equipmentData.optionStat_3_Type, equipmentData.optionStat_3_value, optionStat_3_Lable, optionStat_3_Value);
            InitOptionStat(equipmentData.optionStat_4_Type, equipmentData.optionStat_4_value, optionStat_4_Lable, optionStat_4_Value);
        }

        // 들어온 옵션스탯 종류, 값 에 따라서 텍스트를 보여준다.
        private void InitOptionStat(eEquipmentOptionStat optionStat, float value, TextMeshProUGUI labelText, TextMeshProUGUI valueText)
        {
            // 들어온 스탯이 없다면 텍스트를 숨긴다.
            if (optionStat == eEquipmentOptionStat.NONE)
            {
                labelText.gameObject.SetActive(false);
                valueText.gameObject.SetActive(false);
                return;
            }

            labelText.gameObject.SetActive(true);
            valueText.gameObject.SetActive(true);

            // 옵션스탯 종류에 따라 텍스트 입력
            labelText.text = GameLib.GetOptionStatusText(optionStat);

            switch (optionStat)
            {
                case eEquipmentOptionStat.AttackPoint:
                case eEquipmentOptionStat.HealthPoint:
                case eEquipmentOptionStat.DefencePoint:
                case eEquipmentOptionStat.Speed:
                    // 일반 수치를 보여준다.
                    valueText.text = value.ToString();
                    break;
                case eEquipmentOptionStat.AttackPercent:
                case eEquipmentOptionStat.HealthPercent:
                case eEquipmentOptionStat.DefencePercent:
                case eEquipmentOptionStat.CriticalPercent:
                case eEquipmentOptionStat.CriticalDamagePercent:
                case eEquipmentOptionStat.EffectHitPercent:
                case eEquipmentOptionStat.EffectResistancePercent:
                    // % 수치로 보여준다. 소수점 첫번째자리 까지 보여준다.
                    valueText.text = (value * 100f).ToString("F1") + "%";
                    break;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }
        }
    }
}