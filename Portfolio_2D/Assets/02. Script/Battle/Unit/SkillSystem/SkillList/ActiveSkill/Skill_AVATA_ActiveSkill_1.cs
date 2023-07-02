using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_AVATA_ActiveSkill_1 : ActiveSkill
    {
        public Skill_AVATA_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<BattleUnit> targetUnits)
        {
            return targetUnits.GetEnemyTarget(actionUnit).GetLowHealth().GetTargetNum(this);
        }

        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);
            e.actionUnit.isSkillUsing = false;
        }
    }
}