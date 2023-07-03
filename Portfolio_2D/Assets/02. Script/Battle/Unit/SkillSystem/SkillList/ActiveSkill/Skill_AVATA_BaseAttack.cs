using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_AVATA_BaseAttack : ActiveSkill
    {
        public Skill_AVATA_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            float skillDamage = e.actionUnit.AttackPoint * 0.4f;
            yield return new WaitForSeconds(0.1f);
            foreach(var targetUnit in e.targetUnits)
            {
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_AVATA_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            yield return new WaitForSeconds(0.2f);
            foreach (var targetUnit in e.targetUnits)
            {
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_AVATA_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            yield return new WaitForSeconds(0.2f);
            foreach (var targetUnit in e.targetUnits)
            {
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_AVATA_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            yield return new WaitForSeconds(0.1f);
            e.actionUnit.isSkillUsing = false;
        }
    }
}
