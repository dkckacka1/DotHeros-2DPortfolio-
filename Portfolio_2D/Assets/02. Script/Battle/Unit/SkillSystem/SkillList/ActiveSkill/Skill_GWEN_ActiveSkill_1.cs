using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_GWEN_ActiveSkill_1 : ActiveSkill
    {
        public Skill_GWEN_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}