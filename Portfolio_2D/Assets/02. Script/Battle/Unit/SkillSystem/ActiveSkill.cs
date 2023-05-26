using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class ActiveSkill : Skill
    {
        public ActiveSkillData GetData { get => (this.skillData as ActiveSkillData); }

        public ActiveSkill(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override void Action(object sender, EventArgs e)
        {
            base.Action(sender, e);
        }

        public override void ShowDesc(int skillLevel)
        {
            base.ShowDesc(skillLevel);
            //Debug.Log("나는 액티브 스킬입니다.");
        }
    }
}