using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_AVATA_PassiveSkill_1 : PassiveSkill
    {
        public Skill_AVATA_PassiveSkill_1(SkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);

            foreach(var targetUnit in e.targetUnits)
            {
                targetUnit.EffectHit += (e.skillLevel * GetData.skillLevelValue_1 * 0.01f);
            }
        }
    }

}