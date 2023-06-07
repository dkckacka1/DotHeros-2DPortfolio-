using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] EquipmentCreateData normalCreateData;
        [SerializeField] EquipmentCreateData rareCreateData;
        [SerializeField] EquipmentCreateData uniqueCreateData;
        [SerializeField] EquipmentCreateData legendaryCreateData;

        public T CreateEquipmentItemData<T>(GradeType itemGrade) where T : EquipmentItemData, new()
        {
            T newData = new T();
            EquipmentCreateData creator = null;

            newData.equipmentGrade = itemGrade;

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
            {
                SetNewPropertyRound(ref (newData as WeaponData).attackPoint, creator.attackPoint.min, creator.attackPoint.max);
                newData.equipmentType = EquipmentItemType.Weapon;
            }
            else if (newData is HelmetData)
            {
                SetNewPropertyRound(ref (newData as HelmetData).healthPoint, creator.healthPoint.min, creator.healthPoint.max);
                newData.equipmentType = EquipmentItemType.Helmet;
            }
            else if (newData is ArmorData)
            {
                SetNewPropertyRound(ref (newData as ArmorData).defencePoint, creator.defencePoint.min, creator.defencePoint.max);
                newData.equipmentType = EquipmentItemType.Armor;
            }
            else if (newData is ShoeData)
            {
                SetNewPropertyRound(ref (newData as ShoeData).speed, creator.speed.min, creator.speed.max);
                newData.equipmentType = EquipmentItemType.Shoe;
            }
            else if (newData is AmuletData)
            {
                SetNewProperty(ref (newData as AmuletData).criticalPercent, creator.criticalPercent.min, creator.criticalPercent.max);
                SetNewProperty(ref (newData as AmuletData).criticalDamage, creator.criticalDamage.min, creator.criticalDamage.max);
                newData.equipmentType = EquipmentItemType.Amulet;
            }
            else if (newData is RingData)
            {
                SetNewProperty(ref (newData as RingData).effectHit, creator.effectHit.min, creator.effectHit.max);
                SetNewProperty(ref (newData as RingData).effectResistance, creator.effectRes.min, creator.effectRes.max);
                newData.equipmentType = EquipmentItemType.Ring;
            }
            else
            {
                Debug.LogWarning("CreateItem Error #2");
                return null;
            }

            newData.setType = (SetType)Random.Range(0, (int)SetType.Count);

            return newData;
        }

        public void ReinforceEquipment(EquipmentItemData data)
        {
            data.reinforceCount++;

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

        // TODO ��� ��ȭ�ϰ� �����ؾ���
        private EquipmentOptionStat[] GetEquipmentOptionStat(EquipmentItemData data)
        {
            List<EquipmentOptionStat> optionStats = new List<EquipmentOptionStat>();
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

            optionStats.Remove(optionStats.Find(item => item == data.optionStat_1_Type));
            optionStats.Remove(optionStats.Find(item => item == data.optionStat_2_Type));
            optionStats.Remove(optionStats.Find(item => item == data.optionStat_3_Type));

            return optionStats.ToArray();
        }



        private void AddOption(ref EquipmentOptionStat optionStat, ref float optionValue, EquipmentOptionStat[] options, GradeType itemGrade)
        {
            optionStat = options[Random.Range(0, options.Length)];
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

        private void SetNewProperty(ref float value, float min, float max)
        {
            value = Mathf.Floor(Random.Range(min, max) * 100f) / 100f;
        }

        private void SetNewPropertyRound(ref float value, float min, float max)
        {
            value = Mathf.Round(Random.Range(min, max));
        }
    }
}