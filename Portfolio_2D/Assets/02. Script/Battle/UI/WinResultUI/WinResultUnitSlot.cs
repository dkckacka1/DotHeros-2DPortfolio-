using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 전투 승리창에 표시될 유닛 슬롯 UI 클래스
 * 경험치바가 추가로 있다.
 */

namespace Portfolio.Battle
{
    public class WinResultUnitSlot : MonoBehaviour
    {
        [SerializeField] UnitSlotUI unitSlotUI;             // 유닛 슬롯 UI
        [SerializeField] Slider unitExperienceSlider;       // 유닛 경험치 바
        [SerializeField] TextMeshProUGUI getExperienceText; // 경험치 텍스트

        int unitLevel;                                      // 유닛 레벨
        int unitGrade;                                      // 유닛 등급

        // 유닛의 정보, 레벨과 경험치를 표시한다.
        public void InitSlot(Unit unit,float getExperience)
        {
            unitSlotUI.ShowUnit(unit);
            unitLevel = unit.UnitCurrentLevel;
            unitGrade = unit.UnitGrade;
            unitExperienceSlider.maxValue = unit.MaxExperience;
            unitExperienceSlider.value = unit.CurrentExperience;
            getExperienceText.text = getExperience.ToString("N0");
        }
    }
}