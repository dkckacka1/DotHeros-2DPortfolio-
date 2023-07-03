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
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderFrontLineNLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            float skillDamage = e.actionUnit.AttackPoint * (e.skillLevel * GetData.skillLevelValue_1 * 0.01f);
            foreach (var targetUnit in e.targetUnits)
            {
                for (int i = 0; i < 7; i++)
                {
                    yield return new WaitForSeconds(0.15f);
                    e.actionUnit.HitTarget(targetUnit, skillDamage);
                }
            }
            yield return new WaitForSeconds(1f);
            e.actionUnit.isSkillUsing = false;
        }
    }
}