using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ��ũ�� �нú� ��ų 1 Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_ZICH_PassiveSkill_1 : PassiveSkill
    {
        public Skill_ZICH_PassiveSkill_1(PassiveSkillData skillData) : base(skillData)
        {
        }

        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
            // ���� ���� �� �ڽ��� ���ݷ� ����
            e.actionUnit.OnStartBattleEvent += (object sender, System.EventArgs s) => { e.actionUnit.AttackPoint *= (1 + (GetData.skillLevelValue_1 * e.skillLevel * 0.01f)); };
        }
    }
}