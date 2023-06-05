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

        [Header("���� ������Ƽ")]
        [SerializeField] TextMeshProUGUI unitAttackPointText;
        [SerializeField] TextMeshProUGUI unitHealthPointText;
        [SerializeField] TextMeshProUGUI unitDefencePointText;
        [SerializeField] TextMeshProUGUI unitSpeedText;
        [SerializeField] TextMeshProUGUI unitCriticalPercentText;
        [SerializeField] TextMeshProUGUI unitCriticalDamageText;
        [SerializeField] TextMeshProUGUI unitEffectHitText;
        [SerializeField] TextMeshProUGUI unitEffectResText;

        public void Init(Unit unit)
        {
            this.unit = unit;
            ShowStat(unit);
        }

        public void ReShow()
        {
            if (unit == null) return;

            ShowStat(this.unit);
        }

        private void ShowStat(Unit unit)
        {
            unitNameText.text = unit.Data.unitName;
            unitGradeText.text = unit.UnitGrade.ToString() + " ��";
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
            if (LobbyManager.UIManager.UndoCount() >= 2)
            {
                LobbyManager.UIManager.Undo();
            }
            equipmentUI.Init(this.unit);
            equipmentUI.gameObject.SetActive(true);
        }

        public void ShowSkill(UnitSkillPanelUI unitSkillPanelUI)
        {
            if (LobbyManager.UIManager.UndoCount() >= 2)
            {
                LobbyManager.UIManager.Undo();
            }
            unitSkillPanelUI.Init(this.unit);
            unitSkillPanelUI.gameObject.SetActive(true);
        }
    }

}