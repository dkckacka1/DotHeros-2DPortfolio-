using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유닛 릴리의 기본 공격 스킬 클래스
 */

namespace Portfolio.skill
{
    public class Skill_LILY_BaseAttack : ActiveSkill, ISingleTarget
    {
        public Skill_LILY_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }
        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // 적중 체력이 낮은 순으로 1명 타겟
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // 스킬 데미지
            float skillDamage = e.actionUnit.AttackPoint * 0.75f;
            GameManager.AudioManager.PlaySoundOneShot("Sound_Skill_LILY_BaseAttack");

            foreach (var targetUnit in e.targetUnits)
            {
                // 적에게 피해를 입히고 이펙트 출력
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_LILY_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            yield return new WaitForSeconds(1f);
            // 스킬 종료
            e.actionUnit.isSkillUsing = false;
        }
    }
}