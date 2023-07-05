using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유닛 아바타의 패시브 스킬 2 클래스
 */

namespace Portfolio.skill
{
    public class Skill_AVATA_PassiveSkill_2 : PassiveSkill
    {
        public Skill_AVATA_PassiveSkill_2(SkillData skillData) : base(skillData)
        {
        }
        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
            // 대상의 턴 시작 시에 효과 판단하여 성공시 마나 1회복
            e.actionUnit.OnStartCurrentTurnEvent += (object sender, System.EventArgs s) =>
            {
                if (GameLib.ProbabilityCalculation((e.skillLevel * GetData.skillLevelValue_1)))
                {
                    BattleManager.ManaSystem.AddMana(1);
                }
            };
        }
    }

}