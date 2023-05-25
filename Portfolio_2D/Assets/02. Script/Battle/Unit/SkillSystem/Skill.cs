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

        public virtual void Action(int skillLevel)
        {
            Debug.Log("skillLevel = " + skillLevel);
        }

        public virtual void ShowDesc(int skillLevel)
        {
            Debug.Log($"{this.skillData.skillDesc} + {skillLevel}");
        }

        protected abstract void SetModule();
    } 
}
