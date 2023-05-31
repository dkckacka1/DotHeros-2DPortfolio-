using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Portfolio.skill;
using System.Linq;

namespace Portfolio
{
    public class Unit 
    {
        private UnitData data;

        public int unitLevel = 1;
        public int unitGrade = 1;

        public int activeSkillLevel_1 = 1;
        public int activeSkillLevel_2 = 1;
        public int passiveSkillLevel_1 = 1;
        public int passiveSkillLevel_2 = 1;

        public ActiveSkill basicAttackSkill;
        public ActiveSkill activeSkill_1;
        public ActiveSkill activeSkill_2;
        public PassiveSkill passiveSkill_1;
        public PassiveSkill passiveSkill_2;

        //===========================================================
        // Property
        //===========================================================
        public float AttackPoint
        {
            get
            {
                float returnValue = GetProperty(data.attackPoint);
                Debug.Log(returnValue);
                if (weaponData != null)
                {
                    returnValue += weaponData.attackPoint;
                }

                returnValue += GetItemOptionValue(EquipmentOptionStat.AttackPoint);
                returnValue *= (1 + GetItemOptionValue(EquipmentOptionStat.AttackPercent));

                return returnValue;
            }
        }

        // 프로퍼티노가다 해야함

        //===========================================================
        // Equipment
        //===========================================================
        public WeaponData weaponData;
        public HelmetData helmetData;
        public ArmorData armorData;
        public AmuletData amuletData;
        public RingData ringData;
        public ShoeData ShoeData;

        public UnitData Data { get => data; }

        public Unit(UnitData unitData)
        {
            this.data = unitData;
            GameManager.Instance.TryGetSkill(unitData.basicAttackSKillID, out basicAttackSkill);
            GameManager.Instance.TryGetSkill(unitData.activeSkillID_1, out activeSkill_1);
            GameManager.Instance.TryGetSkill(unitData.activeSkillID_2, out activeSkill_2);
            GameManager.Instance.TryGetSkill(unitData.passiveSkillID_1, out passiveSkill_1);
            GameManager.Instance.TryGetSkill(unitData.passiveSkillID_2, out passiveSkill_2);
        }

        public Unit(UnitData unitData, UserUnitData userUnitData)
        {
            this.data = unitData;

            unitLevel = userUnitData.unitLevel;
            unitGrade = userUnitData.unitGrade;

            weaponData = userUnitData.weaponData;
            helmetData = userUnitData.helmetData;
            armorData = userUnitData.armorData;
            amuletData = userUnitData.amuletData;
            ringData = userUnitData.RingData;
            ShoeData = userUnitData.shoeData;

            activeSkillLevel_1 = userUnitData.activeSkillLevel_1;
            activeSkillLevel_2 = userUnitData.activeSkillLevel_2;
            passiveSkillLevel_1 = userUnitData.passiveSkillLevel_1;
            passiveSkillLevel_2 = userUnitData.passiveSkillLevel_2;

            GameManager.Instance.TryGetSkill(unitData.basicAttackSKillID, out basicAttackSkill);
            GameManager.Instance.TryGetSkill(unitData.activeSkillID_1, out activeSkill_1);
            GameManager.Instance.TryGetSkill(unitData.activeSkillID_2, out activeSkill_2);
            GameManager.Instance.TryGetSkill(unitData.passiveSkillID_1, out passiveSkill_1);
            GameManager.Instance.TryGetSkill(unitData.passiveSkillID_2, out passiveSkill_2);
        }

        public float GetProperty(float DefaultValue)
        {
            return DefaultValue * (1 + ((unitLevel - 1) * data.levelValue)) * (1 + ((unitGrade - 1) * data.gradeValue));
        }

        public float GetItemOptionValue(EquipmentOptionStat optionStatType)
        {
            float returnValue = 0;

            foreach (var item in GetEuqipmentList())
            {
                if (item.optionStat_1_Type == optionStatType)
                {
                    returnValue += item.optionStat_1_value;
                }

                if (item.optionStat_2_Type == optionStatType)
                {
                    returnValue += item.optionStat_2_value;
                }

                if (item.optionStat_3_Type == optionStatType)
                {
                    returnValue += item.optionStat_3_value;
                }

                if (item.optionStat_4_Type == optionStatType)
                {
                    returnValue += item.optionStat_4_value;
                }
            }

            return returnValue;
        }

        public IEnumerable<EquipmentItemData> GetEuqipmentList()
        {
            List<EquipmentItemData> list = new List<EquipmentItemData>() { weaponData , helmetData, armorData, amuletData, ringData, ShoeData};
            return list.Where(item => item != null);
        }
    }
}