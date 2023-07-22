using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

/*
 * ���� �λ��� ��Ƽ�� ��ų 1 Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_LOSA_ActiveSkill_1 : ActiveSkill, ISingleTarget
    {
        float arrowProjectileTime = 0.5f; // ȭ���� ���ư��� �ð�

        public Skill_LOSA_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // ü���� ���� ���� �� �Ѹ� Ÿ��
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // ��ų ������
            float skillDamage = e.actionUnit.AttackPoint * (1 + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));

            // ȭ���� ��� ��� ���ö����� ���
            yield return new WaitForSeconds(0.45f);
            GameManager.AudioManager.PlaySoundOneShot("Sound_Skill_LOSA_ActiveSkill_Shoot");
            foreach (var targetUnit in e.targetUnits)
            {
                // ȭ�� ����Ʈ ���
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectile");
                // ȭ���� ������ ��ġ ����
                Vector3 arrowPos;
                if (e.actionUnit.IsEnemy)
                    // ����ڰ� ���̸�
                {
                    // ���� �������� ������ ȭ���� ������ �ش�.
                    effect.transform.localScale = new Vector3(-1, 1, 1);
                    arrowPos = e.actionUnit.projectilePos.position + new Vector3(-1.2f, 0);
                }
                else
                    // ����ڰ� �Ʊ��̸�
                {
                    // �״��
                    effect.transform.localScale = Vector3.one;
                    arrowPos = e.actionUnit.projectilePos.position + new Vector3(1.2f, 0);
                }
                // ȭ���� ��ġ���ְ�
                effect.transform.position = arrowPos;
                // ȭ���� ����� ��ġ���� �̵������ش�.
                effect.transform.DOMove(targetUnit.transform.position, arrowProjectileTime).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    // �̵��� ������ ȭ���� ������ ����Ʈ�� ������ش�.
                    effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectileHit");
                    if (targetUnit.HasCondition(conditionList[0].conditionID))
                    // ���� �����̻��� ������ �ִٸ�
                    {
                        // Ȯ�� ġ�� ����
                        e.actionUnit.HitTarget(targetUnit, skillDamage, true);
                    }
                    else
                    // ���� �����̻��� ������ ���� �ʴٸ�
                    {
                        // �Ϲ� ����
                        e.actionUnit.HitTarget(targetUnit, skillDamage);
                    }
                    GameManager.AudioManager.PlaySoundOneShot("Sound_Skill_LOSA_ActiveSkill_Hit");

                    // ȭ���� �̵��ߴٸ� ��ų ����
                    e.actionUnit.isSkillUsing = false;
                });
            }
        }
    }
}