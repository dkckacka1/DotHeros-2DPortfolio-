using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * ���� ���� �нú� ��ų 1 Ŭ����
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
            // �ڽ��� �⺻ ���� ��ų ��� ��
            e.actionUnit.OnAttackEvent += (object sender, System.EventArgs s) =>
            {
                // ü���� ���� ���� �Ʊ� ã��
                var targetUnit = BattleManager.ActionSystem.GetLiveUnit.Where(unit => e.actionUnit.IsAlly(unit)).OrderBy(unit => unit.CurrentHP).First();
                if (targetUnit != null)
                {
                    // �Ʊ��� ü�� ȸ��
                    float healValue = targetUnit.MaxHP * (0.1f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
                    e.actionUnit.HealTarget(targetUnit, healValue);
                }
            };
        }
    }

}