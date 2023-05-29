using Portfolio.condition;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class TestActiveSkillFirst : ActiveSkill
    {
        public TestActiveSkillFirst(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, EventArgs e)
        {
            if (!TryGetSkillActionArgs(e, out SkillActionEventArgs args))
            {
                return;
            }

            args.targetUnit.TakeDamage(args.actionUnit.AttackPoint * 0.5f);
        }
    }
}