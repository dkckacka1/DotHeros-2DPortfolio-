using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace Portfolio.skill
{
    public class Skill_LOSA_ActiveSkill_1 : ActiveSkill
    {
        public Skill_LOSA_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);

            e.actionUnit.StartCoroutine(PlaySkillEffect(e));
        }

        private IEnumerator PlaySkillEffect(SkillActionEventArgs e)
        {
            float skillDamage = e.actionUnit.AttackPoint * (1 + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
            yield return new WaitForSeconds(0.45f);
            foreach (var targetUnit in e.targetUnits)
            {
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectile");
                Vector3 arrowPos;
                if (e.actionUnit.IsEnemy)
                {
                    effect.transform.localScale = new Vector3(-1, 1, 1);
                    arrowPos = e.actionUnit.projectilePos.position + new Vector3(-1.2f, 0);
                }
                else
                {
                    effect.transform.localScale = Vector3.one;
                    arrowPos = e.actionUnit.projectilePos.position + new Vector3(1.2f, 0);
                }
                effect.transform.position = arrowPos;
                effect.transform.DOMove(targetUnit.transform.position, effect.arrowProjectileTime).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectileHit");
                    if (targetUnit.HasCondition(conditionList[0].conditionID))
                    // 낙인 상태이상을 가지고 있다면
                    {
                        e.actionUnit.HitTarget(targetUnit, skillDamage, true);
                    }
                    else
                    // 낙인 상태이상을 가지고 있지 않다면
                    {
                        e.actionUnit.HitTarget(targetUnit, skillDamage);
                    }
                    e.actionUnit.isSkillUsing = false;
                });
            }
        }
    }
}

