
/*
 * 장비 아이템 기본 클래스
 */


namespace Portfolio
{
    [System.Serializable]
    public class EquipmentItemData : ItemData
    {
        public eGradeType equipmentGrade = eGradeType.Normal;                         // 아이템 등급
        public eElementalType equipmentElementType;                                  // 아이템 속성
        public eEquipmentItemType equipmentType;                                     // 아아템 타입
        public eSetType setType;                                                     // 세트 타입

        public int reinforceCount = 0;                                              // 장비 강화 수치
        public eEquipmentOptionStat optionStat_1_Type = eEquipmentOptionStat.NONE;    // 옵션 스탯 1 타입
        public float optionStat_1_value = 0;                                        // 옵션 스탯 1 값
        public eEquipmentOptionStat optionStat_2_Type = eEquipmentOptionStat.NONE;    // 옵션 스탯 2 타입
        public float optionStat_2_value = 0;                                        // 옵션 스탯 2 값
        public eEquipmentOptionStat optionStat_3_Type = eEquipmentOptionStat.NONE;    // 옵션 스탯 3 타입
        public float optionStat_3_value = 0;                                        // 옵션 스탯 3 값
        public eEquipmentOptionStat optionStat_4_Type = eEquipmentOptionStat.NONE;    // 옵션 스탯 4 타입
        public float optionStat_4_value = 0;                                        // 옵션 스탯 4 값
    }
}