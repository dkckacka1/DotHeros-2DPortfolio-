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

        public virtual void Action(object sender, EventArgs e)
        {
            //Debug.Log($"{GetType().Name} 액션 실행");

            Debug.Log(e is SkillActionEventArgs);
            if (!(e is SkillActionEventArgs))
            {
                return;
            }

            skillModule_1?.Action(e as SkillActionEventArgs);
            skillModule_2?.Action(e as SkillActionEventArgs);
            skillModule_3?.Action(e as SkillActionEventArgs);
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
            var obj = Activator.CreateInstance(Type.GetType("Portfolio.skill.module." + moduleName));
            return obj as Module;
        }
    } 
}
