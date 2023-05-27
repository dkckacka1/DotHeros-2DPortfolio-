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

        public override void Action(object sender, EventArgs e)
        {
            Debug.Log("�⺻����!");
            if (!TryGetSkillActionArgs(e, out SkillActionEventArgs args))
            {
                return;
            }

            args.targetUnit.TakeDamage(args.actionUnit.AttackPoint);
        }
    }

}