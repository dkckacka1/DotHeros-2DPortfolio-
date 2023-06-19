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
        private int? designatedLevel;
        private int? DesignatedGrade;

        public ActiveSkill basicAttackSkill;
        public ActiveSkill activeSkill_1;
        public ActiveSkill activeSkill_2;
        public PassiveSkill passiveSkill_1;
        public PassiveSkill passiveSkill_2;

        //===========================================================
        // Equipment
        //===========================================================
        public WeaponData WeaponData
        {
            get
            {
                return userUnitData != null ? userUnitData.weaponData : null;
            }
            set
            {
                userUnitData.weaponData = value;
            }
        }
        public HelmetData HelmetData
        {
            get
            {
                return userUnitData != null ? userUnitData.helmetData : null;
            }
            set
            {
                userUnitData.helmetData = value;
            }
        }
        public ArmorData ArmorData
        {
            get
            {
                return userUnitData != null ? userUnitData.armorData : null;
            }
            set
            {
                userUnitData.armorData = value;
            }
        }
        public AmuletData AmuletData
        {
            get
            {
                return userUnitData != null ? userUnitData.amuletData : null;
            }
            set
            {
                userUnitData.amuletData = value;
            }
        }
        public RingData RingData
        {
            get
            {
                return userUnitData != null ? userUnitData.ringData : null;
            }
            set
            {
                userUnitData.ringData = value;
            }
        }
        public ShoeData ShoeData
        {
            get
            {
                return userUnitData != null ? userUnitData.shoeData : null;
            }
            set
            {
                userUnitData.shoeData = value;
            }
        }

        //===========================================================
        // Apparence
        //===========================================================
        public Sprite portraitSprite;
        public RuntimeAnimatorController animController;

        //===========================================================
        // Property
        //===========================================================
        public UnitData Data { get => data; }
        public UserUnitData UserData { get => userUnitData; }
        public int UnitCurrentLevel
        {
            get
            {
                if (userUnitData != null)
                {
                    return userUnitData.unitLevel;
                }
                else
                {
                    if (designatedLevel != null)
                    {
                        return (int)designatedLevel;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            set
            {
                if (userUnitData != null)
                {
                    if (value >= UnitMaxLevel)
                    {
                        userUnitData.unitLevel = UnitMaxLevel;
                    }
                    else
                    {
                        userUnitData.unitLevel = value;
                    }
                }
                else
                {
                    designatedLevel = value;
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
                    if (DesignatedGrade != null)
                    {
                        return (int)DesignatedGrade;
                    }
                    else
                    {
                        return data.defaultGrade;
                    }
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
                    DesignatedGrade = value;
                    return;
                }
            }
        }
        public int UnitMaxLevel
        {
            get
            {
                if (userUnitData != null)
                {
                    return userUnitData.unitGrade * 10;
                }
                else
                {
                    return 1;
                }
            }
        }
        public bool IsMaxLevel
        {
            get
            {
                return UnitCurrentLevel == UnitMaxLevel;
            }
        }
        public float CurrentExperience
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
                    if (value >= MaxExperience)
                    {
                        if (IsMaxLevel)
                        {
                            userUnitData.unitExperience = MaxExperience;
                        }
                        else
                        {
                            userUnitData.unitExperience = value - MaxExperience;
                            LevelUP();
                        }
                    }
                    else
                    {
                        userUnitData.unitExperience = value;
                    }
                }
                else
                {
                    Debug.LogWarning("userUnitData = null");
                    return;
                }
            }
        }
        public float MaxExperience
        {
            get
            {
                //Debug.Log("최대 경험치 : " + Mathf.Log(10, UnitCurrentLevel));
                return UnitCurrentLevel * 1000;
            }
        }
        public float AttackPoint
        {
            get
            {
                float returnValue = GetProperty(data.attackPoint);
                if (WeaponData != null)
                {
                    returnValue += WeaponData.attackPoint;
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
                if (HelmetData != null)
                {
                    returnValue += HelmetData.healthPoint;
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
                if (ArmorData != null)
                {
                    returnValue += ArmorData.defencePoint;
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
                if (ShoeData != null)
                {
                    returnValue += ShoeData.speed;
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
                if (AmuletData != null)
                {
                    returnValue += AmuletData.criticalPercent;
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

                if (AmuletData != null)
                {
                    returnValue += AmuletData.criticalPercent;
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
                if (RingData != null)
                {
                    returnValue += RingData.effectHit;
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
                if (RingData != null)
                {
                    returnValue += RingData.effectResistance;
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
        public int battlePower
        {
            get
            {
                return (int)(AttackPoint + DefencePoint);
            }
        }
        //===========================================================
        // SetUnit
        //===========================================================
        public Unit(UnitData unitData)
        {
            this.data = unitData;
            SetUnitData(unitData);
        }

        public Unit(UnitData unitData, int grade, int level)
        {
            this.data = unitData;
            this.DesignatedGrade = grade;
            this.designatedLevel = level;
            SetUnitData(unitData);
        }

        public Unit(UnitData unitData, UserUnitData userUnitData)
        {
            this.data = unitData;
            this.userUnitData = userUnitData;

            WeaponData = userUnitData.weaponData;
            HelmetData = userUnitData.helmetData;
            ArmorData = userUnitData.armorData;
            AmuletData = userUnitData.amuletData;
            RingData = userUnitData.ringData;
            ShoeData = userUnitData.shoeData;

            SetUnitData(unitData);
        }

        private void SetUnitData(UnitData unitData)
        {
            portraitSprite = GameManager.Instance.GetSprite(this.data.portraitImageName);
            animController = GameManager.Instance.GetAnimController(this.data.animationName);
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
            List<EquipmentItemData> list = new List<EquipmentItemData>() { WeaponData, HelmetData, ArmorData, AmuletData, RingData, ShoeData };
            return list.Where(item => item != null);
        }

        private void LevelUP()
        {
            UnitCurrentLevel++;
        }

        //===========================================================
        // SkillMethod
        //===========================================================
        public void SkillLevelUp(UnitSkillType skillType, int levelCount = 1)
        {
            switch (skillType)
            {
                case UnitSkillType.ActiveSkill_1:
                    userUnitData.activeSkillLevel_1 += levelCount;
                    break;
                case UnitSkillType.ActiveSkill_2:
                    userUnitData.activeSkillLevel_2 += levelCount;
                    break;
                case UnitSkillType.PassiveSkill_1:
                    userUnitData.passiveSkillLevel_1 += levelCount;
                    break;
                case UnitSkillType.PassiveSkill_2:
                    userUnitData.passiveSkillLevel_2 += levelCount;
                    break;
            }
        }

        public int GetSkillLevel(UnitSkillType skillType)
        {
            switch (skillType)
            {
                case UnitSkillType.ActiveSkill_1:
                    return userUnitData.activeSkillLevel_1;
                case UnitSkillType.ActiveSkill_2:
                    return userUnitData.activeSkillLevel_2;
                case UnitSkillType.PassiveSkill_1:
                    return userUnitData.passiveSkillLevel_1;
                case UnitSkillType.PassiveSkill_2:
                    return userUnitData.passiveSkillLevel_2;
            }

            return 1;
        }


        //===========================================================
        // EquipmentMethod
        //===========================================================
        public bool IsItemEquipment(EquipmentItemType equipmentType)
        {
            switch (equipmentType)
            {
                case EquipmentItemType.Weapon:
                    return WeaponData != null;
                case EquipmentItemType.Helmet:
                    return HelmetData != null; 
                case EquipmentItemType.Armor:
                    return ArmorData != null;
                case EquipmentItemType.Amulet:
                    return AmuletData != null;
                case EquipmentItemType.Ring:
                    return RingData != null;
                case EquipmentItemType.Shoe:
                    return ShoeData != null;
            }

            return false;
        }

        public EquipmentItemData ChangeEquipment(EquipmentItemType changeType, EquipmentItemData changeData)
        {
            EquipmentItemData existingEquipment = ReleaseEquipment(changeType);

            switch (changeType)
            {
                case EquipmentItemType.Weapon:
                    WeaponData = changeData as WeaponData;
                    break;
                case EquipmentItemType.Helmet:
                    HelmetData = changeData as HelmetData;
                    break;
                case EquipmentItemType.Armor:
                    ArmorData = changeData as ArmorData;
                    break;
                case EquipmentItemType.Amulet:
                    AmuletData = changeData as AmuletData;
                    break;
                case EquipmentItemType.Ring:
                    RingData = changeData as RingData;
                    break;
                case EquipmentItemType.Shoe:
                    ShoeData = changeData as ShoeData;
                    break;
            }
            return existingEquipment;
        }

        public EquipmentItemData ReleaseEquipment(EquipmentItemType releaseType)
        {
            EquipmentItemData existingEquipment = null;
            switch (releaseType)
            {
                case EquipmentItemType.Weapon:
                    existingEquipment = WeaponData;
                    WeaponData = null;
                    break;
                case EquipmentItemType.Helmet:
                    existingEquipment = HelmetData;
                    HelmetData = null;
                    break;
                case EquipmentItemType.Armor:
                    existingEquipment = ArmorData;
                    ArmorData = null;
                    break;
                case EquipmentItemType.Amulet:
                    existingEquipment = AmuletData;
                    AmuletData = null;
                    break;
                case EquipmentItemType.Ring:
                    existingEquipment = RingData;
                    RingData = null;
                    break;
                case EquipmentItemType.Shoe:
                    existingEquipment = ShoeData;
                    ShoeData = null;
                    break;
            }

            return existingEquipment;
        }
    }
}