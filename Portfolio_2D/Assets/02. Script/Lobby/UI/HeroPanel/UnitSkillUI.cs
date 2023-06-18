using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class UnitSkillUI : MonoBehaviour
    {
        [HideInInspector] public UnitSkillType SkillType;
        Skill skill;
        int skillLevel;

        [SerializeField] Image skillImage;
        [SerializeField] TextMeshProUGUI skillTypeText;
        [SerializeField] TextMeshProUGUI skillLevelText;
        [SerializeField] TextMeshProUGUI skillNameText;
        [SerializeField] TextMeshProUGUI skillDescText;
        [SerializeField] Button skillLevelUpBtn;

        public void Init(Skill skill, int skillLevel, bool showSkillLevelUpBtn = true, UnitSkillType unitSkillType = UnitSkillType.ActiveSkill_1)
        {
            if (skill == null)
            {
                this.gameObject.SetActive(false);
                return;
            }

            this.skill = skill;
            this.SkillType = unitSkillType;
            this.skillLevel = HeroPanelUI.SelectUnit.GetSkillLevel(unitSkillType);
            this.gameObject.SetActive(true);
            skillImage.sprite = skill.skillSprite;
            skillTypeText.text = (skill.GetData.skillType == Portfolio.SkillType.ActiveSkill) ? "액티브 스킬" : "패시브 스킬";
            skillLevelText.text = "레벨 " + skillLevel.ToString();
            skillNameText.text = skill.GetData.skillName;
            skillDescText.text = skill.GetDesc(skillLevel);
            if (showSkillLevelUpBtn && skillLevel < 5)
            {
                skillLevelUpBtn.gameObject.SetActive(true);
            }
            else
            {
                skillLevelUpBtn.gameObject.SetActive(false);
            }
        }

        public void HeroPanelSelectSkill()
        {
            HeroPanelUI.SelectSkill = skill;
            HeroPanelUI.SelectSkillType = SkillType;
        }
    }
}