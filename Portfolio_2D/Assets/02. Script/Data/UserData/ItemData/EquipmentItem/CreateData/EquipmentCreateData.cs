using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    [CreateAssetMenu(fileName = "newCreateEquipmentItemData", menuName = "newScritableOBJ/EquipmentItemData", order =0)]
    public class EquipmentCreateData : ScriptableObject
    {
        public ItemGrade createGrade;

        [Header("WeapoonData")]
        public float minWeaponAttackPoint;
        public float maxWeaponAttackPoint;
        [Header("HelmetData")]
        public float minHelmetHealthPoint;
        public float maxHelmetHealthPoint;
        [Header("ArmorData")]
        public float minArmorDefencePoint;
        public float maxArmorDefencePoint;
        [Header("AmuletData")]
        [Range(0, 0.8f)] public float minAmuletCriticalPercent;
        [Range(0, 0.8f)] public float maxAmuletCriticalPercent;
        [Range(0, 0.8f)] public float minAmuletCriticalDamage;
        [Range(0, 0.8f)] public float maxAmuletCriticalDamage;
        [Header("RingData")]
        [Range(0, 0.8f)] public float minRingEffectHit;
        [Range(0, 0.8f)] public float maxRingEffectHit;
        [Range(0, 0.8f)] public float minRingEffectRes;
        [Range(0, 0.8f)] public float maxRingEffectRes;
        [Header("ShoeData")]
        [Range(0, 100f)] public float minShoeSpeed;
        [Range(0, 100f)] public float maxShoeSpeed;


        [Header("OptionStat")]
        public float minOptionAttackPoint;
        public float maxOptionAttackPoint;
        [Range(0, 0.8f)] public float minOptionAttackPercent;
        [Range(0, 0.8f)] public float maxOptionAttackPercent;
        public float minOptionHealthPoint;
        public float maxOptionHealthPoint;
        [Range(0, 0.8f)] public float minOptionHealthPercent;
        [Range(0, 0.8f)] public float maxOptionHealthPercent;
        public float minOptionDefencePoint;
        public float maxOptionDefencePoint;
        [Range(0, 0.8f)] public float minOptionDefencePercent;
        [Range(0, 0.8f)] public float maxOptionDefencePercent;
        [Range(0, 0.8f)] public float minOptionCriticalPercent;
        [Range(0, 0.8f)] public float maxOptionCriticalPercent;
        [Range(0, 0.8f)] public float minOptionCriticalDamage;
        [Range(0, 0.8f)] public float maxOptionCiritcalDamage;
        public float minOptionSpeed;
        public float maxOptionSpeed;
        [Range(0, 0.8f)] public float minOptionEffectHit;
        [Range(0, 0.8f)] public float maxOptionEffectHit;
        [Range(0, 0.8f)] public float minOptionEffectRes;
        [Range(0, 0.8f)] public float maxOptionEffectRes;
    }
}