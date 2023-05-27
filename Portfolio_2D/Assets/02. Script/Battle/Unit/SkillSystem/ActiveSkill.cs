using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public abstract class ActiveSkill : Skill
    {
        public ActiveSkillData GetData { get => (this.skillData as ActiveSkillData); }

        public ActiveSkill(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override string GetDesc(int skillLevel)
        {
            return base.GetDesc(skillLevel);
            //Debug.Log("나는 액티브 스킬입니다.");
        }
    }
}