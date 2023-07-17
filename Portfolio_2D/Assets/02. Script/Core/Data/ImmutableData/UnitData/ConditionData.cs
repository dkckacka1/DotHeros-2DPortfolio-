/*
 * 상태이상 데이터 클래스
 */

namespace Portfolio.condition
{
    public class ConditionData : Data
    {
        public string conditionName;                // 상태이상 이름
        public string conditionDesc;                // 상태이상 설명
        public string conditionClassName;           // 상태이상 클래스 이름
        public string conditionIconSpriteName;      // 상태이상 이미지 스프라이트 이름

        public eConditionType conditionType;         // 상태이상 타입

        public bool isBuff;                         // 상태이상 버프인가
        public bool isOverlaping;                   // 싱태이상 중첩가능
        public bool isResetCount;                   // 상태이상 초기화 가능

    }
}