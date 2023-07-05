using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * 유닛 릴리스의 패시브 스킬 2 클래스
 */

namespace Portfolio.skill
{
    public class Skill_LILY_PassiveSkill_2 : PassiveSkill
    {
        public Skill_LILY_PassiveSkill_2(SkillData skillData) : base(skillData)
        {
        }
        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
            // 자신이 사망 시 
            e.actionUnit.OnDeadEvent += (object sender, System.EventArgs s) =>
            {
                // 아군 전체를 대상으로
                var targetUnits = BattleManager.ActionSystem.GetLiveUnit.Where(unit => e.actionUnit.IsAlly(unit));
                foreach (var targetUnit in targetUnits)
                {
                    if (targetUnit != null)
                    {
                        // 체력을 회복 시킨다.
                        float healValue = targetUnit.MaxHP * (0.25f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
                        e.actionUnit.HealTarget(targetUnit, healValue);
                    }
                }
            };
        }
    }

}