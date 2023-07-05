using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ���� ����
 */

namespace Portfolio
{
    [System.Serializable]
    public class UserUnitData
    {
        public int unitID;                  // ���� ID
        public int unitLevel = 1;           // ���� ����
        public int unitGrade;               // ���� ���
        public float unitExperience = 0f;   // ���� ����ġ

        public int activeSkillLevel_1 = 1;  // ��Ƽ�� ��ų 1 ����
        public int activeSkillLevel_2 = 1;  // ��Ƽ�� ��ų 2 ����
        public int passiveSkillLevel_1 = 1; // �нú� ��ų 1 ����
        public int passiveSkillLevel_2 = 1; // �нú� ��ų 2 ����

        public WeaponData weaponData;       // ���� ���� ����
        public HelmetData helmetData;       // ���� ��� ���� 
        public ArmorData armorData;         // ���� ���� ���� 
        public AmuletData amuletData;       // ���� ����� ���� 
        public RingData ringData;           // ���� ���� ���� 
        public ShoeData shoeData;           // ���� �Ź� ���� 

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