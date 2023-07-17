
/*
 * ��� ������ �⺻ Ŭ����
 */


namespace Portfolio
{
    [System.Serializable]
    public class EquipmentItemData : ItemData
    {
        public eGradeType equipmentGrade = eGradeType.Normal;                         // ������ ���
        public eElementalType equipmentElementType;                                  // ������ �Ӽ�
        public eEquipmentItemType equipmentType;                                     // �ƾ��� Ÿ��
        public eSetType setType;                                                     // ��Ʈ Ÿ��

        public int reinforceCount = 0;                                              // ��� ��ȭ ��ġ
        public eEquipmentOptionStat optionStat_1_Type = eEquipmentOptionStat.NONE;    // �ɼ� ���� 1 Ÿ��
        public float optionStat_1_value = 0;                                        // �ɼ� ���� 1 ��
        public eEquipmentOptionStat optionStat_2_Type = eEquipmentOptionStat.NONE;    // �ɼ� ���� 2 Ÿ��
        public float optionStat_2_value = 0;                                        // �ɼ� ���� 2 ��
        public eEquipmentOptionStat optionStat_3_Type = eEquipmentOptionStat.NONE;    // �ɼ� ���� 3 Ÿ��
        public float optionStat_3_value = 0;                                        // �ɼ� ���� 3 ��
        public eEquipmentOptionStat optionStat_4_Type = eEquipmentOptionStat.NONE;    // �ɼ� ���� 4 Ÿ��
        public float optionStat_4_value = 0;                                        // �ɼ� ���� 4 ��
    }
}