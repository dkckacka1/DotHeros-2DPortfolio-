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

        public override void Action(int skillLevel)
        {
            base.Action(skillLevel);
            Debug.Log("��Ƽ�� ��ų �׼�!");
        }

        public override void ShowDesc(int skillLevel)
        {
            base.ShowDesc(skillLevel);
            Debug.Log("���� ��Ƽ�� ��ų�Դϴ�.");
        }

        protected override void SetModule()
        {

        }
    }
}