using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class UnitStatusUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI unitNameText;
        [SerializeField] TextMeshProUGUI unitGradeText;
        [SerializeField] TextMeshProUGUI unitCurrentLevelText;
        [SerializeField] TextMeshProUGUI unitMaxLevelText;
        [SerializeField] Slider unitExperienceSlider;
        [SerializeField] TextMeshProUGUI unitExperienceText;

        [Header("유닛 프로퍼티")]
        [SerializeField] TextMeshProUGUI unitAttackPointText;
        [SerializeField] TextMeshProUGUI unitHealthPointText;
        [SerializeField] TextMeshProUGUI unitDefencePointText;
        [SerializeField] TextMeshProUGUI unitSpeedText;
        [SerializeField] TextMeshProUGUI unitCriticalPercentText;
        [SerializeField] TextMeshProUGUI unitCriticalDamageText;
        [SerializeField] TextMeshProUGUI unitEffectHitText;
        [SerializeField] TextMeshProUGUI unitEffectResText;

        public void ShowStat(Unit unit)
        {
            unitNameText.text = unit.Data.unitName;
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
        }


    }

}