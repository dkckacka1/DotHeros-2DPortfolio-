using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��� ������ ������ Ŭ����
 * ������ ���ʷ����Ͱ� �Ʒ� ��ġ�� Ȯ���Ͽ� �������� ���� �������ش�.
 */

namespace Portfolio
{
    [CreateAssetMenu(fileName = "newCreateEquipmentItemData", menuName = "newScritableOBJ/EquipmentItemData", order =0)]
    public class EquipmentCreateData : ScriptableObject
    {
        [System.Serializable]
        // �ִ� �ּ� ��ġ ����
        public struct MINMAXValues
        {
            public float min;
            public float max;
        }

        // ������ �������� ���
        public eGradeType createGrade;

        // ��� �⺻ �ɼ� ��ġ
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


        // �ɼ� ������ �����ɶ��� ��ġ
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