using Portfolio.Battle;
using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_LOSA_BaseAttack : ActiveSkill
    {
        public Skill_LOSA_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);

            float skillDamage = e.actionUnit.AttackPoint * 0.8f;

            foreach (var targetUnit in e.targetUnits)
            {
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                targetUnit.AddCondition(conditionList[0].conditionID, conditionList[0], 2);
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_LOSA_BaseAttack");
                effect.transform.position = targetUnit.transform.position;

            }
            e.actionUnit.isSkillUsing = false;
        }
    }
}

