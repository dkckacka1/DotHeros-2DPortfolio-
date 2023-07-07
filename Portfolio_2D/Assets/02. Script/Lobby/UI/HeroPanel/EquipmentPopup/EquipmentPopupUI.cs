using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Portfolio.UI;
using System;

/*
 * 유닛 장비창에서 장비아이템을 클릭했을때 나오는 팝업 UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class EquipmentPopupUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] Button reinforceBtn;                       // 장비 강화 버튼

        [Header("Equipment Data")]
        [SerializeField] EquipmentItemSlot equipmentSlotUI;         // 장비 슬롯 UI
        [SerializeField] TextMeshProUGUI equipmentNameText;         // 장비 이름 텍스트
        [SerializeField] TextMeshProUGUI equipmentSetText;          // 장비 세트 이름 텍스트
        [SerializeField] Sprite[] defualtEquipmentSprits;           // 장착한 장비가 없을 때 보여줄 장비 기본 이미지

        [Header("Defualt Status")]
        [SerializeField] TextMeshProUGUI defaultStat_1_Lable;       // 기본 스탯 1 종류 텍스트
        [SerializeField] TextMeshProUGUI defaultStat_1_Value;       // 기본 스탯 1 값 텍스트
        [SerializeField] TextMeshProUGUI defaultStat_2_Lable;       // 기본 스탯 2 종류 텍스트
        [SerializeField] TextMeshProUGUI defaultStat_2_Value;       // 기본 스탯 2 값 텍스트

        [Header("Option Status")]
        [SerializeField] TextMeshProUGUI optionExplanationText;     // 아무런 옵션이 없을 때 보여줄 텍스트
        [SerializeField] TextMeshProUGUI optionStat_1_Lable;        // 옵션 스텟 1 종류 텍스트
        [SerializeField] TextMeshProUGUI optionStat_1_Value;        // 옵션 스텟 1 값텍스트
        [SerializeField] TextMeshProUGUI optionStat_2_Lable;        // 옵션 스텟 2 종류 텍스트
        [SerializeField] TextMeshProUGUI optionStat_2_Value;        // 옵션 스텟 2 값텍스트
        [SerializeField] TextMeshProUGUI optionStat_3_Lable;        // 옵션 스텟 3 종류 텍스트
        [SerializeField] TextMeshProUGUI optionStat_3_Value;        // 옵션 스텟 3 값 텍스트
        [SerializeField] TextMeshProUGUI optionStat_4_Lable;        // 옵션 스텟 4 종류 텍스트
        [SerializeField] TextMeshProUGUI optionStat_4_Value;        // 옵션 스텟 4 값 텍스트

        public void Init()
        {
            // 영웅창에서 선택한 장비가 바뀌었을 때 이벤트를 구독한다.
            LobbyManager.UIManager.equipmentItemDataChangeEvent += ShowEquipment;
        }

        // 장비아이템을 보여준다.
        public void ShowEquipment(object sender, EventArgs eventArgs)
        {
            // 장착한 장비가 없다면
            if (HeroPanelUI.SelectEquipmentItem != null)
            {
                // 장착 슬롯의 장비 타입으로 보여준다.
                ShowEquipment(HeroPanelUI.SelectEquipmentItem);
            }
            else
            {
                // 장착한 장비의 데이터를 보여준다.
                ShowEquipment(HeroPanelUI.SelectEquipmentItemType);
            }
        }

        // 선택한 장비 데이터를 보여준다.
        public void ShowEquipment(EquipmentItemData equipmentData)
        {
            // 슬롯에 장비 데이터를 보여준다.
            equipmentSlotUI.ShowEquipment(equipmentData);
            // 장비 강화 수치가 0 이상이라면 '+' 텍스트를 붙여준다.
            string reinforceCountText = (equipmentData.reinforceCount > 0) ? " +" + equipmentData.reinforceCount : "";
            // 장비 등급과 장비 타입, 강화 수치 텍스트를 보여준다.
            equipmentNameText.text = $"{GameLib.GetGradeTypeText(equipmentData.equipmentGrade)} {GameLib.GetEquipmentTypeText(equipmentData.equipmentType)}{reinforceCountText}";

            //장비 세트 이름을 보여준다.
            equipmentSetText.gameObject.SetActive(true);
            equipmentSetText.text = GameLib.GetSetTypeText(equipmentData.setType);

            defaultStat_1_Lable.gameObject.SetActive(true);
            defaultStat_1_Value.gameObject.SetActive(true);
            
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

            // 옵션스탯이 하나도 없다면 강화 필요 텍스트를 보여준다.
            optionExplanationText.gameObject.SetActive(equipmentData.optionStat_1_Type == EquipmentOptionStat.NONE);

            // 옵션 스탯 종류에 따라 보여줄 텍스트를 입력해준다.
            InitOptionStat(equipmentData.optionStat_1_Type, equipmentData.optionStat_1_value, optionStat_1_Lable, optionStat_1_Value);
            InitOptionStat(equipmentData.optionStat_2_Type, equipmentData.optionStat_2_value, optionStat_2_Lable, optionStat_2_Value);
            InitOptionStat(equipmentData.optionStat_3_Type, equipmentData.optionStat_3_value, optionStat_3_Lable, optionStat_3_Value);
            InitOptionStat(equipmentData.optionStat_4_Type, equipmentData.optionStat_4_value, optionStat_4_Lable, optionStat_4_Value);

            // 장착한 장비 아이템이 있으므로 장비 강화 버튼 상호작용을 활성화 한다.
            reinforceBtn.interactable = true;
        }

        // 장착한 장비 아이템이 없을경우 장착 슬롯에 따른 장비 타입을 보여준다.
        public void ShowEquipment(EquipmentItemType equipmentItemType)
        {
            // 기본 이미지를 보여준다.
            equipmentSlotUI.ShowEquipment(equipmentItemType);

            // 장착한 장비가 없으므로 모든 스텟을 정보를 숨겨준다.
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

            // 장착한 장비가 없으므로 장비 강화 버튼 상호작용 비활성화
            reinforceBtn.interactable = false;
        }

        // 들어온 옵션스탯 종류, 값 에 따라서 텍스트를 보여준다.
        private void InitOptionStat(EquipmentOptionStat optionStat, float value, TextMeshProUGUI labelText, TextMeshProUGUI valueText)
        {
            // 들어온 스탯이 없다면 텍스트를 숨긴다.
            if (optionStat == EquipmentOptionStat.NONE)
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
                case EquipmentOptionStat.AttackPoint:
                case EquipmentOptionStat.HealthPoint:
                case EquipmentOptionStat.DefencePoint:
                case EquipmentOptionStat.Speed:
                    // 일반 수치를 보여준다.
                    valueText.text = value.ToString();
                    break;
                case EquipmentOptionStat.AttackPercent:
                case EquipmentOptionStat.HealthPercent:
                case EquipmentOptionStat.DefencePercent:
                case EquipmentOptionStat.CriticalPercent:
                case EquipmentOptionStat.CriticalDamagePercent:
                case EquipmentOptionStat.EffectHitPercent:
                case EquipmentOptionStat.EffectResistancePercent:
                    // % 수치로 보여준다. 소수점 첫번째자리 까지 보여준다.
                    valueText.text = (value * 100f).ToString("F1") + "%";
                    break;
            }
        }



    }
}