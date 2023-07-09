using Portfolio.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 유닛의 스텟창을 보여주는 패널 UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class UnitStatusUI : MonoBehaviour
    {
        [SerializeField] Image unitPortraitImage;                   // 유닛 포트레이트 이미지
        [SerializeField] TextMeshProUGUI unitBattlePowerText;       // 유닛 전투력 텍스트
        [SerializeField] TextMeshProUGUI unitNameText;              // 유닛 이름 텍스트
        [SerializeField] TextMeshProUGUI unitGradeText;             // 유닛의 등급 텍스트
        [SerializeField] TextMeshProUGUI unitCurrentLevelText;      // 유닛의 현재 레벨 텍스트
        [SerializeField] TextMeshProUGUI unitMaxLevelText;          // 유닛의 최대 레벨 텍스트
        [SerializeField] Slider unitExperienceSlider;               // 유닛의 경험치 슬라이더바
        [SerializeField] TextMeshProUGUI unitExperienceText;        // 유닛의 경험치양 텍스트
        [SerializeField] RectTransform potionSlot;                  // 포션 슬롯 레이아웃 오브젝트

        [Header("Unit Status")]
        [SerializeField] TextMeshProUGUI unitAttackPointText;       // 유닛의 공격력 텍스트
        [SerializeField] TextMeshProUGUI unitHealthPointText;       // 유닛의 생명력 텍스트
        [SerializeField] TextMeshProUGUI unitDefencePointText;      // 유닛의 방어력 텍스트
        [SerializeField] TextMeshProUGUI unitSpeedText;             // 유닛의 속도 텍스트
        [SerializeField] TextMeshProUGUI unitCriticalPercentText;   // 유닛의 치명타 확률 텍스트
        [SerializeField] TextMeshProUGUI unitCriticalDamageText;    // 유닛의 치명타 공격력 텍스트
        [SerializeField] TextMeshProUGUI unitEffectHitText;         // 유닛의 효과 적중률 텍스트
        [SerializeField] TextMeshProUGUI unitEffectResText;         // 유닛의 효과 저항력 텍스트
        
        [Header("ExperiencePotion")]
        [SerializeField] Toggle potionPanelToggle;                  // 포션 슬롯 토글
        [SerializeField] ItemSlotUI[] experiencePotionSlots;        // 포션 아이템 슬롯들

        private void OnEnable()
        {
            // 유저가 선택한 유닛이 변경되면 UI를 업데이트 한다.
            LobbyManager.UIManager.unitChangedEvent += ShowStat;
        }


        private void OnDisable()
        {
            // 창이 꺼질때 포션 슬롯이 켜저있다면 꺼줍니다.
            potionPanelToggle.isOn = false;
            potionPanelToggle.onValueChanged?.Invoke(false);
            LobbyManager.UIManager.unitChangedEvent -= ShowStat;
        }

        // 유저가 선택한 유닛의 정보를 표시한다.
        public void ShowStat(object sender, EventArgs eventArgs)
        {
            Unit unit = HeroPanelUI.SelectUnit;
            if (unit == null) return;

            unitBattlePowerText.text = unit.battlePower.ToString("###,###,###");
            unitPortraitImage.sprite = unit.portraitSprite;
            unitNameText.text = unit.UnitName;
            unitGradeText.text = unit.UnitGrade.ToString() + " 성";
            unitCurrentLevelText.text = unit.UnitCurrentLevel.ToString();
            unitMaxLevelText.text = "/ " + unit.UnitMaxLevel.ToString();
            unitExperienceSlider.value = unit.CurrentExperience / unit.MaxExperience;
            unitExperienceText.text = (unit.CurrentExperience / unit.MaxExperience * 100f).ToString("F1") + " %";

            unitAttackPointText.text = unit.AttackPoint.ToString("N0");
            unitHealthPointText.text = unit.HealthPoint.ToString("N0");
            unitDefencePointText.text = unit.DefencePoint.ToString("N0");
            unitSpeedText.text = unit.Speed.ToString("N0");
            // 소수점 첫번째 자리까지 표시한다.
            unitCriticalPercentText.text = (unit.CriticalPercent * 100f).ToString("F1") + " %";
            unitCriticalDamageText.text = (unit.CriticalDamage * 100f).ToString("F1") + " %";
            unitEffectHitText.text = (unit.EffectHit * 100f).ToString("F1") + " %";
            unitEffectResText.text = (unit.EffectResistance * 100f).ToString("F1") + " %";

            for (int i = 0; i < experiencePotionSlots.Length; i++)
            {
                // 경험치 포션을 업데이트 합니다.
                experiencePotionSlots[i].GetComponent<ItemSlotSelector_ItemConsum>().ShowSlot();
            }
        }

        // 포션 슬롯을 업데이트 합니다.
        public void TOGGLE_OnValueChanged_SetPotionSlot()
        {
            for (int i = 0; i < experiencePotionSlots.Length; i++)
            {
                // 경험치 포션을 업데이트 합니다.
                experiencePotionSlots[i].GetComponent<ItemSlotSelector_ItemConsum>().ShowSlot();
            }
        }
    }
}