using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 로사의 패시브 스킬 1 클래스
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
            // 전투 시작 시 치명타 데미지 증가
            e.actionUnit.OnStartBattleEvent += (object sender, System.EventArgs s) =>
            {
                e.actionUnit.CriticalDamage *= 1 + (0.25f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
            };
        }
    }

}
