using DG.Tweening;
using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * ���� ������ �⺻ ���� ��ų Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_GWEN_BaseAttack : ActiveSkill
    {
        // ����Ʈ �̵� �ð�
        float projectileMoveTime = 0.5f;

        public Skill_GWEN_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // �������� ���� ü�� ���� ���� Ÿ������ ����
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            var targetList = e.targetUnits.ToList();
            if (targetList.Count > 0)
            // Ÿ�������� �����ϸ�
            {
                // ù��° Ÿ��
                BattleUnit firstTarget = targetList[0];

                // �ι�° Ÿ��
                BattleUnit secondTarget = null;
                if (targetList.Count > 1)
                {
                    secondTarget = targetList[1];
                }

                float skillDamage = e.actionUnit.AttackPoint * 0.8f;
                // �������� ���ݷ��� 80%

                // ������ ��� ��
                yield return new WaitForSeconds(0.2f);
                // ��ų ����Ʈ ����
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_GWEN_BaseAttack_Move");
                var rotation = effect.StartCoroutine(RotationEffect(effect));
                effect.transform.position = firstTarget.transform.position;
                Vector3 projectilePos;
                if (e.actionUnit.IsEnemy)
                // ���̸� �ܰ� ���� �ݴ��
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

                // 1�ʰ� �����ġ�� �̵�
                effect.transform.DOMove(firstTarget.transform.position, projectileMoveTime).OnComplete(() =>
                {
                    // �̵��Ϸ�� ������ ����
                    e.actionUnit.HitTarget(firstTarget, skillDamage);
                    if (targetList[0].IsDead)
                    // ù��° ����� �׾�����
                    {
                        BattleManager.ManaSystem.AddMana(1);
                        // ���� ȸ��
                        if (secondTarget != null)
                        // �ι�° ����� ������ ���
                        {
                            // �ܰ��� ����� �ñ� ��
                            effect.transform.DOMoveX(-1f, projectileMoveTime).SetEase(Ease.OutQuart);
                            effect.transform.DOMoveY(firstTarget.transform.position.y + 3, projectileMoveTime).SetEase(Ease.OutQuart).OnComplete(() =>
                             {
                                 // �ܰ��� �ι�° ��󿡰� ���ư���.
                                 effect.transform.DOMove(secondTarget.transform.position, projectileMoveTime).OnComplete(() =>
                                  {
                                      // �ܰ� ������ ����Ʈ ����ϰ� �������� ���� �� ��ų ����
                                      effect.StopCoroutine(rotation);
                                      effect.PlayEffect("Anim_Skill_Effect_GWEN_BaseAttack_Check");
                                      e.actionUnit.HitTarget(secondTarget, skillDamage);
                                      e.actionUnit.isSkillUsing = false;
                                  });
                             });
                        }
                        else
                        // ���� ���Ұ��
                        {
                            // �ܰ� ������ ����Ʈ ����ϰ� ��ų ����
                            effect.StopCoroutine(rotation);
                            effect.PlayEffect("Anim_Skill_Effect_GWEN_BaseAttack_Check");
                            e.actionUnit.isSkillUsing = false;
                        }
                    }
                    else
                    // ���� �ʾҴٸ�
                    {
                        // �ܰ� ������ ����Ʈ ����ϰ� ��ų ����
                        effect.StopCoroutine(rotation);
                        effect.PlayEffect("Anim_Skill_Effect_GWEN_BaseAttack_Check");
                        e.actionUnit.isSkillUsing = false;
                    }
                });


            }

            yield return new WaitForSeconds(1f);
        }

        // �ܰ��� ���ۺ��� �����ִ� �޼���
        IEnumerator RotationEffect(SkillEffect effect)
        {
            while (true)
            {
                // �ʴ� 10�� ȸ���ϵ���
                effect.transform.Rotate(new Vector3(0, 0, 3600 * Time.deltaTime));
                yield return null;
            }
        }
    }

}