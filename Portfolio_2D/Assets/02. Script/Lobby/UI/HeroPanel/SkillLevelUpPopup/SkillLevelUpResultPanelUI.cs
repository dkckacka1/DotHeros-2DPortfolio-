using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * ��ų������ �� ��ų������ ���â�� �����ִ� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class SkillLevelUpResultPanelUI : MonoBehaviour
    {
        [SerializeField] UnitSkillUI skillUI;                   // ��ų ���� UI
        [SerializeField] TextMeshProUGUI prevSKillLevelText;    // ���� ��ų ����
        [SerializeField] TextMeshProUGUI afterSkillLevelText;   // ������ ���� ��ų ����


        // ��ų ������ ���â�� �����ش�.
        public void Show(int prevSkillLevel)
        {
            prevSKillLevelText.text = $"��ų ���� {prevSkillLevel}";
            afterSkillLevelText.text = $"��ų ���� {HeroPanelUI.SelectSkillLevel}";
            // ���� �������� ��ų�� ������ ������ ����� �����ش�.
            skillUI.Show(HeroPanelUI.SelectSkill, HeroPanelUI.SelectSkillLevel, false);
        }
    }
}