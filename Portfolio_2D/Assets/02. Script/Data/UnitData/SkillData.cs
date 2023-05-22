using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class SkillData : Data
    {
        public string skillName;
        public string skillDesc;

        public bool isActiveSkill;

        public bool isAutoTarget;

        public bool isPlayerTarget;
        public bool isEnemyTarget;
        public bool isFrontTarget;
        public bool isRearTarget;

        public int targetNum;

        public string optionName1;
        public string optionName2;
        public string optionName3;
    }
}