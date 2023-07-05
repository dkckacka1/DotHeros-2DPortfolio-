using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �λ��� �нú� ��ų 1 Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_LOSA_PassiveSkill_1 : PassiveSkill
    {
        public Skill_LOSA_PassiveSkill_1(SkillData skillData) : base(skillData)
        {
        }

        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
            // ���� ���� �� ġ��Ÿ ������ ����
            e.actionUnit.OnStartBattleEvent += (object sender, System.EventArgs s) =>
            {
                e.actionUnit.CriticalDamage *= 1 + (0.25f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
            };
        }
    }

}
