using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class ItemCreator : MonoBehaviour
    {
        [SerializeField] EquipmentCreateData normalCreateData;
        [SerializeField] EquipmentCreateData rareCreateData;
        [SerializeField] EquipmentCreateData uniqueCreateData;
        [SerializeField] EquipmentCreateData legendaryCreateData;

        public T CreateEquipmentItemData<T>(ItemGrade itemGrade) where T : EquipmentItemData, new()
        {
            T newData = new T();
            EquipmentCreateData creator = null;

            newData.equipmentGrade = itemGrade;

            switch (itemGrade)
            {
                case ItemGrade.Normal:
                    creator = this.normalCreateData;
                    break;
                case ItemGrade.Rare:
                    creator = this.rareCreateData;
                    break;
                case ItemGrade.Unique:
                    creator = this.uniqueCreateData;
                    break;
                case ItemGrade.Legendary:
                    creator = this.legendaryCreateData;
                    break;
                default:
                    Debug.LogWarning("CreateItem Error #1");
                    return null;
            }

            if (newData is WeaponData)
            {
                SetNewPropertyRound(ref (newData as WeaponData).attackPoint, creator.minWeaponAttackPoint, creator.maxWeaponAttackPoint);
                newData.equipmentType = EquipmentItemType.Weapon;
            }
            else if (newData is HelmetData)
            {
                SetNewPropertyRound(ref (newData as HelmetData).healthPoint, creator.minHelmetHealthPoint, creator.maxHelmetHealthPoint);
                newData.equipmentType = EquipmentItemType.Helmet;
            }
            else if(newData is ArmorData)
            {
                SetNewPropertyRound(ref (newData as ArmorData).defencePoint, creator.minArmorDefencePoint, creator.maxArmorDefencePoint);
                newData.equipmentType = EquipmentItemType.Armor;
            }
            else if(newData is ShoeData)
            {
                SetNewPropertyRound(ref (newData as ShoeData).speed, creator.minShoeSpeed, creator.maxShoeSpeed);
                newData.equipmentType = EquipmentItemType.Shoe;
            }
            else if(newData is AmuletData)
            {
                SetNewProperty(ref (newData as AmuletData).criticalPercent, creator.minAmuletCriticalPercent, creator.maxAmuletCriticalPercent);
                SetNewProperty(ref (newData as AmuletData).criticalDamage, creator.minAmuletCriticalDamage, creator.maxAmuletCriticalDamage);
                newData.equipmentType = EquipmentItemType.Amulet;
            }
            else if(newData is RingData)
            {
                SetNewProperty(ref (newData as RingData).effectHit, creator.minRingEffectHit, creator.maxRingEffectHit);
                SetNewProperty(ref (newData as RingData).effectResistance, creator.minRingEffectRes, creator.maxRingEffectRes);
                newData.equipmentType = EquipmentItemType.Ring;
            }
            else
            {
                Debug.LogWarning("CreateItem Error #2");
                return null;
            }

            return newData;
        }

        // TODO 아이템에 옵션 붙여주는 메서드 만들어야함

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