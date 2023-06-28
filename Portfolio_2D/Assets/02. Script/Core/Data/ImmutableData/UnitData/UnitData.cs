using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class UnitData : Data
    {
        // DefualtProperty
        public string unitName;
        public ElementalType elementalType;
        public bool isUserUnit = true;
        public int defaultGrade = 1;

        // UnitAttribute
        public float maxHP = 100;
        public float attackPoint = 10f;
        public float defencePoint = 0f;
        public float speed = 100f;
        public float criticalPercent = 0f;
        public float criticalDamage = 0f;
        public float effectHit = 0f;
        public float effectResistance = 0f;
        public float levelValue = 0.2f;
        public float gradeValue = 0.5f;

        // Skill
        public int basicAttackSKillID = 10000;
        public int activeSkillID_1 = -1;
        public int activeSkillID_2 = -1;
        public int passiveSkillID_1 = -1;
        public int passiveSkillID_2 = -1;

        // Apparence
        public string portraitImageName;
        public string animationName;
    }

}