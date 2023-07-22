using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ������ �нú� ��ų 1 Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_GWEN_PassiveSkill_1 : PassiveSkill
    {
        public Skill_GWEN_PassiveSkill_1(PassiveSkillData skillData) : base(skillData)
        {
        }

        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
            // ������ ���ظ� ������ �������� ���ְ� ȿ�� �Ǵ��ؼ� ���� �� ����ø�� 1�ο��Ѵ�.
            e.actionUnit.OnHitTargetEvent += (object sender, HitEventArgs s) =>
            {
                float effectHit = s.actionUnit.EffectHit + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f);
                float effectRes = s.targetUnit.EffectResistance;
                if (GameLib.ProbabilityCalculation(effectHit - effectRes, 1f))
                {
                    s.targetUnit.AddCondition(GetData.conditinID_1, conditionList[0], 1);
                }
            };
        }
    }
}