using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public abstract class Skill
    {
        protected SkillData skillData;

        public Skill(SkillData skillData)
        {
            this.skillData = skillData;
        }

        public abstract void Action(object sender, EventArgs e);

        public virtual string GetDesc(int skillLevel)
        {
            //Debug.Log($"{this.skillData.skillDesc} + {skillLevel}");
            return "";
        }

        protected bool TryGetSkillActionArgs(EventArgs args, out SkillActionEventArgs skillargs)
        {

            if (args != null && args is SkillActionEventArgs)
            {
                skillargs = args as SkillActionEventArgs;
                return true;
            }

            Debug.LogWarning("Action Eventargs error");
            skillargs = null;
            return false;
        }
    } 
}
