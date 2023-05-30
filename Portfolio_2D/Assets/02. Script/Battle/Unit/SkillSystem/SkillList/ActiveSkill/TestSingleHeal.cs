using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class TestSingleHeal : ActiveSkill
    {
        public TestSingleHeal(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, EventArgs e)
        {
            base.Action(sender, e);
            if (!TryGetSkillActionArgs(e, out SkillActionEventArgs args))
            {
                return;
            }

            args.targetUnit.Heal(args.actionUnit.AttackPoint * 1.5f);
        }
    }
}
