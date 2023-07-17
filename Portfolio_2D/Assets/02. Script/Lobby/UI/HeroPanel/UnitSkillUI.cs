using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
 * 스킬의 정보를 표시해주는 UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class UnitSkillUI : MonoBehaviour
    {
        [HideInInspector] public eUnitSkillType SkillType;   // 이 스킬 슬롯의 스킬 타입
        Skill skill;                                        // 유닛의 스킬
        int skillLevel;                                     // 해당 스킬의 레벨

        [SerializeField] Image skillImage;                  // 스킬의 이미지
        [SerializeField] TextMeshProUGUI skillTypeText;     // 스킬 타입 텍스트
        [SerializeField] TextMeshProUGUI skillLevelText;    // 스킬 레벨 텍스트
        [SerializeField] TextMeshProUGUI skillNameText;     // 스킬 이름 텍스트
        [SerializeField] TextMeshProUGUI skillDescText;     // 스킬 설명 텍스트
        [SerializeField] Button skillLevelUpBtn;            // 스킬 레벨업 버튼

        // 스킬을 보여줍니다.
        public void Show(Skill skill, int skillLevel, bool showSkillLevelUpBtn = true, eUnitSkillType unitSkillType = eUnitSkillType.ActiveSkill_1)
        {
            // 스킬이 없다면 비활성화 합니다.
            if (skill == null)
            {
                this.gameObject.SetActive(false);
                return;
            }

            // 들어온 스킬을 입력합니다.
            this.skill = skill;
            this.SkillType = unitSkillType;
            // 현재 슬롯의 스킬 타입에 맞는 현재 유저가 선택한 유닛 스킬 레벨을 가져옵니다.
            this.skillLevel = HeroPanelUI.SelectUnit.GetSkillLevel(unitSkillType);
            this.gameObject.SetActive(true);
            // 스킬의 정보를 표시합니다.
            skillImage.sprite = skill.skillSprite;
            skillTypeText.text = (skill.GetData.skillType == Portfolio.eSkillType.ActiveSkill) ? "액티브 스킬" : "패시브 스킬";
            skillLevelText.text = "레벨 " + skillLevel.ToString();
            skillNameText.text = skill.GetData.skillName;
            skillDescText.text = skill.GetDesc(skillLevel);

            // 스킬레벨이 5레벨이 아니면 스킬 레벨업 버튼을 표시합니다.
            if (showSkillLevelUpBtn && skillLevel < 5)
            {
                skillLevelUpBtn.gameObject.SetActive(true);
            }
            else
            {
                skillLevelUpBtn.gameObject.SetActive(false);
            }
        }

        public void BTN_OnClick_HeroPanelSelectSkill()
        {
            HeroPanelUI.SelectSkill = skill;
            HeroPanelUI.SelectSkillType = SkillType;
        }
    }
}