using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_ZICH_BaseAttack : ActiveSkill
    {
        public Skill_ZICH_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }
        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<BattleUnit> targetUnits) => targetUnits.GetEnemyTarget(actionUnit).GetLowHealth().GetTargetNum(GetData.targetNum);


        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);

            foreach (var targetUnit in e.targetUnits)
            {
                e.actionUnit.HitTarget(targetUnit, e.actionUnit.AttackPoint);
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_ZICH_BaseAttack");
                effect.transform.position = targetUnit.transform.position;

            }
            e.actionUnit.isSkillUsing = false;
        }

    }
}