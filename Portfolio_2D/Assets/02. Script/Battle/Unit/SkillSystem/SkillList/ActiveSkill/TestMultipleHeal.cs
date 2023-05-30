using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class TestMultipleHeal : ActiveSkill
    {
        public TestMultipleHeal(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, EventArgs e)
        {
            Debug.Log("±¤¿ªÈú");
            if (!TryGetSkillActionArgs(e, out SkillActionEventArgs args))
            {
                return;
            }

            args.targetUnit.Heal(args.actionUnit.AttackPoint);
        }
    }
}
