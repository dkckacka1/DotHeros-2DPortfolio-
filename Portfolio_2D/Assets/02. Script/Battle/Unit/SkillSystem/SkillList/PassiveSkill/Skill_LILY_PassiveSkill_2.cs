using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_LILY_PassiveSkill_2 : PassiveSkill
    {
        public Skill_LILY_PassiveSkill_2(SkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);
            var targetUnits = BattleManager.ActionSystem.GetLiveUnit.GetAllyTarget(e.actionUnit).GetLowHealth().GetTargetNum(5);
            foreach (var targetUnit in targetUnits)
            {
                if (targetUnit != null)
                {
                    float healValue = targetUnit.MaxHP * (0.25f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
                    e.actionUnit.HealTarget(targetUnit, healValue);
                }
            }
        }
    }

}