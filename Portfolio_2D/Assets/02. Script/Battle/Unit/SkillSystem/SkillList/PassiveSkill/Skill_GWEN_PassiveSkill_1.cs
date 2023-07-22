using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유닛 그웬의 패시브 스킬 1 클래스
 */

namespace Portfolio.skill
{
    public class Skill_GWEN_PassiveSkill_1 : PassiveSkill
    {
        public Skill_GWEN_PassiveSkill_1(PassiveSkillData skillData) : base(skillData)
        {
        }

        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
            // 유닛이 피해를 입힐때 피해입은 유닛과 효과 판단해서 성공 시 독중첩을 1부여한다.
            e.actionUnit.OnHitTargetEvent += (object sender, HitEventArgs s) =>
            {
                float effectHit = s.actionUnit.EffectHit + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f);
                float effectRes = s.targetUnit.EffectResistance;
                if (GameLib.ProbabilityCalculation(effectHit - effectRes, 1f))
                {
                    s.targetUnit.AddCondition(GetData.conditinID_1, conditionList[0], 1);
                }
            };
        }
    }
}