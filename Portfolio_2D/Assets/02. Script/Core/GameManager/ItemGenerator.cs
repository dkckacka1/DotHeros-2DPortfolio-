using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ORDER : #07) ScriptableObject�� ���� ���� ������ �����⸦ ���� ���� ������ ������ ���� ����
/*
 *  ������ ���� Ŭ����
 *  ������ ���� ����Ÿ Ŭ������ ���� ������ �������� �����Ѵ�.
 */

namespace Portfolio
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] EquipmentCreateData normalCreateData;      // ��� ��� ������ ���� ����Ÿ ������
        [SerializeField] EquipmentCreateData rareCreateData;        // ��� ��� ������ ���� ����Ÿ ������
        [SerializeField] EquipmentCreateData uniqueCreateData;      // ���� ��� ������ ���� ����Ÿ ������
        [SerializeField] EquipmentCreateData legendaryCreateData;   // ���� ��� ������ ���� ����Ÿ ������

        // ��� ����Ÿ�� �����ϴ� Ŭ���� new() �����ڸ� �־�� Ŭ������ ������ �� �ִ�.
        public T CreateEquipmentItemData<T>(eGradeType itemGrade) where T : EquipmentItemData, new()
        {
            // ���ο� Ŭ����
            T newData = new T();
            EquipmentCreateData creator = null;

            newData.equipmentGrade = itemGrade;

            // ���� ���� ��޿� �´� ���� ������ �����⸦ �����Ѵ�.
            switch (itemGrade)
            {
                case eGradeType.Normal:
                    creator = this.normalCreateData;
                    break;
                case eGradeType.Rare:
                    creator = this.rareCreateData;
                    break;
                case eGradeType.Unique:
                    creator = this.uniqueCreateData;
                    break;
                case eGradeType.Legendary:
                    creator = this.legendaryCreateData;
                    break;
                default:
                    Debug.LogWarning("CreateItem Error #1");
                    return null;
            }

            if (newData is WeaponData)
                // ������� �����Ͱ� ���ⵥ���� ���
            {
                // �� ������ ���ݷ�, ������ �ɷ�ġ �Է�
                SetNewPropertyRound(ref (newData as WeaponData).attackPoint, creator.attackPoint.min, creator.attackPoint.max);
                newData.equipmentType = eEquipmentItemType.Weapon;
            }
            else if (newData is HelmetData)
                // ������� �����Ͱ� ��䵥���� ���
            {
                // �� ������ �����, ������ �ɷ�ġ �Է�
                SetNewPropertyRound(ref (newData as HelmetData).healthPoint, creator.healthPoint.min, creator.healthPoint.max);
                newData.equipmentType = eEquipmentItemType.Helmet;
            }
            else if (newData is ArmorData)
                // ������� �����Ͱ� ���ʵ����� ���
            {
                // �� ������ ����, ������ �ɷ�ġ �Է�
                SetNewPropertyRound(ref (newData as ArmorData).defencePoint, creator.defencePoint.min, creator.defencePoint.max);
                newData.equipmentType = eEquipmentItemType.Armor;
            }
            else if (newData is ShoeData)
                // ������� �����Ͱ� �Źߵ����� ���
            {
                // �� ������ �ӵ�, ������ �ɷ�ġ �Է�
                SetNewPropertyRound(ref (newData as ShoeData).speed, creator.speed.min, creator.speed.max);
                newData.equipmentType = eEquipmentItemType.Shoe;
            }
            else if (newData is AmuletData)
                // ������� �����Ͱ� ����̵����� ���
            {
                // �� ������ ġ��Ÿ Ȯ��, ������, ������ �ɷ�ġ �Է�
                SetNewProperty(ref (newData as AmuletData).criticalPercent, creator.criticalPercent.min, creator.criticalPercent.max);
                SetNewProperty(ref (newData as AmuletData).criticalDamage, creator.criticalDamage.min, creator.criticalDamage.max);
                newData.equipmentType = eEquipmentItemType.Amulet;
            }
            else if (newData is RingData)
                // ������� �����Ͱ� �����̵����� ���
            {
                // �� ������ ȿ�� ����, ���׷�, ������ �ɷ�ġ �Է�
                SetNewProperty(ref (newData as RingData).effectHit, creator.effectHit.min, creator.effectHit.max);
                SetNewProperty(ref (newData as RingData).effectResistance, creator.effectRes.min, creator.effectRes.max);
                newData.equipmentType = eEquipmentItemType.Ring;
            }
            else
            {
                Debug.LogWarning("CreateItem Error #2");
                return null;
            }

            //������ ��Ʈ �Է�
            newData.setType = (eSetType)Random.Range(0, (int)eSetType.Count);
            return newData;
        }

        // ������ ��ȭ
        public void ReinforceEquipment(EquipmentItemData data)
        {
            // ��ȭ ��ġ ����
            data.reinforceCount++;

            // �� �����ۿ� �´� �ɷ�ġ ����
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

            // ��ȭ ��ġ�� 3�þ������ �ɼ� ������ �ϳ��� �ο��Ѵ�.
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

        // �����ۿ� �ο��� �� �ִ� �ɼ� ���� Ÿ�Ե��� ã�´�.
        private eEquipmentOptionStat[] GetEquipmentOptionStat(EquipmentItemData data)
        {
            List<eEquipmentOptionStat> optionStats = new List<eEquipmentOptionStat>();
            
            // �����ۿ� ���� �� �� �ִ� �ɼǽ����� ������ �ִ�.
            if (data is WeaponData)
            {
                optionStats.AddRange(new eEquipmentOptionStat[]
                {
                    eEquipmentOptionStat.AttackPercent,
                    eEquipmentOptionStat.HealthPoint,
                    eEquipmentOptionStat.HealthPercent,
                    eEquipmentOptionStat.CriticalPercent,
                    eEquipmentOptionStat.CriticalDamagePercent,
                    eEquipmentOptionStat.Speed,
                    eEquipmentOptionStat.EffectHitPercent,
                    eEquipmentOptionStat.EffectResistancePercent,
                });
            }
            else if (data is ArmorData)
            {
                optionStats.AddRange(new eEquipmentOptionStat[]
{
                    eEquipmentOptionStat.HealthPoint,
                    eEquipmentOptionStat.HealthPercent,
                    eEquipmentOptionStat.DefencePercent,
                    eEquipmentOptionStat.CriticalPercent,
                    eEquipmentOptionStat.CriticalDamagePercent,
                    eEquipmentOptionStat.Speed,
                    eEquipmentOptionStat.EffectHitPercent,
                    eEquipmentOptionStat.EffectResistancePercent,
});
            }
            else if (data is HelmetData)
            {
                optionStats.AddRange(new eEquipmentOptionStat[]
{
                    eEquipmentOptionStat.AttackPoint,
                    eEquipmentOptionStat.AttackPercent,
                    eEquipmentOptionStat.HealthPercent,
                    eEquipmentOptionStat.DefencePoint,
                    eEquipmentOptionStat.DefencePercent,
                    eEquipmentOptionStat.CriticalPercent,
                    eEquipmentOptionStat.CriticalDamagePercent,
                    eEquipmentOptionStat.Speed,
                    eEquipmentOptionStat.EffectHitPercent,
                    eEquipmentOptionStat.EffectResistancePercent,
});
            }
            else if (data is ShoeData)
            {
                optionStats.AddRange(new eEquipmentOptionStat[]
{
                    eEquipmentOptionStat.AttackPoint,
                    eEquipmentOptionStat.AttackPercent,
                    eEquipmentOptionStat.HealthPoint,
                    eEquipmentOptionStat.HealthPercent,
                    eEquipmentOptionStat.DefencePoint,
                    eEquipmentOptionStat.DefencePercent,
                    eEquipmentOptionStat.CriticalPercent,
                    eEquipmentOptionStat.CriticalDamagePercent,
                    eEquipmentOptionStat.EffectHitPercent,
                    eEquipmentOptionStat.EffectResistancePercent,
});
            }
            else if (data is AmuletData)
            {
                optionStats.AddRange(new eEquipmentOptionStat[]
{
                    eEquipmentOptionStat.AttackPoint,
                    eEquipmentOptionStat.AttackPercent,
                    eEquipmentOptionStat.HealthPoint,
                    eEquipmentOptionStat.HealthPercent,
                    eEquipmentOptionStat.DefencePoint,
                    eEquipmentOptionStat.DefencePercent,
                    eEquipmentOptionStat.Speed,
                    eEquipmentOptionStat.EffectHitPercent,
                    eEquipmentOptionStat.EffectResistancePercent,
});
            }
            else if (data is RingData)
            {
                optionStats.AddRange(new eEquipmentOptionStat[]
{
                    eEquipmentOptionStat.AttackPoint,
                    eEquipmentOptionStat.AttackPercent,
                    eEquipmentOptionStat.HealthPoint,
                    eEquipmentOptionStat.HealthPercent,
                    eEquipmentOptionStat.DefencePoint,
                    eEquipmentOptionStat.DefencePercent,
                    eEquipmentOptionStat.CriticalPercent,
                    eEquipmentOptionStat.CriticalDamagePercent,
                    eEquipmentOptionStat.Speed,
});
            }

            // ������ �ɼ� ������ �������� �ʵ��� �Ѵ�.
            optionStats.Remove(optionStats.Find(item => item == data.optionStat_1_Type));
            optionStats.Remove(optionStats.Find(item => item == data.optionStat_2_Type));
            optionStats.Remove(optionStats.Find(item => item == data.optionStat_3_Type));

            // ���� ���� �ɼ� ���� Ÿ�� �迭�� ����
            return optionStats.ToArray();
        }



        // �ɼ� ������ �����ش�.
        private void AddOption(ref eEquipmentOptionStat optionStat, ref float optionValue, eEquipmentOptionStat[] options, eGradeType itemGrade)
        {
            // ���ü� �ִ� �ɼ� ���� Ÿ���� �ϳ��� �����ϰ� �̴´�.
            optionStat = options[Random.Range(0, options.Length)];
            // ��޿� �´� ���������� ����Ÿ �����⸦ �����Ѵ�.
            EquipmentCreateData creator = null;
            switch (itemGrade)
            {
                case eGradeType.Normal:
                    creator = this.normalCreateData;
                    break;
                case eGradeType.Rare:
                    creator = this.rareCreateData;
                    break;
                case eGradeType.Unique:
                    creator = this.uniqueCreateData;
                    break;
                case eGradeType.Legendary:
                    creator = this.legendaryCreateData;
                    break;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }

            // �ɼ� ������ �ο��Ѵ�.
            switch (optionStat)
            {
                case eEquipmentOptionStat.AttackPoint:
                    optionValue = Mathf.Floor(Random.Range(creator.optionAttackPoint.min, creator.optionAttackPoint.max));
                    break;
                case eEquipmentOptionStat.AttackPercent:
                    optionValue = Random.Range(creator.optionAttackPercent.min, creator.optionAttackPercent.max);
                    break;
                case eEquipmentOptionStat.HealthPoint:
                    optionValue = Mathf.Floor(Random.Range(creator.optionHealthPoint.min, creator.optionHealthPoint.max));
                    break;
                case eEquipmentOptionStat.HealthPercent:
                    optionValue = Random.Range(creator.optionHealthPercent.min, creator.optionHealthPercent.max);
                    break;
                case eEquipmentOptionStat.DefencePoint:
                    optionValue = Mathf.Floor(Random.Range(creator.optionDefencePoint.min, creator.optionDefencePoint.max));
                    break;
                case eEquipmentOptionStat.DefencePercent:
                    optionValue = Random.Range(creator.optionDefencePercent.min, creator.optionDefencePercent.max);
                    break;
                case eEquipmentOptionStat.CriticalPercent:
                    optionValue = Random.Range(creator.optionCriticalPercent.min, creator.optionCriticalPercent.max);
                    break;
                case eEquipmentOptionStat.CriticalDamagePercent:
                    optionValue = Random.Range(creator.optionCriticalDamage.min, creator.optionCriticalDamage.max);
                    break;
                case eEquipmentOptionStat.Speed:
                    optionValue = Mathf.Floor(Random.Range(creator.optionSpeed.min, creator.optionSpeed.max));
                    break;
                case eEquipmentOptionStat.EffectHitPercent:
                    optionValue = Random.Range(creator.optionEffectHit.min, creator.optionEffectHit.max);
                    break;
                case eEquipmentOptionStat.EffectResistancePercent:
                    optionValue = Random.Range(creator.optionEffectRes.min, creator.optionEffectRes.max);
                    break;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }
        }

        // �ۼ�Ʈ�� ǥ���� �� �Ҽ��� ���ֱ�
        private void SetNewProperty(ref float value, float min, float max)
        {
            value = Mathf.Floor(Random.Range(min, max) * 100f) / 100f;
        }

        // �Ϲ� ���� ǥ���� �� �Ҽ��� ���ֱ�
        private void SetNewPropertyRound(ref float value, float min, float max)
        {
            value = Mathf.Round(Random.Range(min, max));
        }
    }
}