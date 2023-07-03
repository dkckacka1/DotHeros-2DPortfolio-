using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_GWEN_PassiveSkill_1 : PassiveSkill
    {
        public Skill_GWEN_PassiveSkill_1(SkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);
        }

        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
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