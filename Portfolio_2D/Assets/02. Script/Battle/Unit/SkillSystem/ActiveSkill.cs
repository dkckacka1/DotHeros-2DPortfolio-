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

        public override void Action(object sender, int skillLevel)
        {
            base.Action(sender, skillLevel);
            //Debug.Log("액티브 스킬 액션!");
        }

        public override void ShowDesc(int skillLevel)
        {
            base.ShowDesc(skillLevel);
            //Debug.Log("나는 액티브 스킬입니다.");
        }
    }
}