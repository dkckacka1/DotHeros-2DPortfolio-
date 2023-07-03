using DG.Tweening;
using Portfolio.Battle;
using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_LOSA_ActiveSkill_2 : ActiveSkill
    {
        public Skill_LOSA_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<BattleUnit> targetUnits)
        {
            return targetUnits.GetEnemyTarget(actionUnit).GetTargetNum(this);
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
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
                    effect.transform.rotation = Quaternion.Euler(0, 0, -60);
                    arrowPos = e.actionUnit.projectilePos.position + new Vector3(0, 2);
                }
                else
                {
                    effect.transform.localScale = Vector3.one;
                    effect.transform.rotation = Quaternion.Euler(0, 0, 60);
                    arrowPos = e.actionUnit.projectilePos.position + new Vector3(0, 2);
                }
                effect.transform.position = arrowPos;
                effect.transform.DOMove(new Vector3(-2, 10), effect.arrowProjectileTime).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    if (e.actionUnit.IsEnemy)
                    {
                        effect.transform.rotation = Quaternion.Euler(0, 0, 60);
                    }
                    else
                    {
                        effect.transform.rotation = Quaternion.Euler(0, 0, -60);
                    }
                    effect.transform.DOMove(targetUnit.transform.position, effect.arrowProjectileTime).OnComplete(() =>
                    {

                        effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectileHit");

                        if (targetUnit.HasCondition(conditionList[0]))
                        {
                            e.actionUnit.HitTarget(targetUnit, skillDamage, true);
                        }
                        else
                        {
                            e.actionUnit.HitTarget(targetUnit, skillDamage);
                            targetUnit.AddCondition(conditionList[0].conditionID, conditionList[0], 2);
                        }

                        e.actionUnit.isSkillUsing = false;
                    });

                });
            }
        }
    }
}