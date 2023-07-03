using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_AVATA_PassiveSkill_2 : PassiveSkill
    {
        public Skill_AVATA_PassiveSkill_2(SkillData skillData) : base(skillData)
        {
        }
        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
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