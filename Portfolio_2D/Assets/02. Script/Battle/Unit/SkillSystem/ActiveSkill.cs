using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class ActiveSkill : Skill
    {
        private Module activeModule_1;
        private Module activeModule_2;
        private Module activeModule_3;

        public ActiveSkillData GetData { get => (this.skillData as ActiveSkillData); }

        public ActiveSkill(ActiveSkillData skillData) : base(skillData)
        {
            sk
        }

        public override void Action(int skillLevel)
        {
            base.Action(skillLevel);
            Debug.Log("액티브 스킬 액션!");
        }

        public override void ShowDesc(int skillLevel)
        {
            base.ShowDesc(skillLevel);
            Debug.Log("나는 액티브 스킬입니다.");
        }

        protected override void SetModule()
        {

        }
    }
}