using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Portfolio.skill
{
    public class Skill_LILY_PassiveSkill_2 : PassiveSkill
    {
        public Skill_LILY_PassiveSkill_2(SkillData skillData) : base(skillData)
        {
        }
        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
            e.actionUnit.OnDeadEvent += (object sender, System.EventArgs s) =>
            {
                var targetUnits = BattleManager.ActionSystem.GetLiveUnit.Where(unit => e.actionUnit.IsAlly(unit));
                foreach (var targetUnit in targetUnits)
                {
                    if (targetUnit != null)
                    {
                        float healValue = targetUnit.MaxHP * (0.25f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
                        e.actionUnit.HealTarget(targetUnit, healValue);
                    }
                }
            };
        }
    }

}