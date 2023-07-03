using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_GWEN_ActiveSkill_2 : ActiveSkill
    {
        public Skill_GWEN_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> grids)
        {
            return grids.GetAllyTarget(actionUnit).GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            int conditionTime = 1 + (e.skillLevel);
            foreach (var targetUnit in e.targetUnits)
            {
                targetUnit.AddCondition(GetData.conditinID_1, conditionList[0], conditionTime);
            }
            yield return new WaitForSeconds(1f);
            e.actionUnit.isSkillUsing = false;
        }
    }

}