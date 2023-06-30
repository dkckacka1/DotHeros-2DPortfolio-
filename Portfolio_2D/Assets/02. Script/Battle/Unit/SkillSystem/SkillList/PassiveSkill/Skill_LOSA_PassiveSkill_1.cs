using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_LOSA_PassiveSkill_1 : PassiveSkill
    {
        public Skill_LOSA_PassiveSkill_1(SkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);
            if (!TryGetSkillActionArgs(e, out SkillActionEventArgs args))
            {
                return;
            }

            e.actionUnit.CriticalDamage *= 1 + (0.25f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
        }
    }

}
