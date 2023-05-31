using Portfolio.Battle;
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

        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);
            if (!TryGetSkillActionArgs(e, out SkillActionEventArgs args))
            {
                return;
            }

            foreach (var targetUnit in args.targetUnits)
            {
                targetUnit.Heal(args.actionUnit.AttackPoint * 1.5f);
            }
        }
    }
}
