using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Portfolio.skill;
using System.Linq;

/*
 * 유저 데이터를 토대로 만든 유닛 클래스
 */

namespace Portfolio
{
    public class Unit
    {
        private UnitData unitData;              // 유닛 데이터
        private UserUnitData userUnitData;      // 유저 유닛 데이터
        private int? designatedLevel;           // 유저 유닛 데이터가 없다면 표시할 기본 레벨
        private int? designatedGrade;           // 유저 유닛 데이터가 없다면 표시할 기본 등급

        public ActiveSkill basicAttackSkill;    // 기본 공격 스킬
        public ActiveSkill activeSkill_1;       // 액티브 스킬 1
        public ActiveSkill activeSkill_2;       // 액티브 스킬 2
        public PassiveSkill passiveSkill_1;     // 패시브 스킬 1
        public PassiveSkill passiveSkill_2;     // 패시브 스킬 2

        //===========================================================
        // Equipment
        //===========================================================
        // 장비 아이템은 유저 유닛 데이터에서 가져온다.
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
        public Sprite portraitSprite;                       // 유닛 포트레이트 이미지
        public RuntimeAnimatorController animController;    // 유닛 애니메이션 컨트롤러

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
                // 유저 유닛 데이터가 있다면
                {
                    // 유저유닛데이터의 유닛 레벨 리턴
                    return userUnitData.unitLevel;
                }
                else
                // 유저 유닛 데이터가 없다면
                {
                    if (designatedLevel != null)
                    // 기본 레벨이 있다면
                    {
                        // 기본 레벨 표시
                        return (int)designatedLevel;
                    }
                    else
                    {
                        // 없다면 1 고정값
                        return 1;
                    }
                }
            }
            set
            {
                if (userUnitData != null)
                // 유저 유닛 데이터가 있다면
                {
                    // UnitMaxLevel 이내의 값으로 설정
                    userUnitData.unitLevel = Mathf.Clamp(value, 1, UnitMaxLevel);
                }
                else
                // 유저 유닛 데이터가 없다면
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
                // 유저 유닛 데이터가 있다면
                {
                    // 유저 유닛 데이터의 등급 리턴
                    return userUnitData.unitGrade;
                }
                else
                // 유저 유닛 데이터가 없다면
                {
                    if (designatedGrade != null)
                    // 기본 유닛 등급이 있다면 
                    {
                        // 기본 유닛 등급 리턴
                        return (int)designatedGrade;
                    }
                    else
                    {
                        // 데이터 상의 등급 리턴
                        return unitData.defaultGrade;
                    }
                }
            }
            set
            {
                if (userUnitData != null)
                // 유저 유닛 데이터가 있다면
                {
                    // 유저 유닛의 등급을 설정
                    userUnitData.unitGrade = Mathf.Clamp(value, 1, 5);
                }
                else
                // 유저 유닛 데이터가 없다면
                {
                    // 기본 유닛 등급을 설정
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
                // 유저 유닛 데이터가 있다면
                {
                    // 유저 유닛 등급 * 10이 최대 레벨
                    return userUnitData.unitGrade * 10;
                }
                else
                // 유저 유닛 데이터가 없다면
                {
                    // 1 고정값
                    return 1;
                }
            }
        }
        public bool IsMaxLevel
        {
            get
            {
                // 유닛 현재 레벨이 최대 레벨인지 확인
                return UnitCurrentLevel == UnitMaxLevel;
            }
        }
        public float CurrentExperience
        {
            get
            {
                if (userUnitData != null)
                // 유저 유닛 데이터가 있다면
                {
                    // 유저 유닛 데이터의 경험치 리턴
                    return userUnitData.unitExperience;
                }
                else
                // 유저 유닛 데이터가 없다면
                {
                    // 0 고정
                    return 0;
                }
            }
            set
            {
                if (userUnitData != null)
                // 유저 유닛 데이터가 있다면
                {
                    if (value >= MaxExperience)
                    // 만약 최대 경험치 량을 넘어 선다면
                    {
                        if (IsMaxLevel)
                        // 이미 최대 레벨 일 경우
                        {
                            // 경험치는 맥스 경험치로 고정
                            userUnitData.unitExperience = MaxExperience;
                        }
                        else
                        {
                            // 경험치는 맥스 경험치를 뺀 나머지
                            userUnitData.unitExperience = value - MaxExperience;
                            // 레벨업 한다.
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
                // 유저 현재 레벨 * 1000f 리턴
                return UnitCurrentLevel * Constant.UnitLevelUpExperienceValue;
            }
        }

        public float AttackPoint
        {
            get
            {
                // 유닛 공격력 가져오기
                float returnValue = GetProperty(unitData.attackPoint);
                // 장비 능력치 가져오기
                if (WeaponData != null)
                {
                    returnValue += WeaponData.attackPoint;
                }

                // 합연산 계산
                returnValue += GetItemOptionValue(eEquipmentOptionStat.AttackPoint);
                // 곱연산 계산
                returnValue *= (1 + GetItemOptionValue(eEquipmentOptionStat.AttackPercent));

                // 소수점 버리기
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
                // 치명타 확률의 최대 수치는 80%
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
                // 치명타 공격력의 최대 수치는 150%
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
                // 효과 적중의 최대 수치는 200%
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
                // 효과 저항력의 최대 수치는 100%
                return Mathf.Clamp(returnValue, 0, 1f);
            }
        }
        public int ActiveSkillLevel_1
        {
            get
            {
                if (userUnitData != null)
                // 유저 유닛 데이터가 있다면
                {
                    // 유저 유닛 데이터의 액티브 스킬레벨 1 리턴
                    return userUnitData.activeSkillLevel_1;
                }
                else
                {
                    // 없다면 1 고정
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                // 유저 유닛 데이터가 있다면
                {
                    // 유저 유닛 데이터의 액티브 스킬레벨 1 세팅
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
                // 유저 유닛 데이터가 있다면
                {
                    // 유저 유닛 데이터의 액티브 스킬레벨 2 리턴
                    return userUnitData.activeSkillLevel_2;
                }
                else
                {
                    // 없다면 1 고정
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                // 유저 유닛 데이터가 있다면
                {
                    // 유저 유닛 데이터의 액티브 스킬레벨 2 세팅
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
                // 유저 유닛 데이터가 있다면
                {
                    // 유저 유닛 데이터의 패시브 스킬레벨 1 리턴
                    return userUnitData.passiveSkillLevel_1;
                }
                else
                {
                    // 없다면 1 고정
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                // 유저 유닛 데이터가 있다면
                {
                    // 유저 유닛 데이터의 패시브 스킬레벨 1 설정
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
                // 유저 유닛 데이터가 있다면
                {
                    // 유저 유닛 데이터의 패시브 스킬레벨 2 리턴
                    return userUnitData.passiveSkillLevel_2;
                }
                else
                {
                    // 없다면 1 고정
                    return 1;
                }
            }
            set
            {
                if (userUnitData != null)
                // 유저 유닛 데이터가 있다면
                {
                    // 유저 유닛 데이터의 패시브 스킬레벨 2 설정
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
                // ((공격력 * 1.6 + 공격력 * 1.6 * 치명확률 * 치명피해량) * (1 + (속도 - 45) * 0.02) + 채력 + 방어력 * 9.3) * (1 + (저항 + 적중) / 4)
                return ((AttackPoint * 1.6f + AttackPoint * 1.6f * CriticalPercent * CriticalDamage) * (1 + (Speed - 45) * 0.02f) + HealthPoint + DefencePoint * 9.3f) * (1 + (EffectHit + EffectResistance) / 4);
            }
        }

        //===========================================================
        // SetUnit
        //===========================================================
        // 유닛 데이터를 토대로 한 생성자
        public Unit(UnitData unitData)
        {
            this.unitData = unitData;
            SetUnitData(unitData);
        }

        // 적 유닛을 만들 때 생성자
        public Unit(UnitData unitData, int grade, int level)
        {
            this.unitData = unitData;
            this.designatedGrade = grade;
            this.designatedLevel = level;
            SetUnitData(unitData);
        }

        // 유저가 가지고 있는 유닛 정보를 토대로한 생성자
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

        // 리소스 세팅
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

        // 유닛 등급과 유닛 레벨에 맞춰서 스텟값 리턴 (공격력, 생명력, 방어력만 해당)
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

        // 장비의 옵션 스탯 가져오기
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

        // 유닛의 장비 아이템 정보 가져오기
        public IEnumerable<EquipmentItemData> GetEuqipmentList()
        {
            List<EquipmentItemData> list = new List<EquipmentItemData>() { WeaponData, HelmetData, ArmorData, AmuletData, RingData, ShoeData };
            return list.Where(item => item != null);
        }

        // 유닛 레벨업
        private void LevelUP()
        {
            UnitCurrentLevel++;
        }

        //===========================================================
        // SkillMethod
        //===========================================================
        // 스킬 레벨업
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

        // 스킬 레벨을 가져온다.
        public int GetSkillLevel(eUnitSkillType skillType)
        {
            if (userUnitData == null)
                // 유저 유닛 데이타가 없다면 1 고정
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
        // 장비아이템을 장착하고 있는지 확인
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

        // 유닛이 가지고 있는 모든 장비아이템을 반환한다.
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

        // 장비아이템 변경해주기
        public EquipmentItemData ChangeEquipment(eEquipmentItemType changeType, EquipmentItemData changeData)
        {
            // 이미 장착중이라면 해제한다.
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

            // 장착 해제한 장비
            return existingEquipment;
        }

        // 장비를 해제한다.
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

            // 장착 해제한 장비 리턴
            return existingEquipment;
        }
    }
}