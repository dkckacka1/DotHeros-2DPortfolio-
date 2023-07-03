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

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            float skillDamage = e.actionUnit.AttackPoint * (0.5f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));

            foreach (var targetUnit in e.targetUnits)
            {
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                targetUnit.AddCondition(GetData.conditinID_1, conditionList[0], 3);
            }

            yield return new WaitForSeconds(0.5f);

            e.actionUnit.isSkillUsing = false;
        }
    }
}