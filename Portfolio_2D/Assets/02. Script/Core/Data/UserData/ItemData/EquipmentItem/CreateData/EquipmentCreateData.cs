using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 장비 아이템 생성기 클래스
 * 아이템 제너레이터가 아래 수치를 확인하여 아이템을 랜덤 생성해준다.
 */

namespace Portfolio
{
    [CreateAssetMenu(fileName = "newCreateEquipmentItemData", menuName = "newScritableOBJ/EquipmentItemData", order =0)]
    public class EquipmentCreateData : ScriptableObject
    {
        [System.Serializable]
        // 최대 최소 수치 설정
        public struct MINMAXValues
        {
            public float min;
            public float max;
        }

        // 생성할 아이템의 등급
        public eGradeType createGrade;

        // 장비별 기본 옵션 수치
        [Header("WeapoonData")]
        public MINMAXValues attackPoint;
        [Header("HelmetData")]
        public MINMAXValues healthPoint;
        [Header("ArmorData")]
        public MINMAXValues defencePoint;
        [Header("AmuletData")]
        public MINMAXValues criticalPercent;
        public MINMAXValues criticalDamage;
        [Header("RingData")]
        public MINMAXValues effectHit;
        public MINMAXValues effectRes;
        [Header("ShoeData")]
        public MINMAXValues speed;


        // 옵션 스텟이 생성될때의 수치
        [Header("OptionStat")]
        public MINMAXValues optionAttackPoint;
        public MINMAXValues optionAttackPercent;
        public MINMAXValues optionHealthPoint;
        public MINMAXValues optionHealthPercent;
        public MINMAXValues optionDefencePoint;
        public MINMAXValues optionDefencePercent;
        public MINMAXValues optionCriticalPercent;
        public MINMAXValues optionCriticalDamage;
        public MINMAXValues optionSpeed;
        public MINMAXValues optionEffectHit;
        public MINMAXValues optionEffectRes;
    }
}