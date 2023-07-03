using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_GWEN_BaseAttack : ActiveSkill
    {
        public Skill_GWEN_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<BattleUnit> targetUnits)
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }

}