using Portfolio.Battle;
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
            if (!TryGetSkillActionArgs(e, out SkillActionEventArgs args))
            {
                return;
            }

            // TODO
            //foreach (var targetUnit in args.targetUnits)
            //{
            //    targetUnit.TakeDamage(args.actionUnit.AttackPoint);
            //    var effect = BattleManager.ObjectPool.SpawnSkillEffect();
            //    effect.PlayEffect("Anim_Skill_Effect_ZICH_BaseAttack");
            //    effect.transform.position = targetUnit.transform.position;

            //}
            e.actionUnit.isSkillUsing = false;
        }
    }
}