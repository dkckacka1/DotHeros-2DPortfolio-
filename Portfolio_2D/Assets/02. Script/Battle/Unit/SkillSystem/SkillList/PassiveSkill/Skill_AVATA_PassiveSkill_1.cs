using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유닛 아바타의 패시브 스킬 1 클래스
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
            // 전투 시작시 대상의 효과 적중을 증가 시킨다.
            e.actionUnit.OnStartBattleEvent += (object sender, System.EventArgs s) =>
            {
                e.actionUnit.EffectHit += (e.skillLevel * GetData.skillLevelValue_1 * 0.01f);
            };
        }
    }

}