using Portfolio.Battle;
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

        protected override string GetLogString(SkillActionEventArgs e)
        {
            string playerUnit = string.Empty;

            if (!e.actionUnit.IsEnemy)
            {
                playerUnit = $"<color=green>[{e.actionUnit.name}]</color>";
            }
            else
            {
                playerUnit = $"<color=red>[{e.actionUnit.name}]</color>";
            }

            string log = $"{playerUnit}의 패시브 스킬[{GetData.skillName}] 발동!";

            return log;
        }

        public abstract void SetPassiveSkill(SkillActionEventArgs e);
    }

}