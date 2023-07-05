using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

/*
 * 유닛 로사의 액티브 스킬 1 클래스
 */

namespace Portfolio.skill
{
    public class Skill_LOSA_ActiveSkill_1 : ActiveSkill
    {
        float arrowProjectileTime = 0.5f; // 화살이 날아가는 시간

        public Skill_LOSA_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // 스킬 데미지
            float skillDamage = e.actionUnit.AttackPoint * (1 + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));

            // 화살을 쏘는 모션 나올때까지 대기
            yield return new WaitForSeconds(0.45f);
            foreach (var targetUnit in e.targetUnits)
            {
                // 화살 이펙트 출력
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectile");
                // 화살이 나오는 위치 정의
                Vector3 arrowPos;
                if (e.actionUnit.IsEnemy)
                    // 사용자가 적이면
                {
                    // 로컬 스케일을 변경해 화살을 뒤집어 준다.
                    effect.transform.localScale = new Vector3(-1, 1, 1);
                    arrowPos = e.actionUnit.projectilePos.position + new Vector3(-1.2f, 0);
                }
                else
                    // 사용자가 아군이면
                {
                    // 그대로
                    effect.transform.localScale = Vector3.one;
                    arrowPos = e.actionUnit.projectilePos.position + new Vector3(1.2f, 0);
                }
                // 화살을 위치해주고
                effect.transform.position = arrowPos;
                // 화살을 대상의 위치까지 이동시켜준다.
                effect.transform.DOMove(targetUnit.transform.position, arrowProjectileTime).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    // 이동이 끝나면 화살이 꽂히는 이펙트를 출력해준다.
                    effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectileHit");
                    if (targetUnit.HasCondition(conditionList[0].conditionID))
                    // 낙인 상태이상을 가지고 있다면
                    {
                        // 확정 치명 공격
                        e.actionUnit.HitTarget(targetUnit, skillDamage, true);
                    }
                    else
                    // 낙인 상태이상을 가지고 있지 않다면
                    {
                        // 일반 공격
                        e.actionUnit.HitTarget(targetUnit, skillDamage);
                    }

                    // 화살이 이동했다면 스킬 종료
                    e.actionUnit.isSkillUsing = false;
                });
            }
        }
    }
}

