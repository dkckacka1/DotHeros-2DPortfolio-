using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 스킬 설명을 표시할 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleSkillDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI skillNameText;         // 스킬 이름 텍스트
        [SerializeField] TextMeshProUGUI skillManaText;         // 스킬 마나량 텍스트
        [SerializeField] TextMeshProUGUI skillCoolTimeText;     // 스킬 쿨타임 텍스트
        [SerializeField] TextMeshProUGUI skillDescText;         // 스킬 설명 텍스트
        [SerializeField] List<BattleConditionDescUI> conditionDescUIList; // 스킬의 상태이상 설명

        // 기본 공격 스킬이 들어올 경우
        public void ShowSkillDesc(ActiveSkill skill)
        {
            // 이름과 스킬 설명(레벨 1 고정)만 보여준다.
            skillNameText.text = $"{skill.GetData.skillName}";
            skillManaText.text = $"";
            skillCoolTimeText.text = $"";
            skillDescText.text = skill.GetDesc(1);

            // 상태이상 설명 표시
            for (int i = 0; i < 3; i++)
            {
                conditionDescUIList[i].Show(skill.conditionList[i]);
            }
        }

        // 액티브 스킬이 들어올 경우
        public void ShowSkillDesc(ActiveSkill skill, int skillLevel)
        {
            // 소비 마나량과 스킬 쿨타임을 추가로 보여준다.
            skillNameText.text = $"{skill.GetData.skillName} (LV{skillLevel})";
            skillManaText.text = $"소비 마나 : {skill.GetData.consumeManaValue}";
            skillCoolTimeText.text = $"스킬 쿨타임 : {skill.GetActiveSkillCooltime(skillLevel)}";
            skillDescText.text = skill.GetDesc(skillLevel);

            // 상태이상 설명 표시
            for (int i = 0; i < 3; i++)
            {
                conditionDescUIList[i].Show(skill.conditionList[i]);
            }
        }
    }
}