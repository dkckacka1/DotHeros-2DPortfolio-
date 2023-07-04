using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_ZICH_PassiveSkill_1 : PassiveSkill
    {
        public Skill_ZICH_PassiveSkill_1(SkillData skillData) : base(skillData)
        {
        }

        public override void SetPassiveSkill(SkillActionEventArgs e)
        {
            e.actionUnit.OnStartBattleEvent += (object sender, System.EventArgs s) => { e.actionUnit.AttackPoint *= (1 + (GetData.skillLevelValue_1 * e.skillLevel * 0.01f)); };
        }
    }

}