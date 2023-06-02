using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class UnitStatusUI : MonoBehaviour
    {
        private Unit unit;

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

        public void ShowUnit(Unit unit)
        {
            this.unit = unit;
            unitNameText.text = unit.Data.unitName;
            unitGradeText.text = unit.UnitGrade.ToString() + " 성";
            unitCurrentLevelText.text = unit.UnitCurrentLevel.ToString();
            unitMaxLevelText.text = "/ " + unit.UnitMaxLevel.ToString();
            unitExperienceSlider.value = unit.CurrentExperience / unit.MaxExperience;
            unitExperienceText.text = (unit.CurrentExperience / unit.MaxExperience * 100f).ToString("N1") + " %";

            unitAttackPointText.text = unit.AttackPoint.ToString();
            unitHealthPointText.text = unit.HealthPoint.ToString();
            unitDefencePointText.text = unit.DefencePoint.ToString();
            unitSpeedText.text = unit.Speed.ToString();
            unitCriticalPercentText.text = (unit.CriticalPercent * 100f).ToString() + " %";
            unitCriticalDamageText.text = (unit.CriticalDamage * 100f).ToString() + " %";
            unitEffectHitText.text = (unit.EffectHit * 100f).ToString() + " %";
            unitEffectResText.text = (unit.EffectResistance * 100f).ToString() + " %";
        }

        public void ShowEquipment(UnitEquipmentUI equipmentUI)
        {
            equipmentUI.Init(this.unit);
            equipmentUI.gameObject.SetActive(true);
        }
    }

}