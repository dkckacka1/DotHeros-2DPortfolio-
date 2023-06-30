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

        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);


            float skillDamage = e.actionUnit.AttackPoint * (0.5f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));

            foreach (var targetUnit in e.targetUnits)
            {
                if(targetUnit.HasCondition(conditionList[0]))
                {
                    e.actionUnit.HitTarget(targetUnit, skillDamage, true);
                }
                else
                {
                    e.actionUnit.HitTarget(targetUnit, skillDamage);
                    targetUnit.AddCondition(conditionList[0].conditionID, conditionList[0], 2);
                }

                //var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                //effect.PlayEffect("Anim_Skill_Effect_ZICH_BaseAttack");
                //effect.transform.position = targetUnit.transform.position;
            }
            e.actionUnit.isSkillUsing = false;
        }
    }
}