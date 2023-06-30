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
            if (!TryGetSkillActionArgs(e, out SkillActionEventArgs args))
            {
                return;
            }

            Condition brand;

            if (!GameManager.Instance.TryGetCondition(GetData.conditinID_1, out brand))
            {
                return;
            }

            foreach (var targetUnit in args.targetUnits)
            {
                targetUnit.TakeDamage(args.actionUnit.AttackPoint * 0.8f);
                targetUnit.AddCondition(brand.conditionID, brand, 2);
                // TODO
                //var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                //effect.PlayEffect("Anim_Skill_Effect_ZICH_BaseAttack");
                //effect.transform.position = targetUnit.transform.position;

            }
            e.actionUnit.isSkillUsing = false;
        }
    }
}

