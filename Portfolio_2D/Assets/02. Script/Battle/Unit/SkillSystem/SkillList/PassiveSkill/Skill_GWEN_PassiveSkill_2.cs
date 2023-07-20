using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ������ �нú� ��ų 2 Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_GWEN_PassiveSkill_2 : PassiveSkill
    {
        public Skill_GWEN_PassiveSkill_2(PassiveSkillData skillData) : base(skillData)
        {
        }

        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
            // ���� ���� ��� ���� Ȯ���� �ڽſ��� ���� �����̻� �ο�
            e.actionUnit.OnTakeDamagedEvent += (object sender, TakeDamageEventArgs s) =>
            {
                float effectPercent = (e.skillLevel * GetData.skillLevelValue_1 * 0.01f);
                if (GameLib.ProbabilityCalculation(effectPercent, 1f))
                {
                    e.actionUnit.AddCondition(GetData.conditinID_1, conditionList[0], 1);
                }
            };
        }
    }

}