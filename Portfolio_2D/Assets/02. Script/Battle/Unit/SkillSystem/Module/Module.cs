using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public abstract class Module
    {
        public string moduleName;
        public string moduleDesc;

        public abstract void Action(SkillActionEventArgs skillActionEventArgs);

        public abstract string ShowDesc(int skillLevel);
    }
}