using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class UnitData : Data
    {
        public string unitName;
        public ElementalType elementalType;

        // UnitAttribute
        public float maxHP = 100;
        public float attackPoint = 10f;
        public float defencePoint = 0f;
        public float speed = 100f;
        public float criticalPoint = 0f;
        public float criticalDamage = 0f;
        public float effectHit = 0f;
        public float effectResistance = 0f;

        // Apparence
    }

}