using Portfolio.Battle;
using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_ZICH_ActiveSkill_1 : ActiveSkill
    {
        public Skill_ZICH_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);
            if (!TryGetSkillActionArgs(e, out SkillActionEventArgs args))
            {
                return;
            }

            if (GameManager.Instance.TryGetCondition(GetData.conditinID_1, out Condition condition))
            {
                Debug.Log(e.skillLevel);
                foreach (var targetUnit in args.targetUnits)
                {
                    targetUnit.AddCondition(GetData.conditinID_1, condition, 1 + e.skillLevel);
                    var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                    effect.PlayEffect("Anim_Skill_Effect_ZICH_ActiveSkill1");
                    effect.transform.position = targetUnit.footPos.position;
                }
            }

            e.actionUnit.isSkillUsing = false;
        }
    }
}