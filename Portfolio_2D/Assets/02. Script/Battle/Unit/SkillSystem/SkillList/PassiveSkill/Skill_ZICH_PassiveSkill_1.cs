using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유닛 지크의 패시브 스킬 1 클래스
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
            // 전투 시작 시 자신의 공격력 증가
            e.actionUnit.OnStartBattleEvent += (object sender, System.EventArgs s) => { e.actionUnit.AttackPoint *= (1 + (GetData.skillLevelValue_1 * e.skillLevel * 0.01f)); };
        }
    }
}