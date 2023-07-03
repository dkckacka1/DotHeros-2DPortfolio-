using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_AVATA_ActiveSkill_2 : ActiveSkill
    {
        public Skill_AVATA_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override int GetActiveSkillCooltime(int skillLevel)
        {
            return base.GetActiveSkillCooltime(skillLevel) - skillLevel;
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            return targetUnits.GetEnemyTarget(actionUnit).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            SkillEffect effect = BattleManager.ObjectPool.SpawnSkillEffect();
            effect.PlayEffect("Anim_Skill_Effect_AVATA_ActiveSkill2");
            effect.transform.position = e.actionUnit.transform.position;


            foreach (var targetUnit in e.targetUnits)
            {
                if (targetUnit.IsEffectHit(e.actionUnit.EffectHit))
                {
                    BattleManager.ManaSystem.AddMana(1);
                }
            }

            yield return new WaitForSeconds(0.5f);

            e.actionUnit.isSkillUsing = false;
        }
    }

}