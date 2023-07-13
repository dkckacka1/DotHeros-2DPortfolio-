using Portfolio.Battle;
using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유닛 로사의 기본 공격 스킬 클래스
 */

namespace Portfolio.skill
{
    public class Skill_LOSA_BaseAttack : ActiveSkill, ISingleTarget
    {
        public Skill_LOSA_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // 체력이 가장 낮은 적 순으로 1명 타겟
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // 스킬 데미지
            float skillDamage = e.actionUnit.AttackPoint * 0.8f;

            foreach (var targetUnit in e.targetUnits)
            {
                // 대상을 타격
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                // 대상에게 낙인 상태이상 부여
                targetUnit.AddCondition(conditionList[0].conditionID, conditionList[0], 2);
                // 이펙트 출력
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_LOSA_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            yield return new WaitForSeconds(0.5f);
            // 스킬 종료
            e.actionUnit.isSkillUsing = false;
        }
    }
}

