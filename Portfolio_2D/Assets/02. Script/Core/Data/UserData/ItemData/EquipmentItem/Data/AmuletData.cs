/*
 * 목걸이 데이터 클래스
 */

namespace Portfolio
{
    [System.Serializable]
    public class AmuletData : EquipmentItemData
    {
        public float criticalPercent;   // 장비 치명타 확률
        public float criticalDamage;    // 장비 치명타 공격력
    }
}