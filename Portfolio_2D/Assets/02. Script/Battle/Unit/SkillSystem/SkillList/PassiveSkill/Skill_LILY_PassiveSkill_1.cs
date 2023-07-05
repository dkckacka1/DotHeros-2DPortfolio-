using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * 유닛 릴리 패시브 스킬 1 클래스
 */

namespace Portfolio.skill
{
    public class Skill_LILY_PassiveSkill_1 : PassiveSkill
    {
        public Skill_LILY_PassiveSkill_1(SkillData skillData) : base(skillData)
        {
        }

        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
            // 자신이 기본 공격 스킬 사용 시
            e.actionUnit.OnAttackEvent += (object sender, System.EventArgs s) =>
            {
                // 체력이 가장 낮은 아군 찾기
                var targetUnit = BattleManager.ActionSystem.GetLiveUnit.Where(unit => e.actionUnit.IsAlly(unit)).OrderBy(unit => unit.CurrentHP).First();
                if (targetUnit != null)
                {
                    // 아군의 체력 회복
                    float healValue = targetUnit.MaxHP * (0.1f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
                    e.actionUnit.HealTarget(targetUnit, healValue);
                }
            };
        }
    }

}