using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 스킬레벨업 시 스킬레벨업 결과창을 보여주는 UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class SkillLevelUpResultPanelUI : MonoBehaviour
    {
        [SerializeField] UnitSkillUI skillUI;                   // 스킬 정보 UI
        [SerializeField] TextMeshProUGUI prevSKillLevelText;    // 이전 스킬 레벨
        [SerializeField] TextMeshProUGUI afterSkillLevelText;   // 레벨업 이후 스킬 레벨


        // 스킬 레벨업 결과창을 보여준다.
        public void Show(int prevSkillLevel)
        {
            prevSKillLevelText.text = $"스킬 레벨 {prevSkillLevel}";
            afterSkillLevelText.text = $"스킬 레벨 {HeroPanelUI.SelectSkillLevel}";
            // 현재 선택중인 스킬의 정보를 가져와 결과로 보여준다.
            skillUI.Show(HeroPanelUI.SelectSkill, HeroPanelUI.SelectSkillLevel, false);
        }
    }
}