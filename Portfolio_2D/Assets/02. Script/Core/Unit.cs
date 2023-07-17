using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Portfolio.skill;
using System.Linq;

/*
 * ���� �����͸� ���� ���� ���� Ŭ����
 */

namespace Portfolio
{
    public class Unit
    {
        private UnitData unitData;              // ���� ������
        private UserUnitData userUnitData;      // ���� ���� ������
        private int? designatedLevel;           // ���� ���� �����Ͱ� ���ٸ� ǥ���� �⺻ ����
        private int? designatedGrade;           // ���� ���� �����Ͱ� ���ٸ� ǥ���� �⺻ ���

        public ActiveSkill basicAttackSkill;    // �⺻ ���� ��ų
        public ActiveSkill activeSkill_1;       // ��Ƽ�� ��ų 1
        public ActiveSkill activeSkill_2;       // ��Ƽ�� ��ų 2
        public PassiveSkill passiveSkill_1;     // �нú� ��ų 1
        public PassiveSkill passiveSkill_2;     // �нú� ��ų 2

        //===========================================================
        // Equipment
        //===========================================================
        // ��� �������� ���� ���� �����Ϳ��� �����´�.
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
        public Sprite portraitSprite;                       // ���� ��Ʈ����Ʈ �̹���
        public RuntimeAnimatorController animController;    // ���� �ִϸ��̼� ��Ʈ�ѷ�

