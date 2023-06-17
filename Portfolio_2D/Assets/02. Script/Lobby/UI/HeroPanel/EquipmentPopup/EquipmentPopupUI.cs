using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Portfolio.UI;
using System;

namespace Portfolio.Lobby.Hero
{
    public class EquipmentPopupUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] Button reinforceBtn;

        [Header("Equipment Data")]
        [SerializeField] UnitEquipmentSlotUI equipmentSlotUI;
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

        internal void Init()
        {
            LobbyManager.UIManager.equipmentItemDataChangeEvent += ShowEquipment;
        }

        public void ShowEquipment(object sender, EventArgs eventArgs)
        {
            if (HeroPanelUI.SelectEquipmentItem != null)
            {
                ShowEquipment(HeroPanelUI.SelectEquipmentItem);
            }
            else
            {
                ShowEquipment(HeroPanelUI.SelectEquipmentItemType);
            }
        }

        public void ShowEquipment(EquipmentItemData equipmentData)
        {
            equipmentSlotUI.ShowEquipment(equipmentData);
            string reinforceCountText = (equipmentData.reinforceCount > 0) ? " +" + equipmentData.reinforceCount : "";
            equipmentNameText.text = $"{GameLib.GetGradeTypeText(equipmentData.equipmentGrade)} {GameLib.GetEquipmentTypeText(equipmentData.equipmentType)}{reinforceCountText}";
            equipmentSetText.gameObject.SetActive(true);
            equipmentSetText.text = GameLib.GetSetTypeText(equipmentData.setType);



            defaultStat_1_Lable.gameObject.SetActive(true);
            defaultStat_1_Value.gameObject.SetActive(true);
            bool isAmuletOrRing = equipmentData is AmuletData || equipmentData is RingData;
            defaultStat_2_Lable.gameObject.SetActive(isAmuletOrRing);
            defaultStat_2_Value.gameObject.SetActive(isAmuletOrRing);


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

            optionExplanationText.gameObject.SetActive(equipmentData.optionStat_1_Type == EquipmentOptionStat.NONE);

            InitOptionStat(equipmentData.optionStat_1_Type, equipmentData.optionStat_1_value, optionStat_1_Lable, optionStat_1_Value);
            InitOptionStat(equipmentData.optionStat_2_Type, equipmentData.optionStat_2_value, optionStat_2_Lable, optionStat_2_Value);
            InitOptionStat(equipmentData.optionStat_3_Type, equipmentData.optionStat_3_value, optionStat_3_Lable, optionStat_3_Value);
            InitOptionStat(equipmentData.optionStat_4_Type, equipmentData.optionStat_4_value, optionStat_4_Lable, optionStat_4_Value);

            reinforceBtn.interactable = true;
        }


        public void ShowEquipment(EquipmentItemType equipmentItemType)
        {
            equipmentSlotUI.ShowEquipment(equipmentItemType);

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

            reinforceBtn.interactable = false;
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