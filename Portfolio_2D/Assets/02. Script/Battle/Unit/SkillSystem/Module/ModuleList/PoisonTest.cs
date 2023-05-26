using Portfolio.Condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.module
{
    public class PoisonTest : Module
    {
        public override void Action(SkillActionEventArgs args)
        {
            args.targetUnit.AddCondition("Potion", new Poison(), 4);
        }

        public override string ShowDesc(int skillLevel)
        {
            return "";
        }
    }

}