using Portfolio.condition;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class TestActiveSkill : ActiveSkill
    {
        public TestActiveSkill(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, EventArgs e)
        {
            if (!TryGetSkillActionArgs(e, out SkillActionEventArgs args))
            {
                return;
            }

            if (GameManager.Instance.TryGetCondition(GetData.conditinID_1, out Condition condition))
            {
                args.targetUnit.AddCondition(GetData.conditinID_1, condition, 3);
            }
        }
    }

}