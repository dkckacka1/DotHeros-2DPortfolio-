using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_AVATA_ActiveSkill_2 : ActiveSkill
    {
        public Skill_AVATA_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override int GetActiveSkillCooltime(int skillLevel)
        {
            return base.GetActiveSkillCooltime(skillLevel) - skillLevel;
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