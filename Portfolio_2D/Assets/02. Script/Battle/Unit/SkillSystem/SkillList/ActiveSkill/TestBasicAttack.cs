using Portfolio.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class TestBasicAttack : ActiveSkill
    {
        public TestBasicAttack(ActiveSkillData skillData) : base(skillData)
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
                targetUnit.TakeDamage(args.actionUnit.AttackPoint);
            }
        }
    }

}