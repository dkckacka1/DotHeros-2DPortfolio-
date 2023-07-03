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

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            return targetUnits.GetAllyTarget(actionUnit).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            foreach (var targetUnit in e.targetUnits)
            {
                float healValue = targetUnit.MaxHP * (0.3f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
                e.actionUnit.HealTarget(targetUnit, healValue);
            }

            yield return new WaitForSeconds(0.5f);

            e.actionUnit.isSkillUsing = false;
        }
    }

}