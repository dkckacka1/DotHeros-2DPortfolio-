using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 아바타 유닛의 기본 공격 스킬 클래스
 */

namespace Portfolio.skill
{
    public class Skill_AVATA_BaseAttack : ActiveSkill
    {
        public Skill_AVATA_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // 체력이 낮은 적순으로 1명 타겟
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // 스킬 데미지
            float skillDamage = e.actionUnit.AttackPoint * 0.4f;
            // 잠시 쉬고
            yield return new WaitForSeconds(0.1f);
            foreach(var targetUnit in e.targetUnits)
            {
                // 타겟에게 데미지를 입히고 이펙트 출력
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_AVATA_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            // 잠시 쉬고
            yield return new WaitForSeconds(0.2f);
            foreach (var targetUnit in e.targetUnits)
            {
                // 타겟에게 데미지를 입히고 이펙트 출력
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_AVATA_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            // 잠시 쉬고
            yield return new WaitForSeconds(0.2f);
            foreach (var targetUnit in e.targetUnits)
            {
                // 타겟에게 데미지를 입히고 이펙트 출력
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_AVATA_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            // 잠시 쉬고
            yield return new WaitForSeconds(0.1f);
            // 스킬 종료
            e.actionUnit.isSkillUsing = false;
        }
    }
}
