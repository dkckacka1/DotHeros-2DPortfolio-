/*
 * 반지 데이터 클래스
 */

namespace Portfolio
{
    [System.Serializable]
    public class RingData : EquipmentItemData
    {
        public float effectHit;         // 장비 효과 적중
        public float effectResistance;  // 장비 효과 저항력
    }
}