using Portfolio.Battle;
using Portfolio.condition;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class TestPassiveSkill : PassiveSkill
    {
        public TestPassiveSkill(SkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, SkillActionEventArgs e)
        {
            if (!TryGetSkillActionArgs(e, out SkillActionEventArgs args))
            {
                return;
            }


            if (GameManager.Instance.TryGetCondition(GetData.conditinID_1, out Condition condition))
            {
                foreach (var targetUnit in args.targetUnits)
                {
                    targetUnit.AddCondition(GetData.conditinID_1, condition, 4);
                }
            }
        }
    }
}