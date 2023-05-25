using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class PassiveSkill : Skill
    {
        private Module passiveModule_1;

        public PassiveSkillData GetData { get => (this.skillData as PassiveSkillData); }

        public PassiveSkill(SkillData skillData) : base(skillData)
        {
        }

        public override void Action(int skillLevel)
        {
            base.Action(skillLevel);
            Debug.Log("패시브 스킬 액션!");
        }

        public override void ShowDesc(int skillLevel)
        {
            base.ShowDesc(skillLevel);
            Debug.Log("나는 패시브 스킬입니다.");
        }

        protected override void SetModule()
        {
            throw new System.NotImplementedException();
        }
    }

}