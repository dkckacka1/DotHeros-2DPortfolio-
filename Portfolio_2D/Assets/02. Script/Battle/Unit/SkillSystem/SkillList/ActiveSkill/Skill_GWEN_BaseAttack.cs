using DG.Tweening;
using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * 그웬 유닛의 기본 공격 스킬 클래스
 */

namespace Portfolio.skill
{
    public class Skill_GWEN_BaseAttack : ActiveSkill
    {
        // 이펙트 이동 시간
        float projectileMoveTime = 0.5f;

        public Skill_GWEN_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // 적군에서 가장 체력 낮은 적을 타겟으로 잡음
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            var targetList = e.targetUnits.ToList();
            if (targetList.Count > 0)
            // 타겟유닛이 존재하면
            {
                // 첫번째 타겟
                BattleUnit firstTarget = targetList[0];

                // 두번째 타겟
                BattleUnit secondTarget = null;
                if (targetList.Count > 1)
                {
                    secondTarget = targetList[1];
                }

                float skillDamage = e.actionUnit.AttackPoint * 0.8f;
                // 데미지는 공격력의 80%

                // 던지는 모션 중
                yield return new WaitForSeconds(0.2f);
                // 스킬 이펙트 생성
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_GWEN_BaseAttack_Move");
                var rotation = effect.StartCoroutine(RotationEffect(effect));
                effect.transform.position = firstTarget.transform.position;
                Vector3 projectilePos;
                if (e.actionUnit.IsEnemy)
                // 적이면 단검 방향 반대로
                {
                    effect.transform.localScale = new Vector3(-1, 1, 1);
                    projectilePos = e.actionUnit.projectilePos.position;
                }
                else
                {
                    effect.transform.localScale = Vector3.one;
                    projectilePos = e.actionUnit.projectilePos.position;
                }
                effect.transform.position = projectilePos;

                // 1초간 대상위치로 이동
                effect.transform.DOMove(firstTarget.transform.position, projectileMoveTime).OnComplete(() =>
                {
                    // 이동완료시 데미지 입힘
                    e.actionUnit.HitTarget(firstTarget, skillDamage);
                    if (targetList[0].IsDead)
                    // 첫번째 대상이 죽었을때
                    {
                        BattleManager.ManaSystem.AddMana(1);
                        // 마나 회복
                        if (secondTarget != null)
                        // 두번째 대상이 존재할 경우
                        {
                            // 단검이 가운데로 팅긴 후
                            effect.transform.DOMoveX(-1f, projectileMoveTime).SetEase(Ease.OutQuart);
                            effect.transform.DOMoveY(firstTarget.transform.position.y + 3, projectileMoveTime).SetEase(Ease.OutQuart).OnComplete(() =>
                             {
                                 // 단검이 두번째 대상에게 날아간다.
                                 effect.transform.DOMove(secondTarget.transform.position, projectileMoveTime).OnComplete(() =>
                                  {
                                      // 단검 꽂히는 이펙트 출력하고 데미지를 입힌 후 스킬 종료
                                      effect.StopCoroutine(rotation);
                                      effect.PlayEffect("Anim_Skill_Effect_GWEN_BaseAttack_Check");
                                      e.actionUnit.HitTarget(secondTarget, skillDamage);
                                      e.actionUnit.isSkillUsing = false;
                                  });
                             });
                        }
                        else
                        // 존재 안할경우
                        {
                            // 단검 꽂히는 이펙트 출력하고 스킬 종료
                            effect.StopCoroutine(rotation);
                            effect.PlayEffect("Anim_Skill_Effect_GWEN_BaseAttack_Check");
                            e.actionUnit.isSkillUsing = false;
                        }
                    }
                    else
                    // 죽지 않았다면
                    {
                        // 단검 꽂히는 이펙트 출력하고 스킬 종료
                        effect.StopCoroutine(rotation);
                        effect.PlayEffect("Anim_Skill_Effect_GWEN_BaseAttack_Check");
                        e.actionUnit.isSkillUsing = false;
                    }
                });


            }

            yield return new WaitForSeconds(1f);
        }

        // 단검을 빙글빙글 돌려주는 메서드
        IEnumerator RotationEffect(SkillEffect effect)
        {
            while (true)
            {
                // 초당 10번 회전하도록
                effect.transform.Rotate(new Vector3(0, 0, 3600 * Time.deltaTime));
                yield return null;
            }
        }
    }

}