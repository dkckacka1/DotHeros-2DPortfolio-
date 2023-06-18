using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Portfolio.Lobby.Hero
{
    public class SkillLevelUpResultUI : MonoBehaviour
    {
        [SerializeField] UnitSkillUI skillUI;
        [SerializeField] TextMeshProUGUI prevSKillLevelText;
        [SerializeField] TextMeshProUGUI afterSkillLevelText;


        public void Show(int prevSkillLevel)
        {
            Debug.Log(HeroPanelUI.SelectSkillType);
            Debug.Log(prevSkillLevel + HeroPanelUI.SelectSkill.GetData.skillName + HeroPanelUI.SelectSkillLevel);
            prevSKillLevelText.text = $"스킬 레벨 {prevSkillLevel}";
            afterSkillLevelText.text = $"스킬 레벨 {HeroPanelUI.SelectSkillLevel}";
            skillUI.Init(HeroPanelUI.SelectSkill, HeroPanelUI.SelectSkillLevel, false);
        }
    }
}