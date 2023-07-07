using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ���� �¸�â�� ǥ�õ� ���� ���� UI Ŭ����
 * ����ġ�ٰ� �߰��� �ִ�.
 */

namespace Portfolio.Battle
{
    public class WinResultUnitSlot : MonoBehaviour
    {
        [SerializeField] UnitSlotUI unitSlotUI;             // ���� ���� UI
        [SerializeField] Slider unitExperienceSlider;       // ���� ����ġ ��
        [SerializeField] TextMeshProUGUI getExperienceText; // ����ġ �ؽ�Ʈ

        int unitLevel;                                      // ���� ����
        int unitGrade;                                      // ���� ���

        // ������ ����, ������ ����ġ�� ǥ���Ѵ�.
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