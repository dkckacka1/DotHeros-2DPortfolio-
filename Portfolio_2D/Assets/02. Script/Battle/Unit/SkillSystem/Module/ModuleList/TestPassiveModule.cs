using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.module
{
    public class TestPassiveModule : Module
    {
        public override void Action(SkillActionEventArgs args)
        {
            args.targetUnit.TakeDamage(args.skillLevel * 10);
        }

        public override string ShowDesc(int skillLevel)
        {
            return "";
        }
    }

}