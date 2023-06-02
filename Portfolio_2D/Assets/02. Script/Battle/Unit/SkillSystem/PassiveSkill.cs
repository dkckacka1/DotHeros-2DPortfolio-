using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public abstract class PassiveSkill : Skill
    {
        public new PassiveSkillData GetData { get => (this.skillData as PassiveSkillData); }

        public PassiveSkill(SkillData skillData) : base(skillData)
        {
        }

        public override string GetDesc(int skillLevel)
        {
            return base.GetDesc(skillLevel);
            //Debug.Log("나는 패시브 스킬입니다.");
        }
    }

}