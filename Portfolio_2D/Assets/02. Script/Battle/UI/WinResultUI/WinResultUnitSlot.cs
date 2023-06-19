using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Battle
{
    public class WinResultUnitSlot : MonoBehaviour
    {
        [SerializeField] UnitSlotUI unitSlotUI;
        [SerializeField] Slider unitExperienceSlider;
        [SerializeField] TextMeshProUGUI getExperienceText;

        public void InitSlot(Unit unit,float getExperience)
        {
            unitSlotUI.Init(unit);
            unitExperienceSlider.maxValue = unit.MaxExperience;
            unitExperienceSlider.value = unit.CurrentExperience;
            getExperienceText.text = getExperience.ToString("N0");
        }
    }
}