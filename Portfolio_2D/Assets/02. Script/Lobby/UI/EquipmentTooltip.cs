using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ��� ������ ������ �����ִ� UI Ŭ����
 */

namespace Portfolio.Lobby
{
    public class EquipmentTooltip : MonoBehaviour
    {
        [Header("Equipment Data")]
        [SerializeField] private TextMeshProUGUI equipmentNameText;     // ��� �̸� �ؽ�Ʈ
        [SerializeField] private TextMeshProUGUI equipmentSetText;      // ��� ��Ʈ �̸� �ؽ�Ʈ

        [Header("Default Status")]
        [SerializeField] private TextMeshProUGUI defaultStat_1_Lable;   // �⺻ ���� 1 ���� �ؽ�Ʈ
        [SerializeField] private TextMeshProUGUI defaultStat_1_Value;   // �⺻ ���� 1 �� �ؽ�Ʈ
        [SerializeField] private TextMeshProUGUI defaultStat_2_Lable;   // �⺻ ���� 2 ���� �ؽ�Ʈ
        [SerializeField] private TextMeshProUGUI defaultStat_2_Value;   // �⺻ ���� 2 �� �ؽ�Ʈ

        [Header("Option Status")]
        [SerializeField] private TextMeshProUGUI optionStat_1_Lable;    // �ɼ� ���� 1 ���� �ؽ�Ʈ
        [SerializeField] private TextMeshProUGUI optionStat_1_Value;    // �ɼ� ���� 1 ���ؽ�Ʈ
        [SerializeField] private TextMeshProUGUI optionStat_2_Lable;    // �ɼ� ���� 2 ���� �ؽ�Ʈ
        [SerializeField] private TextMeshProUGUI optionStat_2_Value;    // �ɼ� ���� 2 ���ؽ�Ʈ
        [SerializeField] private TextMeshProUGUI optionStat_3_Lable;    // �ɼ� ���� 3 ���� �ؽ�Ʈ
        [SerializeField] private TextMeshProUGUI optionStat_3_Value;    // �ɼ� ���� 3 �� �ؽ�Ʈ
        [SerializeField] private TextMeshProUGUI optionStat_4_Lable;    // �ɼ� ���� 4 ���� �ؽ�Ʈ
        [SerializeField] private TextMeshProUGUI optionStat_4_Value;    // �ɼ� ���� 4 �� �ؽ�Ʈ

