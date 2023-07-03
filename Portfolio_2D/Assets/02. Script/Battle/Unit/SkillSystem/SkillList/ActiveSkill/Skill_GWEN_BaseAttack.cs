using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_GWEN_BaseAttack : ActiveSkill
    {
        public Skill_GWEN_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            var targetList = e.targetUnits.ToList();

            float skillDamage = e.actionUnit.AttackPoint * 0.8f;

            e.actionUnit.HitTarget(targetList[0], skillDamage);
            yield return new WaitForSeconds(0.5f);
            if (targetList[0].IsDead )
            {
                BattleManager.ManaSystem.AddMana(1);
                if (targetList.Count > 1)
                {
                    e.actionUnit.HitTarget(targetList[1], skillDamage);
                }
            }

            yield return new WaitForSeconds(1f);
            e.actionUnit.isSkillUsing = false;
        }
    }

}