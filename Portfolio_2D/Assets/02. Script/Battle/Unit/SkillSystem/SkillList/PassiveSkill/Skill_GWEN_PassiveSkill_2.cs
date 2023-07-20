using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유닛 그웬의 패시브 스킬 2 클래스
 */

namespace Portfolio.skill
{
    public class Skill_GWEN_PassiveSkill_2 : PassiveSkill
    {
        public Skill_GWEN_PassiveSkill_2(PassiveSkillData skillData) : base(skillData)
        {
        }

        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
            // 피해 입을 경우 일정 확률로 자신에게 은신 상태이상 부여
            e.actionUnit.OnTakeDamagedEvent += (object sender, TakeDamageEventArgs s) =>
            {
                float effectPercent = (e.skillLevel * GetData.skillLevelValue_1 * 0.01f);
                if (GameLib.ProbabilityCalculation(effectPercent, 1f))
                {
                    e.actionUnit.AddCondition(GetData.conditinID_1, conditionList[0], 1);
                }
            };
        }
    }

}