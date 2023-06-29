using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_ZICH_PassiveSkill_1 : PassiveSkill
    {
        public Skill_ZICH_PassiveSkill_1(SkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);
            if (!TryGetSkillActionArgs(e, out SkillActionEventArgs args))
            {
                return;
            }


            e.actionUnit.AttackPoint *= (1 + (0.2f * e.skillLevel));
        }
    }

}