        public void ShowEquipmentTooltip(EquipmentItemData equipmentData)
        {
            // ��� ��ȭ ��ġ�� 0 �̻��̶�� '+' �ؽ�Ʈ�� �ٿ��ش�.
            string reinforceCountText = (equipmentData.reinforceCount > 0) ? " +" + equipmentData.reinforceCount : "";
            // ��� ��ް� ��� Ÿ��, ��ȭ ��ġ �ؽ�Ʈ�� �����ش�.
            equipmentNameText.text = $"{GameLib.GetGradeTypeText(equipmentData.equipmentGrade)} {GameLib.GetEquipmentTypeText(equipmentData.equipmentType)}{reinforceCountText}";
            //��� ��Ʈ �̸��� �����ش�.
            equipmentSetText.gameObject.SetActive(true);
            equipmentSetText.text = GameLib.GetSetTypeText(equipmentData.setType);

            // ����̿� ������ �⺻ ���� ������ 2������ �߰������� �����ش�.
            bool isAmuletOrRing = equipmentData is AmuletData || equipmentData is RingData;
            defaultStat_2_Lable.gameObject.SetActive(isAmuletOrRing);
            defaultStat_2_Value.gameObject.SetActive(isAmuletOrRing);

            // �� �����Ϳ� �µ��� �⺻ ���� �̸��� ���� �����ش�.
            if (equipmentData is WeaponData)
            {
                defaultStat_1_Lable.text = "���ݷ�";
                defaultStat_1_Value.text = (equipmentData as WeaponData).attackPoint.ToString();
            }
            else if (equipmentData is HelmetData)
            {
                defaultStat_1_Lable.text = "�����";
                defaultStat_1_Value.text = (equipmentData as HelmetData).healthPoint.ToString();
            }
            else if (equipmentData is ArmorData)
            {
                defaultStat_1_Lable.text = "����";
                defaultStat_1_Value.text = (equipmentData as ArmorData).defencePoint.ToString();
            }
            else if (equipmentData is ShoeData)
            {
                defaultStat_1_Lable.text = "�ӵ�";
                defaultStat_1_Value.text = (equipmentData as ShoeData).speed.ToString();
            }
            else if (equipmentData is AmuletData)
            {
                defaultStat_1_Lable.text = "ġ��Ÿ ����";
                defaultStat_1_Value.text = ((equipmentData as AmuletData).criticalPercent * 100).ToString("F1") + "%";
                defaultStat_2_Lable.text = "ġ��Ÿ ����";
                defaultStat_2_Value.text = ((equipmentData as AmuletData).criticalDamage * 100).ToString("F1") + "%";
            }
            else if (equipmentData is RingData)
            {
                defaultStat_1_Lable.text = "ȿ�� ����";
                defaultStat_1_Value.text = ((equipmentData as RingData).effectHit * 100).ToString("F1") + "%";
                defaultStat_2_Lable.text = "ȿ�� ����";
                defaultStat_2_Value.text = ((equipmentData as RingData).effectResistance * 100).ToString("F1") + "%";
            }

            // �ɼ� ���� ������ ���� ������ �ؽ�Ʈ�� �Է����ش�.
            InitOptionStat(equipmentData.optionStat_1_Type, equipmentData.optionStat_1_value, optionStat_1_Lable, optionStat_1_Value);
            InitOptionStat(equipmentData.optionStat_2_Type, equipmentData.optionStat_2_value, optionStat_2_Lable, optionStat_2_Value);
            InitOptionStat(equipmentData.optionStat_3_Type, equipmentData.optionStat_3_value, optionStat_3_Lable, optionStat_3_Value);
            InitOptionStat(equipmentData.optionStat_4_Type, equipmentData.optionStat_4_value, optionStat_4_Lable, optionStat_4_Value);
        }

        // ���� �ɼǽ��� ����, �� �� ���� �ؽ�Ʈ�� �����ش�.
        private void InitOptionStat(eEquipmentOptionStat optionStat, float value, TextMeshProUGUI labelText, TextMeshProUGUI valueText)
        {
            // ���� ������ ���ٸ� �ؽ�Ʈ�� �����.
            if (optionStat == eEquipmentOptionStat.NONE)
            {
                labelText.gameObject.SetActive(false);
                valueText.gameObject.SetActive(false);
                return;
            }

            labelText.gameObject.SetActive(true);
            valueText.gameObject.SetActive(true);

            // �ɼǽ��� ������ ���� �ؽ�Ʈ �Է�
            labelText.text = GameLib.GetOptionStatusText(optionStat);

            switch (optionStat)
            {
                case eEquipmentOptionStat.AttackPoint:
                case eEquipmentOptionStat.HealthPoint:
                case eEquipmentOptionStat.DefencePoint:
                case eEquipmentOptionStat.Speed:
                    // �Ϲ� ��ġ�� �����ش�.
                    valueText.text = value.ToString();
                    break;
                case eEquipmentOptionStat.AttackPercent:
                case eEquipmentOptionStat.HealthPercent:
                case eEquipmentOptionStat.DefencePercent:
                case eEquipmentOptionStat.CriticalPercent:
                case eEquipmentOptionStat.CriticalDamagePercent:
                case eEquipmentOptionStat.EffectHitPercent:
                case eEquipmentOptionStat.EffectResistancePercent:
                    // % ��ġ�� �����ش�. �Ҽ��� ù��°�ڸ� ���� �����ش�.
                    valueText.text = (value * 100f).ToString("F1") + "%";
                    break;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }
        }
    }
}