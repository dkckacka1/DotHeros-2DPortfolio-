using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Portfolio
{
    public class BattleSkillDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI skillNameText;
        [SerializeField] TextMeshProUGUI skillManaText;
        [SerializeField] TextMeshProUGUI skillCoolTimeText;
        [SerializeField] TextMeshProUGUI skillDescText;

        public void ShowSkillDesc(ActiveSkill skill)
        {
            skillNameText.text = $"{skill.GetData.skillName}";
            skillManaText.text = $"";
            skillCoolTimeText.text = $"";
            skillDescText.text = skill.GetDesc(1);
        }

        public void ShowSkillDesc(ActiveSkill skill, int skillLevel)
        {
            skillNameText.text = $"{skill.GetData.skillName} (LV{skillLevel})";
            skillManaText.text = $"�Һ� ���� : {skill.GetData.consumeManaValue}";
            skillCoolTimeText.text = $"��ų ��Ÿ�� : {skill.GetData.skillCoolTime}";
            skillDescText.text = skill.GetDesc(skillLevel);
        }
    }
}