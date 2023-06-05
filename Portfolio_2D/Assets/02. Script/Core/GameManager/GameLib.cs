using UnityEngine;

namespace Portfolio
{
    public static class GameLib
    {
        public static string GetOptionStatusText(EquipmentOptionStat optionStat)
        {
            switch (optionStat)
            {
                case EquipmentOptionStat.AttackPoint:
                    return "공격력";
                case EquipmentOptionStat.AttackPercent:
                    return "공격력(%)";
                case EquipmentOptionStat.HealthPoint:
                    return "생명력";
                case EquipmentOptionStat.HealthPercent:
                    return "생명력(%)";
                case EquipmentOptionStat.DefencePoint:
                    return "방어력";
                case EquipmentOptionStat.DefencePercent:
                    return "방어력(%)";
                case EquipmentOptionStat.CriticalPercent:
                    return "치명타 적중";
                case EquipmentOptionStat.CriticalDamagePercent:
                    return "치명타 피해";
                case EquipmentOptionStat.Speed:
                    return "속도";
                case EquipmentOptionStat.EffectHitPercent:
                    return "효과 적중";
                case EquipmentOptionStat.EffectResistancePercent:
                    return "효과 저항";
            }

            return "없음";
        }

        public static string GetEquipmentTypeText(EquipmentItemType type)
        {
            switch (type)
            {
                case EquipmentItemType.Weapon:
                    return "무기";
                case EquipmentItemType.Helmet:
                    return "투구";
                case EquipmentItemType.Armor:
                    return "갑옷";
                case EquipmentItemType.Amulet:
                    return "목걸이";
                case EquipmentItemType.Ring:
                    return "반지";
                case EquipmentItemType.Shoe:
                    return "신발";
            }

            return "없음";
        }

        public static string GetGradeTypeText(GradeType type)
        {
            switch (type)
            {
                case GradeType.Normal:
                    return "일반";
                case GradeType.Rare:
                    return "희귀";
                case GradeType.Unique:
                    return "고유";
                case GradeType.Legendary:
                    return "전설";
            }

            return "없음";
        }

        public static string GetSetTypeText(SetType type)
        {
            switch (type)
            {
                case SetType.Critical:
                    return "치명";
                case SetType.Hit:
                    return "적중";
                case SetType.Speed:
                    return "속도";
                case SetType.Attack:
                    return "공격";
                case SetType.Defence:
                    return "방어";
                case SetType.Health:
                    return "체력";
                case SetType.Resistance:
                    return "저항";
                case SetType.Destruction:
                    return "파멸";
            }

            return "없음";
        }

        /// <summary>
        /// 확률 계산 true면 성공, false면 실패
        /// </summary>
        /// <param name="value">확률</param>
        /// <param name="seed">들어온 값</param>
        /// <returns></returns>
        public static bool ProbabilityCalculation(float value)
        {
            return value < Random.Range(0, 100);
        }

        
    }
}