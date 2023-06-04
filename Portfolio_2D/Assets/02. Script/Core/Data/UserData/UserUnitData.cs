using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class UserUnitData
    {
        public int unitID;
        public int unitLevel = 1;
        public int unitGrade;
        public float unitExperience = 0f;

        public int activeSkillLevel_1 = 1;
        public int activeSkillLevel_2 = 1;
        public int passiveSkillLevel_1 = 1;
        public int passiveSkillLevel_2 = 1;

        public WeaponData weaponData;
        public HelmetData helmetData;
        public ArmorData armorData;
        public AmuletData amuletData;
        public RingData RingData;
        public ShoeData shoeData;

        public UserUnitData() { }

        public UserUnitData(Unit unit)
        {
            unitID = unit.Data.ID;
            unitLevel = 1;
            unitGrade = unit.Data.defaultGrade;
        }
    }
}