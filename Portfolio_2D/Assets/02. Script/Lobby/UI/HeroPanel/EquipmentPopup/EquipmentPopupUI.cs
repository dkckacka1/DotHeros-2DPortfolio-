using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Portfolio.UI;
using System;

/*
 * ���� ���â���� ���������� Ŭ�������� ������ �˾� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class EquipmentPopupUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] Button reinforceBtn;                       // ��� ��ȭ ��ư

        [Header("Equipment Data")]
        [SerializeField] EquipmentItemSlot equipmentSlotUI;         // ��� ���� UI
        [SerializeField] TextMeshProUGUI equipmentNameText;         // ��� �̸� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI equipmentSetText;          // ��� ��Ʈ �̸� �ؽ�Ʈ
        [SerializeField] Sprite[] defualtEquipmentSprits;           // ������ ��� ���� �� ������ ��� �⺻ �̹���

        [Header("Defualt Status")]
        [SerializeField] TextMeshProUGUI defaultStat_1_Lable;       // �⺻ ���� 1 ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI defaultStat_1_Value;       // �⺻ ���� 1 �� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI defaultStat_2_Lable;       // �⺻ ���� 2 ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI defaultStat_2_Value;       // �⺻ ���� 2 �� �ؽ�Ʈ

        [Header("Option Status")]
        [SerializeField] TextMeshProUGUI optionExplanationText;     // �ƹ��� �ɼ��� ���� �� ������ �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI optionStat_1_Lable;        // �ɼ� ���� 1 ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI optionStat_1_Value;        // �ɼ� ���� 1 ���ؽ�Ʈ
        [SerializeField] TextMeshProUGUI optionStat_2_Lable;        // �ɼ� ���� 2 ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI optionStat_2_Value;        // �ɼ� ���� 2 ���ؽ�Ʈ
        [SerializeField] TextMeshProUGUI optionStat_3_Lable;        // �ɼ� ���� 3 ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI optionStat_3_Value;        // �ɼ� ���� 3 �� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI optionStat_4_Lable;        // �ɼ� ���� 4 ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI optionStat_4_Value;        // �ɼ� ���� 4 �� �ؽ�Ʈ

        public void Init()
        {
            // ����â���� ������ ��� �ٲ���� �� �̺�Ʈ�� �����Ѵ�.
            LobbyManager.UIManager.equipmentItemDataChangeEvent += ShowEquipment;
        }

        // ���������� �����ش�.
        public void ShowEquipment(object sender, EventArgs eventArgs)
        {
            // ������ ��� ���ٸ�
            if (HeroPanelUI.SelectEquipmentItem != null)
            {
                // ���� ������ ��� Ÿ������ �����ش�.
                ShowEquipment(HeroPanelUI.SelectEquipmentItem);
            }
            else
            {
                // ������ ����� �����͸� �����ش�.
                ShowEquipment(HeroPanelUI.SelectEquipmentItemType);
            }
        }

        // ������ ��� �����͸� �����ش�.
        public void ShowEquipment(EquipmentItemData equipmentData)
        {
            // ���Կ� ��� �����͸� �����ش�.
            equipmentSlotUI.ShowEquipment(equipmentData);
            // ��� ��ȭ ��ġ�� 0 �̻��̶�� '+' �ؽ�Ʈ�� �ٿ��ش�.
            string reinforceCountText = (equipmentData.reinforceCount > 0) ? " +" + equipmentData.reinforceCount : "";
            // ��� ��ް� ��� Ÿ��, ��ȭ ��ġ �ؽ�Ʈ�� �����ش�.
            equipmentNameText.text = $"{GameLib.GetGradeTypeText(equipmentData.equipmentGrade)} {GameLib.GetEquipmentTypeText(equipmentData.equipmentType)}{reinforceCountText}";

            //��� ��Ʈ �̸��� �����ش�.
            equipmentSetText.gameObject.SetActive(true);
            equipmentSetText.text = GameLib.GetSetTypeText(equipmentData.setType);

            defaultStat_1_Lable.gameObject.SetActive(true);
            defaultStat_1_Value.gameObject.SetActive(true);
            
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

            // �ɼǽ����� �ϳ��� ���ٸ� ��ȭ �ʿ� �ؽ�Ʈ�� �����ش�.
            optionExplanationText.gameObject.SetActive(equipmentData.optionStat_1_Type == EquipmentOptionStat.NONE);

            // �ɼ� ���� ������ ���� ������ �ؽ�Ʈ�� �Է����ش�.
            InitOptionStat(equipmentData.optionStat_1_Type, equipmentData.optionStat_1_value, optionStat_1_Lable, optionStat_1_Value);
            InitOptionStat(equipmentData.optionStat_2_Type, equipmentData.optionStat_2_value, optionStat_2_Lable, optionStat_2_Value);
            InitOptionStat(equipmentData.optionStat_3_Type, equipmentData.optionStat_3_value, optionStat_3_Lable, optionStat_3_Value);
            InitOptionStat(equipmentData.optionStat_4_Type, equipmentData.optionStat_4_value, optionStat_4_Lable, optionStat_4_Value);

            // ������ ��� �������� �����Ƿ� ��� ��ȭ ��ư ��ȣ�ۿ��� Ȱ��ȭ �Ѵ�.
            reinforceBtn.interactable = true;
        }

        // ������ ��� �������� ������� ���� ���Կ� ���� ��� Ÿ���� �����ش�.
        public void ShowEquipment(EquipmentItemType equipmentItemType)
        {
            // �⺻ �̹����� �����ش�.
            equipmentSlotUI.ShowEquipment(equipmentItemType);

            // ������ ��� �����Ƿ� ��� ������ ������ �����ش�.
            equipmentNameText.text = $"������ ��� �����ϴ�.";
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

            // ������ ��� �����Ƿ� ��� ��ȭ ��ư ��ȣ�ۿ� ��Ȱ��ȭ
            reinforceBtn.interactable = false;
        }

        // ���� �ɼǽ��� ����, �� �� ���� �ؽ�Ʈ�� �����ش�.
        private void InitOptionStat(EquipmentOptionStat optionStat, float value, TextMeshProUGUI labelText, TextMeshProUGUI valueText)
        {
            // ���� ������ ���ٸ� �ؽ�Ʈ�� �����.
            if (optionStat == EquipmentOptionStat.NONE)
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
                case EquipmentOptionStat.AttackPoint:
                case EquipmentOptionStat.HealthPoint:
                case EquipmentOptionStat.DefencePoint:
                case EquipmentOptionStat.Speed:
                    // �Ϲ� ��ġ�� �����ش�.
                    valueText.text = value.ToString();
                    break;
                case EquipmentOptionStat.AttackPercent:
                case EquipmentOptionStat.HealthPercent:
                case EquipmentOptionStat.DefencePercent:
                case EquipmentOptionStat.CriticalPercent:
                case EquipmentOptionStat.CriticalDamagePercent:
                case EquipmentOptionStat.EffectHitPercent:
                case EquipmentOptionStat.EffectResistancePercent:
                    // % ��ġ�� �����ش�. �Ҽ��� ù��°�ڸ� ���� �����ش�.
                    valueText.text = (value * 100f).ToString("F1") + "%";
                    break;
            }
        }



    }
}