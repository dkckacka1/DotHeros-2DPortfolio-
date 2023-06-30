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

            foreach (var targetUnit in e.targetUnits)
            {
                e.actionUnit.HitTarget(targetUnit, e.actionUnit.AttackPoint);
            }

            e.actionUnit.isSkillUsing = false;
        }
    }

}