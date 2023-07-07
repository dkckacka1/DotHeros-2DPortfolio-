using Portfolio.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class UnitStatusUI : MonoBehaviour
    {
        [SerializeField] Image unitPortraitImage;
        [SerializeField] TextMeshProUGUI unitNameText;
        [SerializeField] TextMeshProUGUI unitGradeText;
        [SerializeField] TextMeshProUGUI unitCurrentLevelText;
        [SerializeField] TextMeshProUGUI unitMaxLevelText;
        [SerializeField] Slider unitExperienceSlider;
        [SerializeField] TextMeshProUGUI unitExperienceText;
        [SerializeField] RectTransform potionSlot;

        [Header("유닛 프로퍼티")]
        [SerializeField] TextMeshProUGUI unitAttackPointText;
        [SerializeField] TextMeshProUGUI unitHealthPointText;
        [SerializeField] TextMeshProUGUI unitDefencePointText;
        [SerializeField] TextMeshProUGUI unitSpeedText;
        [SerializeField] TextMeshProUGUI unitCriticalPercentText;
        [SerializeField] TextMeshProUGUI unitCriticalDamageText;
        [SerializeField] TextMeshProUGUI unitEffectHitText;
        [SerializeField] TextMeshProUGUI unitEffectResText;
        [SerializeField] ItemSlotUI[] experiencePotionSlots;

        public void Init()
        {
            LobbyManager.UIManager.unitChangedEvent += ShowStat;
        }

        public void ShowStat(object sender, EventArgs eventArgs)
        {
            Unit unit = HeroPanelUI.SelectUnit;
            if (unit == null) return;

            unitPortraitImage.sprite = unit.portraitSprite;
            unitNameText.text = unit.UnitName;
            unitGradeText.text = unit.UnitGrade.ToString() + " 성";
            unitCurrentLevelText.text = unit.UnitCurrentLevel.ToString();
            unitMaxLevelText.text = "/ " + unit.UnitMaxLevel.ToString();
            unitExperienceSlider.value = unit.CurrentExperience / unit.MaxExperience;
            unitExperienceText.text = (unit.CurrentExperience / unit.MaxExperience * 100f).ToString("N1") + " %";

            unitAttackPointText.text = unit.AttackPoint.ToString("N0");
            unitHealthPointText.text = unit.HealthPoint.ToString("N0");
            unitDefencePointText.text = unit.DefencePoint.ToString("N0");
            unitSpeedText.text = unit.Speed.ToString("N0");
            unitCriticalPercentText.text = (unit.CriticalPercent * 100f).ToString("F1") + " %";
            unitCriticalDamageText.text = (unit.CriticalDamage * 100f).ToString("F1") + " %";
            unitEffectHitText.text = (unit.EffectHit * 100f).ToString("F1") + " %";
            unitEffectResText.text = (unit.EffectResistance * 100f).ToString("F1") + " %";

            for (int i = 0; i < experiencePotionSlots.Length; i++)
            {
                experiencePotionSlots[i].ShowItem();
            }
        }
    }
}