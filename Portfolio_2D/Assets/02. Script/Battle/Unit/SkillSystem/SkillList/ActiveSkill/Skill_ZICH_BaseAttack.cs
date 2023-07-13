using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * 유닛 지크의 기본 공격 스킬 클래스
 */

namespace Portfolio.skill
{
    public class Skill_ZICH_BaseAttack : ActiveSkill, ISingleTarget
    {
        public Skill_ZICH_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }
        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // 체력이 가장 낮은 적 순으로 1명 타겟
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(GetData.targetNum).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            float skillDamage = e.actionUnit.AttackPoint;
            foreach (var targetUnit in e.targetUnits)
            {
                // 대상 타격
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                // 대상 위치에 이펙트 출력
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_ZICH_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            yield return new WaitForSeconds(0.5f);
            // 스킬 종료
            e.actionUnit.isSkillUsing = false;
        }
    }
}