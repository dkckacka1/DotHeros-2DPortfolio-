using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유저 유닛 정보
 */

namespace Portfolio
{
    [System.Serializable]
    public class UserUnitData
    {
        public int unitID;                  // 유닛 ID
        public int unitLevel = 1;           // 유닛 레벨
        public int unitGrade;               // 유닛 등급
        public float unitExperience = 0f;   // 유닛 경험치

        public int activeSkillLevel_1 = 1;  // 액티브 스킬 1 레벨
        public int activeSkillLevel_2 = 1;  // 액티브 스킬 2 레벨
        public int passiveSkillLevel_1 = 1; // 패시브 스킬 1 레벨
        public int passiveSkillLevel_2 = 1; // 패시브 스킬 2 레벨

        public WeaponData weaponData;       // 장착 무기 정보
        public HelmetData helmetData;       // 장착 헬멧 정보 
        public ArmorData armorData;         // 장착 갑옷 정보 
        public AmuletData amuletData;       // 장착 목걸이 정보 
        public RingData ringData;           // 장착 반지 정보 
        public ShoeData shoeData;           // 장착 신발 정보 

        public UserUnitData() { }

        public UserUnitData(Unit unit)
        {
            unitID = unit.UnitID;
            unitLevel = 1;
            unitGrade = unit.UnitGrade;
        }

        public UserUnitData(UnitData unitData)
        {
            unitID = unitData.ID;
            unitLevel = 1;
            unitGrade = unitData.defaultGrade;
        }
    }
}