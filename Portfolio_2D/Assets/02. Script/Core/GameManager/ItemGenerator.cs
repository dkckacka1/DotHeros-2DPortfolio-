using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ORDER : ScriptableObject로 만든 랜덤 데이터 생성기를 통한 랜덤 아이템 데이터 생성 구현
/*
 *  아이템 생성 클래스
 *  아이템 생성 데이타 클래스를 통해 랜덤한 아이템을 생성한다.
 */

namespace Portfolio
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] EquipmentCreateData normalCreateData;      // 평범 등급 아이템 랜덤 데이타 생성기
        [SerializeField] EquipmentCreateData rareCreateData;        // 희귀 등급 아이템 랜덤 데이타 생성기
        [SerializeField] EquipmentCreateData uniqueCreateData;      // 고유 등급 아이템 랜덤 데이타 생성기
        [SerializeField] EquipmentCreateData legendaryCreateData;   // 전설 등급 아이템 랜덤 데이타 생성기

        // 장비 데이타를 생성하는 클래스 new() 제한자를 넣어야 클래스를 생성할 수 있다.
        public T CreateEquipmentItemData<T>(GradeType itemGrade) where T : EquipmentItemData, new()
        {
            // 새로운 클래스
            T newData = new T();
            EquipmentCreateData creator = null;

            newData.equipmentGrade = itemGrade;

            // 만들 려는 등급에 맞는 랜덤 데이터 생성기를 참조한다.
            switch (itemGrade)
            {
                case GradeType.Normal:
                    creator = this.normalCreateData;
                    break;
                case GradeType.Rare:
                    creator = this.rareCreateData;
                    break;
                case GradeType.Unique:
                    creator = this.uniqueCreateData;
                    break;
                case GradeType.Legendary:
                    creator = this.legendaryCreateData;
                    break;
                default:
                    Debug.LogWarning("CreateItem Error #1");
                    return null;
            }

            if (newData is WeaponData)
                // 만들려는 데이터가 무기데이터 라면
            {
                // 주 스텟은 공격력, 랜덤한 능력치 입력
                SetNewPropertyRound(ref (newData as WeaponData).attackPoint, creator.attackPoint.min, creator.attackPoint.max);
                newData.equipmentType = EquipmentItemType.Weapon;
            }
            else if (newData is HelmetData)
                // 만들려는 데이터가 헬멧데이터 라면
            {
                // 주 스텟은 생명력, 랜덤한 능력치 입력
                SetNewPropertyRound(ref (newData as HelmetData).healthPoint, creator.healthPoint.min, creator.healthPoint.max);
                newData.equipmentType = EquipmentItemType.Helmet;
            }
            else if (newData is ArmorData)
                // 만들려는 데이터가 갑옷데이터 라면
            {
                // 주 스텟은 방어력, 랜덤한 능력치 입력
                SetNewPropertyRound(ref (newData as ArmorData).defencePoint, creator.defencePoint.min, creator.defencePoint.max);
                newData.equipmentType = EquipmentItemType.Armor;
            }
            else if (newData is ShoeData)
                // 만들려는 데이터가 신발데이터 라면
            {
                // 주 스텟은 속도, 랜덤한 능력치 입력
                SetNewPropertyRound(ref (newData as ShoeData).speed, creator.speed.min, creator.speed.max);
                newData.equipmentType = EquipmentItemType.Shoe;
            }
            else if (newData is AmuletData)
                // 만들려는 데이터가 목걸이데이터 라면
            {
                // 주 스텟은 치명타 확률, 데미지, 랜덤한 능력치 입력
                SetNewProperty(ref (newData as AmuletData).criticalPercent, creator.criticalPercent.min, creator.criticalPercent.max);
                SetNewProperty(ref (newData as AmuletData).criticalDamage, creator.criticalDamage.min, creator.criticalDamage.max);
                newData.equipmentType = EquipmentItemType.Amulet;
            }
            else if (newData is RingData)
                // 만들려는 데이터가 반지이데이터 라면
            {
                // 주 스텟은 효과 적중, 저항력, 랜덤한 능력치 입력
                SetNewProperty(ref (newData as RingData).effectHit, creator.effectHit.min, creator.effectHit.max);
                SetNewProperty(ref (newData as RingData).effectResistance, creator.effectRes.min, creator.effectRes.max);
                newData.equipmentType = EquipmentItemType.Ring;
            }
            else
            {
                Debug.LogWarning("CreateItem Error #2");
                return null;
            }

            //랜덤한 세트 입력
            newData.setType = (SetType)Random.Range(0, (int)SetType.Count);
            return newData;
        }

        // 아ㅣㅇ템 강화
        public void ReinforceEquipment(EquipmentItemData data)
        {
            // 강화 수치 증가
            data.reinforceCount++;

            // 각 아이템에 맞는 능력치 증가
            if (data is WeaponData)
            {
                (data as WeaponData).attackPoint = Mathf.Floor((data as WeaponData).attackPoint * 1.2f);
            }
            else if (data is ArmorData)
            {
                (data as ArmorData).defencePoint = Mathf.Floor((data as ArmorData).defencePoint * 1.2f);
            }
            else if (data is HelmetData)
            {
                (data as HelmetData).healthPoint = Mathf.Floor((data as HelmetData).healthPoint * 1.2f);

            }
            else if (data is ShoeData)
            {
                (data as ShoeData).speed = Mathf.Floor((data as ShoeData).speed * 1.1f);
            }
            else if (data is AmuletData)
            {
                (data as AmuletData).criticalPercent = (data as AmuletData).criticalPercent + 0.01f;
                (data as AmuletData).criticalDamage = (data as AmuletData).criticalDamage + 0.01f;

            }
            else if (data is RingData)
            {
                (data as RingData).effectHit = (data as RingData).effectHit + 0.01f;
                (data as RingData).effectResistance = (data as RingData).effectResistance + 0.01f;
            }

            // 강화 수치가 3늘어날때마다 옵션 스탯을 하나씩 부여한다.
            if (data.reinforceCount == 3)
            {
                AddOption(ref data.optionStat_1_Type, ref data.optionStat_1_value, GetEquipmentOptionStat(data), data.equipmentGrade);
            }
            else if (data.reinforceCount == 6)
            {
                AddOption(ref data.optionStat_2_Type, ref data.optionStat_2_value, GetEquipmentOptionStat(data), data.equipmentGrade);
            }
            else if (data.reinforceCount == 9)
            {
                AddOption(ref data.optionStat_3_Type, ref data.optionStat_3_value, GetEquipmentOptionStat(data), data.equipmentGrade);
            }
            else if (data.reinforceCount == 12)
            {
                AddOption(ref data.optionStat_4_Type, ref data.optionStat_4_value, GetEquipmentOptionStat(data), data.equipmentGrade);
            }
        }

        // 아이템에 부여할 수 있는 옵션 스탯 타입들을 찾는다.
        private EquipmentOptionStat[] GetEquipmentOptionStat(EquipmentItemData data)
        {
            List<EquipmentOptionStat> optionStats = new List<EquipmentOptionStat>();
            
            // 아이템에 따라 들어갈 수 있는 옵션스탯이 정해져 있다.
            if (data is WeaponData)
            {
                optionStats.AddRange(new EquipmentOptionStat[]
                {
                    EquipmentOptionStat.AttackPercent,
                    EquipmentOptionStat.HealthPoint,
                    EquipmentOptionStat.HealthPercent,
                    EquipmentOptionStat.CriticalPercent,
                    EquipmentOptionStat.CriticalDamagePercent,
                    EquipmentOptionStat.Speed,
                    EquipmentOptionStat.EffectHitPercent,
                    EquipmentOptionStat.EffectResistancePercent,
                });
            }
            else if (data is ArmorData)
            {
                optionStats.AddRange(new EquipmentOptionStat[]
{
                    EquipmentOptionStat.HealthPoint,
                    EquipmentOptionStat.HealthPercent,
                    EquipmentOptionStat.DefencePercent,
                    EquipmentOptionStat.CriticalPercent,
                    EquipmentOptionStat.CriticalDamagePercent,
                    EquipmentOptionStat.Speed,
                    EquipmentOptionStat.EffectHitPercent,
                    EquipmentOptionStat.EffectResistancePercent,
});
            }
            else if (data is HelmetData)
            {
                optionStats.AddRange(new EquipmentOptionStat[]
{
                    EquipmentOptionStat.AttackPoint,
                    EquipmentOptionStat.AttackPercent,
                    EquipmentOptionStat.HealthPercent,
                    EquipmentOptionStat.DefencePoint,
                    EquipmentOptionStat.DefencePercent,
                    EquipmentOptionStat.CriticalPercent,
                    EquipmentOptionStat.CriticalDamagePercent,
                    EquipmentOptionStat.Speed,
                    EquipmentOptionStat.EffectHitPercent,
                    EquipmentOptionStat.EffectResistancePercent,
});
            }
            else if (data is ShoeData)
            {
                optionStats.AddRange(new EquipmentOptionStat[]
{
                    EquipmentOptionStat.AttackPoint,
                    EquipmentOptionStat.AttackPercent,
                    EquipmentOptionStat.HealthPoint,
                    EquipmentOptionStat.HealthPercent,
                    EquipmentOptionStat.DefencePoint,
                    EquipmentOptionStat.DefencePercent,
                    EquipmentOptionStat.CriticalPercent,
                    EquipmentOptionStat.CriticalDamagePercent,
                    EquipmentOptionStat.EffectHitPercent,
                    EquipmentOptionStat.EffectResistancePercent,
});
            }
            else if (data is AmuletData)
            {
                optionStats.AddRange(new EquipmentOptionStat[]
{
                    EquipmentOptionStat.AttackPoint,
                    EquipmentOptionStat.AttackPercent,
                    EquipmentOptionStat.HealthPoint,
                    EquipmentOptionStat.HealthPercent,
                    EquipmentOptionStat.DefencePoint,
                    EquipmentOptionStat.DefencePercent,
                    EquipmentOptionStat.Speed,
                    EquipmentOptionStat.EffectHitPercent,
                    EquipmentOptionStat.EffectResistancePercent,
});
            }
            else if (data is RingData)
            {
                optionStats.AddRange(new EquipmentOptionStat[]
{
                    EquipmentOptionStat.AttackPoint,
                    EquipmentOptionStat.AttackPercent,
                    EquipmentOptionStat.HealthPoint,
                    EquipmentOptionStat.HealthPercent,
                    EquipmentOptionStat.DefencePoint,
                    EquipmentOptionStat.DefencePercent,
                    EquipmentOptionStat.CriticalPercent,
                    EquipmentOptionStat.CriticalDamagePercent,
                    EquipmentOptionStat.Speed,
});
            }

            // 동일한 옵션 스탯이 배정되지 않도록 한다.
            optionStats.Remove(optionStats.Find(item => item == data.optionStat_1_Type));
            optionStats.Remove(optionStats.Find(item => item == data.optionStat_2_Type));
            optionStats.Remove(optionStats.Find(item => item == data.optionStat_3_Type));

            // 뽑은 랜덤 옵션 스탯 타입 배열을 리턴
            return optionStats.ToArray();
        }



        // 옵션 스탯을 더해준다.
        private void AddOption(ref EquipmentOptionStat optionStat, ref float optionValue, EquipmentOptionStat[] options, GradeType itemGrade)
        {
            // 나올수 있는 옵션 스탯 타입중 하나를 랜덤하게 뽑는다.
            optionStat = options[Random.Range(0, options.Length)];
            // 등급에 맞는 랜덤아이템 데이타 생성기를 참조한다.
            EquipmentCreateData creator = null;
            switch (itemGrade)
            {
                case GradeType.Normal:
                    creator = this.normalCreateData;
                    break;
                case GradeType.Rare:
                    creator = this.rareCreateData;
                    break;
                case GradeType.Unique:
                    creator = this.uniqueCreateData;
                    break;
                case GradeType.Legendary:
                    creator = this.legendaryCreateData;
                    break;
            }

            // 옵션 스탯을 부여한다.
            switch (optionStat)
            {
                case EquipmentOptionStat.AttackPoint:
                    optionValue = Mathf.Floor(Random.Range(creator.optionAttackPoint.min, creator.optionAttackPoint.max));
                    break;
                case EquipmentOptionStat.AttackPercent:
                    optionValue = Random.Range(creator.optionAttackPercent.min, creator.optionAttackPercent.max);
                    break;
                case EquipmentOptionStat.HealthPoint:
                    optionValue = Mathf.Floor(Random.Range(creator.optionHealthPoint.min, creator.optionHealthPoint.max));
                    break;
                case EquipmentOptionStat.HealthPercent:
                    optionValue = Random.Range(creator.optionHealthPercent.min, creator.optionHealthPercent.max);
                    break;
                case EquipmentOptionStat.DefencePoint:
                    optionValue = Mathf.Floor(Random.Range(creator.optionDefencePoint.min, creator.optionDefencePoint.max));
                    break;
                case EquipmentOptionStat.DefencePercent:
                    optionValue = Random.Range(creator.optionDefencePercent.min, creator.optionDefencePercent.max);
                    break;
                case EquipmentOptionStat.CriticalPercent:
                    optionValue = Random.Range(creator.optionCriticalPercent.min, creator.optionCriticalPercent.max);
                    break;
                case EquipmentOptionStat.CriticalDamagePercent:
                    optionValue = Random.Range(creator.optionCriticalDamage.min, creator.optionCriticalDamage.max);
                    break;
                case EquipmentOptionStat.Speed:
                    optionValue = Mathf.Floor(Random.Range(creator.optionSpeed.min, creator.optionSpeed.max));
                    break;
                case EquipmentOptionStat.EffectHitPercent:
                    optionValue = Random.Range(creator.optionEffectHit.min, creator.optionEffectHit.max);
                    break;
                case EquipmentOptionStat.EffectResistancePercent:
                    optionValue = Random.Range(creator.optionEffectRes.min, creator.optionEffectRes.max);
                    break;
            }
        }

        // 퍼센트를 표시할 때 소수점 없애기
        private void SetNewProperty(ref float value, float min, float max)
        {
            value = Mathf.Floor(Random.Range(min, max) * 100f) / 100f;
        }

        // 일반 숫자 표시할 때 소수점 없애기
        private void SetNewPropertyRound(ref float value, float min, float max)
        {
            value = Mathf.Round(Random.Range(min, max));
        }
    }
}