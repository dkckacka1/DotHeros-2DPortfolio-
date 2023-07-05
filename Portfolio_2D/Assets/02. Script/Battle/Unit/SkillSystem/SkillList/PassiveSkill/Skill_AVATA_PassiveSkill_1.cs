using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� �ƹ�Ÿ�� �нú� ��ų 1 Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_AVATA_PassiveSkill_1 : PassiveSkill
    {
        public Skill_AVATA_PassiveSkill_1(SkillData skillData) : base(skillData)
        {
        }

        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
            // ���� ���۽� ����� ȿ�� ������ ���� ��Ų��.
            e.actionUnit.OnStartBattleEvent += (object sender, System.EventArgs s) =>
            {
                e.actionUnit.EffectHit += (e.skillLevel * GetData.skillLevelValue_1 * 0.01f);
            };
        }
    }

}