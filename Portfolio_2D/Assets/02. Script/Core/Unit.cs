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
        private UserUnitData userUnitData;

        public ActiveSkill basicAttackSkill;
        public ActiveSkill activeSkill_1;
        public ActiveSkill activeSkill_2;
        public PassiveSkill passiveSkill_1;
        public PassiveSkill passiveSkill_2;

        //===========================================================
        // Equipment
        //===========================================================
        public WeaponData weaponData;
        public HelmetData helmetData;
        public ArmorData armorData;
        public AmuletData amuletData;
        public RingData ringData;
        public ShoeData shoeData;

        //===========================================================
        // Property
        //===========================================================
        public UnitData Data { get => data; }
        public int UnitLevel
        {
            get
            {
                if (userUnitData != null)
                {
                    return userUnitData.unitLevel;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                {
                    userUnitData.unitLevel = value;
                }
                else
                {
                    Debug.LogWarning("userUnitData = null");
                    return;
                }
            }
        }
        public int UnitGrade
        {
            get
            {
                if (userUnitData != null)
                {
                    return userUnitData.unitGrade;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                {
                    userUnitData.unitGrade = value;
                }
                else
                {
                    Debug.LogWarning("userUnitData = null");
                    return;
                }
            }
        }
        public float UnitExperience
        {
            get
            {
                if (userUnitData != null)
                {
                    return userUnitData.unitExperience;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (userUnitData != null)
                {
                    userUnitData.unitExperience = value;
                }
                else
                {
                    Debug.LogWarning("userUnitData = null");
                    return;
                }
            }
        }
        public float AttackPoint
        {
            get
            {
                float returnValue = GetProperty(data.attackPoint);
                if (weaponData != null)
                {
                    returnValue += weaponData.attackPoint;
                }

                returnValue += GetItemOptionValue(EquipmentOptionStat.AttackPoint);
                returnValue *= (1 + GetItemOptionValue(EquipmentOptionStat.AttackPercent));

                return Mathf.Round(returnValue);
            }
        }
        public float HealthPoint
        {
            get
            {
                float returnValue = GetProperty(data.maxHP);
                if (helmetData != null)
                {
                    returnValue += helmetData.healthPoint;
                }

                returnValue += GetItemOptionValue(EquipmentOptionStat.HealthPoint);
                returnValue *= 1 + GetItemOptionValue(EquipmentOptionStat.HealthPercent);
                return Mathf.Round(returnValue); ;
            }
        }
        public float DefencePoint
        {
            get
            {
                float returnValue = GetProperty(data.defencePoint);
                if (armorData != null)
                {
                    returnValue += armorData.defencePoint;
                }

                returnValue += GetItemOptionValue(EquipmentOptionStat.DefencePoint);
                returnValue *= 1 + GetItemOptionValue(EquipmentOptionStat.DefencePercent);
                return Mathf.Round(returnValue);
            }
        }
        public float Speed
        {
            get
            {
                float returnValue = data.speed;
                if (shoeData != null)
                {
                    returnValue += shoeData.speed;
                }

                returnValue += GetItemOptionValue(EquipmentOptionStat.Speed);
                return Mathf.Round(returnValue);
            }
        }
        public float CriticalPercent
        {
            get
            {
                float returnValue = data.criticalPercent;
                if (amuletData != null)
                {
                    returnValue += amuletData.criticalPercent;
                }

                returnValue += GetItemOptionValue(EquipmentOptionStat.CriticalPercent);
                return Mathf.Clamp(returnValue, 0, 0.8f);
            }
        }
        public float CriticalDamage
        {
            get
            {
                float returnValue = data.criticalDamage;

                if (amuletData != null)
                {
                    returnValue += amuletData.criticalPercent;
                }

                returnValue += GetItemOptionValue(EquipmentOptionStat.CriticalDamagePercent);
                return Mathf.Clamp(returnValue, 0, 0.8f);
            }
        }
        public float EffectHit
        {
            get
            {
                float returnValue = data.effectHit;
                if (ringData != null)
                {
                    returnValue += ringData.effectHit;
                }

                returnValue += GetItemOptionValue(EquipmentOptionStat.EffectHitPercent);
                return Mathf.Clamp(returnValue, 0, 0.8f);
            }
        }
        public float EffectResistance
        {
            get
            {
                float returnValue = data.effectResistance;
                if (ringData != null)
                {
                    returnValue += ringData.effectResistance;
                }

                returnValue += GetItemOptionValue(EquipmentOptionStat.EffectResistancePercent);
                return Mathf.Clamp(returnValue, 0, 1f);
            }
        }
        public int ActiveSkillLevel_1
        {
            get
            {
                if (userUnitData != null)
                {
                    return userUnitData.activeSkillLevel_1;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                {
                    userUnitData.activeSkillLevel_1 = value;
                }
                else
                {
                    Debug.LogWarning("userUnitData = null");
                    return;
                }
            }
        }
        public int ActiveSkillLevel_2
        {
            get
            {
                if (userUnitData != null)
                {
                    return userUnitData.activeSkillLevel_2;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                {
                    userUnitData.activeSkillLevel_2 = value;
                }
                else
                {
                    Debug.LogWarning("userUnitData = null");
                    return;
                }
            }
        }
        public int PassiveSkillLevel_1
        {
            get
            {
                if (userUnitData != null)
                {
                    return userUnitData.passiveSkillLevel_1;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                {
                    userUnitData.passiveSkillLevel_1 = value;
                }
                else
                {
                    Debug.LogWarning("userUnitData = null");
                    return;
                }
            }
        }
        public int PassiveSkillLevel_2
        {
            get
            {
                if (userUnitData != null)
                {
                    return userUnitData.passiveSkillLevel_2;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                {
                    userUnitData.passiveSkillLevel_2 = value;
                }
                else
                {
                    Debug.LogWarning("userUnitData = null");
                    return;
                }
            }
        }

        //===========================================================
        // SetUnit
        //===========================================================


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
            this.userUnitData = userUnitData;

            weaponData = userUnitData.weaponData;
            helmetData = userUnitData.helmetData;
            armorData = userUnitData.armorData;
            amuletData = userUnitData.amuletData;
            ringData = userUnitData.RingData;
            shoeData = userUnitData.shoeData;

            GameManager.Instance.TryGetSkill(unitData.basicAttackSKillID, out basicAttackSkill);
            GameManager.Instance.TryGetSkill(unitData.activeSkillID_1, out activeSkill_1);
            GameManager.Instance.TryGetSkill(unitData.activeSkillID_2, out activeSkill_2);
            GameManager.Instance.TryGetSkill(unitData.passiveSkillID_1, out passiveSkill_1);
            GameManager.Instance.TryGetSkill(unitData.passiveSkillID_2, out passiveSkill_2);
        }

        public float GetProperty(float DefaultValue)
        {
            if (userUnitData != null)
            {
                return DefaultValue * (1 + ((userUnitData.unitLevel - 1) * data.levelValue)) * (1 + ((userUnitData.unitGrade - 1) * data.gradeValue));
            }
            else
            {
                return DefaultValue;
            }
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
            List<EquipmentItemData> list = new List<EquipmentItemData>() { weaponData, helmetData, armorData, amuletData, ringData, shoeData };
            return list.Where(item => item != null);
        }
    }
}