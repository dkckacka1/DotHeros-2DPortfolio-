using DG.Tweening;
using Portfolio.Battle;
using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� �λ��� ��Ƽ�� ��ų 2Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_LOSA_ActiveSkill_2 : ActiveSkill
    {
        float arrowProjectileTime = 0.5f; // ȭ���� ���� ���� �ð�

        public Skill_LOSA_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // �ִ� ���� 5�� Ÿ��
            return targetUnits.GetEnemyTarget(actionUnit, this).GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // ��ų ������
            float skillDamage = e.actionUnit.AttackPoint * (1 + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));

            // ȭ���� ��� ��� ���ö����� ���
            yield return new WaitForSeconds(0.45f);
            foreach (var targetUnit in e.targetUnits)
            {
                // ����Ʈ ����
                SkillEffect effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectile");
                // ȭ���� ������ ��ġ ����
                Vector3 arrowPos;
                if (e.actionUnit.IsEnemy)
                    // ����ڰ� ���̸�
                {
                    // ���� �������� ������ ȭ���� ������ �ش�.
                    effect.transform.localScale = new Vector3(-1, 1, 1);
                    // ȭ���� ���� ������
                    effect.transform.rotation = Quaternion.Euler(0, 0, -60);
                    arrowPos = e.actionUnit.projectilePos.position + new Vector3(0, 2);
                }
                else
                    // ����ڰ� �Ʊ��̸�
                {
                    // �״��
                    effect.transform.localScale = Vector3.one;
                    // ȭ���� ���� ������
                    effect.transform.rotation = Quaternion.Euler(0, 0, 60);
                    arrowPos = e.actionUnit.projectilePos.position + new Vector3(0, 2);
                }

                // ȭ���� ��ġ�����ش�.
                effect.transform.position = arrowPos;
                // ȭ���� �ϴ÷� �̵��ϵ���
                effect.transform.DOMove(new Vector3(-2, 10), arrowProjectileTime).SetEase(Ease.InOutSine).OnComplete(() =>
                    // �̵��� ��������
                {
                    if (e.actionUnit.IsEnemy)
                        // ����� ���� ���ư��� ����� �Ʊ��̳� ���� ���� ȸ���� ����
                    {
                        effect.transform.rotation = Quaternion.Euler(0, 0, 60);
                    }
                    else
                    {
                        effect.transform.rotation = Quaternion.Euler(0, 0, -60);
                    }
                    effect.transform.DOMove(targetUnit.transform.position, arrowProjectileTime).OnComplete(() =>
                    {
                        // ��󿡰� �����ٸ�
                        // ���� ����Ʈ ǥ��
                        effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectileHit");

                        if (targetUnit.HasCondition(conditionList[0]))
                        {
                            // ���� �����̻��� �ִٸ� Ȯ�� ġ��Ÿ
                            e.actionUnit.HitTarget(targetUnit, skillDamage, true);
                        }
                        else
                        {
                            // ���ٸ� �Ϲ� ���� �ϰ� ���� �����̻� �ο�
                            e.actionUnit.HitTarget(targetUnit, skillDamage);
                            targetUnit.AddCondition(conditionList[0].conditionID, conditionList[0], 2);
                        }

                        // ��ų ����
                        e.actionUnit.isSkillUsing = false;
                    });

                });
            }
        }
    }
}