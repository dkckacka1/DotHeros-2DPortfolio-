using DG.Tweening;
using Portfolio.Battle;
using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유닛 로사의 액티브 스킬 2클래스
 */

namespace Portfolio.skill
{
    public class Skill_LOSA_ActiveSkill_2 : ActiveSkill
    {
        float arrowProjectileTime = 0.5f; // 화살이 날아 가는 시간

        public Skill_LOSA_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // 최대 적군 5명 타겟
            return targetUnits.GetEnemyTarget(actionUnit, this).GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // 스킬 데미지
            float skillDamage = e.actionUnit.AttackPoint * (1 + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));

            // 화살을 쏘는 모션 나올때까지 대기
            yield return new WaitForSeconds(0.45f);
            foreach (var targetUnit in e.targetUnits)
            {
                // 이펙트 생성
                SkillEffect effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectile");
                // 화살이 나오는 위치 정의
                Vector3 arrowPos;
                if (e.actionUnit.IsEnemy)
                    // 사용자가 적이면
                {
                    // 로컬 스케일을 변경해 화살을 뒤집어 준다.
                    effect.transform.localScale = new Vector3(-1, 1, 1);
                    // 화살이 위를 보도록
                    effect.transform.rotation = Quaternion.Euler(0, 0, -60);
                    arrowPos = e.actionUnit.projectilePos.position + new Vector3(0, 2);
                }
                else
                    // 사용자가 아군이면
                {
                    // 그대로
                    effect.transform.localScale = Vector3.one;
                    // 화살이 위를 보도록
                    effect.transform.rotation = Quaternion.Euler(0, 0, 60);
                    arrowPos = e.actionUnit.projectilePos.position + new Vector3(0, 2);
                }

                // 화살을 위치시켜준다.
                effect.transform.position = arrowPos;
                // 화살이 하늘로 이동하도록
                effect.transform.DOMove(new Vector3(-2, 10), arrowProjectileTime).SetEase(Ease.InOutSine).OnComplete(() =>
                    // 이동이 끝났으면
                {
                    if (e.actionUnit.IsEnemy)
                        // 대상을 향해 날아가되 대상이 아군이냐 적에 따라 회전값 변경
                    {
                        effect.transform.rotation = Quaternion.Euler(0, 0, 60);
                    }
                    else
                    {
                        effect.transform.rotation = Quaternion.Euler(0, 0, -60);
                    }
                    effect.transform.DOMove(targetUnit.transform.position, arrowProjectileTime).OnComplete(() =>
                    {
                        // 대상에게 꽂혔다면
                        // 꽂힌 이펙트 표시
                        effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectileHit");

                        if (targetUnit.HasCondition(conditionList[0]))
                        {
                            // 낙인 상태이상이 있다면 확정 치명타
                            e.actionUnit.HitTarget(targetUnit, skillDamage, true);
                        }
                        else
                        {
                            // 없다면 일반 공격 하고 낙인 상태이상 부여
                            e.actionUnit.HitTarget(targetUnit, skillDamage);
                            targetUnit.AddCondition(conditionList[0].conditionID, conditionList[0], 2);
                        }

                        // 스킬 종료
                        e.actionUnit.isSkillUsing = false;
                    });

                });
            }
        }
    }
}