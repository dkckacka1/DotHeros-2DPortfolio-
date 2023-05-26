using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public abstract class Skill
    {
        protected SkillData skillData;

        protected Module skillModule_1;
        protected Module skillModule_2;
        protected Module skillModule_3;

        public Skill(SkillData skillData)
        {
            this.skillData = skillData;

            if (skillData.moduleName_1 != "NULL")
            {
                skillModule_1 = CreateModule(skillData.moduleName_1);
            }

            if (skillData.moduleName_2 != "NULL")
            {
                skillModule_2 = CreateModule(skillData.moduleName_2);
            }

            if (skillData.moduleName_3 != "NULL")
            {
                skillModule_3 = CreateModule(skillData.moduleName_3);
            }
        }

        public virtual void Action(object sender, int skillLevel)
        {
            //Debug.Log("skillLevel = " + skillLevel);
        }

        public virtual void ShowDesc(int skillLevel)
        {
            //Debug.Log($"{this.skillData.skillDesc} + {skillLevel}");
            skillModule_1?.ShowDesc(skillLevel);
            skillModule_2?.ShowDesc(skillLevel);
            skillModule_3?.ShowDesc(skillLevel);
        }

        private Module CreateModule(string moduleName)
        {
            //Debug.Log(moduleName);
            var obj = Activator.CreateInstance(Type.GetType("Portfolio.skill." + moduleName));
            return obj as Module;
        }
    } 
}
