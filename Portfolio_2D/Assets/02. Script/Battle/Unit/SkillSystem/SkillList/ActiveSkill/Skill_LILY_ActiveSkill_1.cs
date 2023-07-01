using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_LILY_ActiveSkill_1 : ActiveSkill
    {
        public Skill_LILY_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);

            foreach (var targetUnit in e.targetUnits)
            {
                float healValue = targetUnit.MaxHP * (0.3f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
                e.actionUnit.HealTarget(targetUnit, healValue);
            }
            e.actionUnit.isSkillUsing = false;
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<BattleUnit> targetUnits)
        {
            return targetUnits.GetAllyTarget(actionUnit).GetLowHealth().GetTargetNum(this);
        }
    }

}