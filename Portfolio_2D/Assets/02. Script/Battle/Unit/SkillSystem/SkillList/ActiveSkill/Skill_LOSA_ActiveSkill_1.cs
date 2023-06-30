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

            float skillDamage = e.actionUnit.AttackPoint * (1 + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
            //Debug.Log(skillDamage);
            //Debug.Log(conditionList.Count);

            foreach (var targetUnit in e.targetUnits)
            {
                if(targetUnit.HasCondition(conditionList[0].conditionID))
                    // 낙인 상태이상을 가지고 있다면
                {
                    e.actionUnit.HitTarget(targetUnit, skillDamage, true);
                }
                else
                    // 낙인 상태이상을 가지고 있지 않다면
                {
                    e.actionUnit.HitTarget(targetUnit, skillDamage);
                }
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectile");
                effect.transform.position = e.actionUnit.projectilePos.position;
                effect.transform.localScale = e.actionUnit.IsEnemy ? new Vector3(-1, 1, 1) : Vector3.one;
                effect.transform.DOMove(targetUnit.transform.position, effect.arrowProjectileTime).SetEase(Ease.InOutSine).OnComplete(() => { effect.PlayEffect("Anim_Skill_Effect_LOSA_ArrowProjectileHit"); });
                Debug.Log(targetUnit.transform.position);
                //effect.transform.DOMove(targetUnit.transform.position, effect.arrowProjectileTime).OnComplete(() => { effect.ReleaseEffect(); });
            }

            e.actionUnit.isSkillUsing = false;
        }
    }
}