        //===========================================================
        // Property
        //===========================================================
        public UserUnitData UserData => userUnitData;
        public int UnitID => unitData.ID;
        public string UnitName => unitData.unitName;
        public int UnitCurrentLevel
        {
            get
            {
                if (userUnitData != null)
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // �������ֵ������� ���� ���� ����
                    return userUnitData.unitLevel;
                }
                else
                // ���� ���� �����Ͱ� ���ٸ�
                {
                    if (designatedLevel != null)
                    // �⺻ ������ �ִٸ�
                    {
                        // �⺻ ���� ǥ��
                        return (int)designatedLevel;
                    }
                    else
                    {
                        // ���ٸ� 1 ������
                        return 1;
                    }
                }
            }
            set
            {
                if (userUnitData != null)
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // UnitMaxLevel �̳��� ������ ����
                    userUnitData.unitLevel = Mathf.Clamp(value, 1, UnitMaxLevel);
                }
                else
                // ���� ���� �����Ͱ� ���ٸ�
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
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // ���� ���� �������� ��� ����
                    return userUnitData.unitGrade;
                }
                else
                // ���� ���� �����Ͱ� ���ٸ�
                {
                    if (designatedGrade != null)
                    // �⺻ ���� ����� �ִٸ� 
                    {
                        // �⺻ ���� ��� ����
                        return (int)designatedGrade;
                    }
                    else
                    {
                        // ������ ���� ��� ����
                        return unitData.defaultGrade;
                    }
                }
            }
            set
            {
                if (userUnitData != null)
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // ���� ������ ����� ����
                    userUnitData.unitGrade = Mathf.Clamp(value, 1, 5);
                }
                else
                // ���� ���� �����Ͱ� ���ٸ�
                {
                    // �⺻ ���� ����� ����
                    designatedGrade = Mathf.Clamp(value, 1, 5);
                    return;
                }
            }
        }
        public int UnitMaxLevel
        {
            get
            {
                if (userUnitData != null)
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // ���� ���� ��� * 10�� �ִ� ����
                    return userUnitData.unitGrade * 10;
                }
                else
                // ���� ���� �����Ͱ� ���ٸ�
                {
                    // 1 ������
                    return 1;
                }
            }
        }
        public bool IsMaxLevel
        {
            get
            {
                // ���� ���� ������ �ִ� �������� Ȯ��
                return UnitCurrentLevel == UnitMaxLevel;
            }
        }
        public float CurrentExperience
        {
            get
            {
                if (userUnitData != null)
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // ���� ���� �������� ����ġ ����
                    return userUnitData.unitExperience;
                }
                else
                // ���� ���� �����Ͱ� ���ٸ�
                {
                    // 0 ����
                    return 0;
                }
            }
            set
            {
                if (userUnitData != null)
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    if (value >= MaxExperience)
                    // ���� �ִ� ����ġ ���� �Ѿ� ���ٸ�
                    {
                        if (IsMaxLevel)
                        // �̹� �ִ� ���� �� ���
                        {
                            // ����ġ�� �ƽ� ����ġ�� ����
                            userUnitData.unitExperience = MaxExperience;
                        }
                        else
                        {
                            // ����ġ�� �ƽ� ����ġ�� �� ������
                            userUnitData.unitExperience = value - MaxExperience;
                            // ������ �Ѵ�.
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
                // ���� ���� ���� * 1000f ����
                return UnitCurrentLevel * Constant.UnitLevelUpExperienceValue;
            }
        }

        public float AttackPoint
        {
            get
            {
                // ���� ���ݷ� ��������
                float returnValue = GetProperty(unitData.attackPoint);
                // ��� �ɷ�ġ ��������
                if (WeaponData != null)
                {
                    returnValue += WeaponData.attackPoint;
                }

                // �տ��� ���
                returnValue += GetItemOptionValue(eEquipmentOptionStat.AttackPoint);
                // ������ ���
                returnValue *= (1 + GetItemOptionValue(eEquipmentOptionStat.AttackPercent));

                // �Ҽ��� ������
                return Mathf.Round(returnValue);
            }
        }
        public float HealthPoint
        {
            get
            {
                float returnValue = GetProperty(unitData.maxHP);
                if (HelmetData != null)
                {
                    returnValue += HelmetData.healthPoint;
                }

                returnValue += GetItemOptionValue(eEquipmentOptionStat.HealthPoint);
                returnValue *= 1 + GetItemOptionValue(eEquipmentOptionStat.HealthPercent);
                return Mathf.Round(returnValue); ;
            }
        }
        public float DefencePoint
        {
            get
            {
                float returnValue = GetProperty(unitData.defencePoint);
                if (ArmorData != null)
                {
                    returnValue += ArmorData.defencePoint;
                }

                returnValue += GetItemOptionValue(eEquipmentOptionStat.DefencePoint);
                returnValue *= 1 + GetItemOptionValue(eEquipmentOptionStat.DefencePercent);
                return Mathf.Round(returnValue);
            }
        }
        public float Speed
        {
            get
            {
                float returnValue = unitData.speed;
                if (ShoeData != null)
                {
                    returnValue += ShoeData.speed;
                }

                returnValue += GetItemOptionValue(eEquipmentOptionStat.Speed);
                return Mathf.Round(returnValue);
            }
        }
        public float CriticalPercent
        {
            get
            {
                float returnValue = unitData.criticalPercent;
                if (AmuletData != null)
                {
                    returnValue += AmuletData.criticalPercent;
                }

                returnValue += GetItemOptionValue(eEquipmentOptionStat.CriticalPercent);
                // ġ��Ÿ Ȯ���� �ִ� ��ġ�� 80%
                return Mathf.Clamp(returnValue, 0, 0.8f);
            }
        }
        public float CriticalDamage
        {
            get
            {
                float returnValue = unitData.criticalDamage;

                if (AmuletData != null)
                {
                    returnValue += AmuletData.criticalDamage;
                }

                returnValue += GetItemOptionValue(eEquipmentOptionStat.CriticalDamagePercent);
                // ġ��Ÿ ���ݷ��� �ִ� ��ġ�� 150%
                return Mathf.Clamp(returnValue, 0, 1.5f);
            }
        }
        public float EffectHit
        {
            get
            {
                float returnValue = unitData.effectHit;
                if (RingData != null)
                {
                    returnValue += RingData.effectHit;
                }

                returnValue += GetItemOptionValue(eEquipmentOptionStat.EffectHitPercent);
                // ȿ�� ������ �ִ� ��ġ�� 200%
                return Mathf.Clamp(returnValue, 0, 2.0f);
            }
        }
        public float EffectResistance
        {
            get
            {
                float returnValue = unitData.effectResistance;
                if (RingData != null)
                {
                    returnValue += RingData.effectResistance;
                }

                returnValue += GetItemOptionValue(eEquipmentOptionStat.EffectResistancePercent);
                // ȿ�� ���׷��� �ִ� ��ġ�� 100%
                return Mathf.Clamp(returnValue, 0, 1f);
            }
        }
        public int ActiveSkillLevel_1
        {
            get
            {
                if (userUnitData != null)
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // ���� ���� �������� ��Ƽ�� ��ų���� 1 ����
                    return userUnitData.activeSkillLevel_1;
                }
                else
                {
                    // ���ٸ� 1 ����
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // ���� ���� �������� ��Ƽ�� ��ų���� 1 ����
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
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // ���� ���� �������� ��Ƽ�� ��ų���� 2 ����
                    return userUnitData.activeSkillLevel_2;
                }
                else
                {
                    // ���ٸ� 1 ����
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // ���� ���� �������� ��Ƽ�� ��ų���� 2 ����
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
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // ���� ���� �������� �нú� ��ų���� 1 ����
                    return userUnitData.passiveSkillLevel_1;
                }
                else
                {
                    // ���ٸ� 1 ����
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // ���� ���� �������� �нú� ��ų���� 1 ����
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
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // ���� ���� �������� �нú� ��ų���� 2 ����
                    return userUnitData.passiveSkillLevel_2;
                }
                else
                {
                    // ���ٸ� 1 ����
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                // ���� ���� �����Ͱ� �ִٸ�
                {
                    // ���� ���� �������� �нú� ��ų���� 2 ����
                    userUnitData.passiveSkillLevel_2 = value;
                }
                else
                {
                    Debug.LogWarning("userUnitData = null");
                    return;
                }
            }
        }
        public float battlePower
        {
            get
            {
                // ((���ݷ� * 1.6 + ���ݷ� * 1.6 * ġ��Ȯ�� * ġ�����ط�) * (1 + (�ӵ� - 45) * 0.02) + ä�� + ���� * 9.3) * (1 + (���� + ����) / 4)
                return ((AttackPoint * 1.6f + AttackPoint * 1.6f * CriticalPercent * CriticalDamage) * (1 + (Speed - 45) * 0.02f) + HealthPoint + DefencePoint * 9.3f) * (1 + (EffectHit + EffectResistance) / 4);
            }
        }

        //===========================================================
        // SetUnit
        //===========================================================
        // ���� �����͸� ���� �� ������
        public Unit(UnitData unitData)
        {
            this.unitData = unitData;
            SetUnitData(unitData);
        }

        // �� ������ ���� �� ������
        public Unit(UnitData unitData, int grade, int level)
        {
            this.unitData = unitData;
            this.designatedGrade = grade;
            this.designatedLevel = level;
            SetUnitData(unitData);
        }

        // ������ ������ �ִ� ���� ������ ������ ������
        public Unit(UnitData unitData, UserUnitData userUnitData)
        {
            this.unitData = unitData;
            this.userUnitData = userUnitData;

            WeaponData = userUnitData.weaponData;
            HelmetData = userUnitData.helmetData;
            ArmorData = userUnitData.armorData;
            AmuletData = userUnitData.amuletData;
            RingData = userUnitData.ringData;
            ShoeData = userUnitData.shoeData;

            SetUnitData(unitData);
        }

        // ���ҽ� ����
        private void SetUnitData(UnitData unitData)
        {
            portraitSprite = GameManager.Instance.GetSprite(this.unitData.portraitImageName);
            animController = GameManager.Instance.GetAnimController(this.unitData.animationName);
            GameManager.Instance.TryGetSkill(unitData.basicAttackSKillID, out basicAttackSkill);
            GameManager.Instance.TryGetSkill(unitData.activeSkillID_1, out activeSkill_1);
            GameManager.Instance.TryGetSkill(unitData.activeSkillID_2, out activeSkill_2);
            GameManager.Instance.TryGetSkill(unitData.passiveSkillID_1, out passiveSkill_1);
            GameManager.Instance.TryGetSkill(unitData.passiveSkillID_2, out passiveSkill_2);
        }

        // ���� ��ް� ���� ������ ���缭 ���ݰ� ���� (���ݷ�, �����, ���¸� �ش�)
        public float GetProperty(float DefaultValue)
        {
            if (userUnitData != null)
            {
                return DefaultValue * (1 + ((userUnitData.unitLevel - 1) * unitData.levelValue)) * (1 + ((userUnitData.unitGrade - 1) * unitData.gradeValue));
            }
            else
            {
                return DefaultValue;
            }
        }

        // ����� �ɼ� ���� ��������
        public float GetItemOptionValue(eEquipmentOptionStat optionStatType)
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

        // ������ ��� ������ ���� ��������
        public IEnumerable<EquipmentItemData> GetEuqipmentList()
        {
            List<EquipmentItemData> list = new List<EquipmentItemData>() { WeaponData, HelmetData, ArmorData, AmuletData, RingData, ShoeData };
            return list.Where(item => item != null);
        }

        // ���� ������
        private void LevelUP()
        {
            UnitCurrentLevel++;
        }

        //===========================================================
        // SkillMethod
        //===========================================================
        // ��ų ������
        public void SkillLevelUp(eUnitSkillType skillType, int levelCount = 1)
        {
            switch (skillType)
            {
                case eUnitSkillType.ActiveSkill_1:
                    userUnitData.activeSkillLevel_1 += levelCount;
                    break;
                case eUnitSkillType.ActiveSkill_2:
                    userUnitData.activeSkillLevel_2 += levelCount;
                    break;
                case eUnitSkillType.PassiveSkill_1:
                    userUnitData.passiveSkillLevel_1 += levelCount;
                    break;
                case eUnitSkillType.PassiveSkill_2:
                    userUnitData.passiveSkillLevel_2 += levelCount;
                    break;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }
        }

        // ��ų ������ �����´�.
        public int GetSkillLevel(eUnitSkillType skillType)
        {
            if (userUnitData == null)
                // ���� ���� ����Ÿ�� ���ٸ� 1 ����
            {
                return 1;
            }

            switch (skillType)
            {
                case eUnitSkillType.ActiveSkill_1:
                    return userUnitData.activeSkillLevel_1;
                case eUnitSkillType.ActiveSkill_2:
                    return userUnitData.activeSkillLevel_2;
                case eUnitSkillType.PassiveSkill_1:
                    return userUnitData.passiveSkillLevel_1;
                case eUnitSkillType.PassiveSkill_2:
                    return userUnitData.passiveSkillLevel_2;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }

            return 1;
        }


        //===========================================================
        // EquipmentMethod
        //===========================================================
        // ���������� �����ϰ� �ִ��� Ȯ��
        public bool IsItemEquipment(eEquipmentItemType equipmentType)
        {
            switch (equipmentType)
            {
                case eEquipmentItemType.Weapon:
                    return WeaponData != null;
                case eEquipmentItemType.Helmet:
                    return HelmetData != null;
                case eEquipmentItemType.Armor:
                    return ArmorData != null;
                case eEquipmentItemType.Amulet:
                    return AmuletData != null;
                case eEquipmentItemType.Ring:
                    return RingData != null;
                case eEquipmentItemType.Shoe:
                    return ShoeData != null;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }

            return false;
        }

        // ������ ������ �ִ� ��� ���������� ��ȯ�Ѵ�.
        public List<EquipmentItemData> GetAllEquipmentItem()
        {
            List<EquipmentItemData> equipmentItemList = new List<EquipmentItemData>();

            if (WeaponData != null) equipmentItemList.Add(WeaponData);
            if (ArmorData != null)  equipmentItemList.Add(ArmorData);
            if (HelmetData != null) equipmentItemList.Add(HelmetData);
            if (ShoeData != null)   equipmentItemList.Add(ShoeData);
            if (AmuletData != null) equipmentItemList.Add(AmuletData);
            if (RingData != null)   equipmentItemList.Add(RingData);

            return equipmentItemList;
        }

        // �������� �������ֱ�
        public EquipmentItemData ChangeEquipment(eEquipmentItemType changeType, EquipmentItemData changeData)
        {
            // �̹� �������̶�� �����Ѵ�.
            EquipmentItemData existingEquipment = ReleaseEquipment(changeType);

            switch (changeType)
            {
                case eEquipmentItemType.Weapon:
                    WeaponData = changeData as WeaponData;
                    break;
                case eEquipmentItemType.Helmet:
                    HelmetData = changeData as HelmetData;
                    break;
                case eEquipmentItemType.Armor:
                    ArmorData = changeData as ArmorData;
                    break;
                case eEquipmentItemType.Amulet:
                    AmuletData = changeData as AmuletData;
                    break;
                case eEquipmentItemType.Ring:
                    RingData = changeData as RingData;
                    break;
                case eEquipmentItemType.Shoe:
                    ShoeData = changeData as ShoeData;
                    break;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }

            // ���� ������ ���
            return existingEquipment;
        }

        // ��� �����Ѵ�.
        public EquipmentItemData ReleaseEquipment(eEquipmentItemType releaseType)
        {
            EquipmentItemData existingEquipment = null;
            switch (releaseType)
            {
                case eEquipmentItemType.Weapon:
                    existingEquipment = WeaponData;
                    WeaponData = null;
                    break;
                case eEquipmentItemType.Helmet:
                    existingEquipment = HelmetData;
                    HelmetData = null;
                    break;
                case eEquipmentItemType.Armor:
                    existingEquipment = ArmorData;
                    ArmorData = null;
                    break;
                case eEquipmentItemType.Amulet:
                    existingEquipment = AmuletData;
                    AmuletData = null;
                    break;
                case eEquipmentItemType.Ring:
                    existingEquipment = RingData;
                    RingData = null;
                    break;
                case eEquipmentItemType.Shoe:
                    existingEquipment = ShoeData;
                    ShoeData = null;
                    break;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }

            // ���� ������ ��� ����
            return existingEquipment;
        }
    }
}