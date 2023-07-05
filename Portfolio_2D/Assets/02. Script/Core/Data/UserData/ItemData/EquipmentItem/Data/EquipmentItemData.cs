
/*
 * ��� ������ �⺻ Ŭ����
 */


namespace Portfolio
{
    [System.Serializable]
    public class EquipmentItemData : ItemData
    {
        public GradeType equipmentGrade = GradeType.Normal;                         // ������ ���
        public ElementalType equipmentElementType;                                  // ������ �Ӽ�
        public EquipmentItemType equipmentType;                                     // �ƾ��� Ÿ��
        public SetType setType;                                                     // ��Ʈ Ÿ��

        public int reinforceCount = 0;                                              // ��� ��ȭ ��ġ
        public EquipmentOptionStat optionStat_1_Type = EquipmentOptionStat.NONE;    // �ɼ� ���� 1 Ÿ��
        public float optionStat_1_value = 0;                                        // �ɼ� ���� 1 ��
        public EquipmentOptionStat optionStat_2_Type = EquipmentOptionStat.NONE;    // �ɼ� ���� 2 Ÿ��
        public float optionStat_2_value = 0;                                        // �ɼ� ���� 2 ��
        public EquipmentOptionStat optionStat_3_Type = EquipmentOptionStat.NONE;    // �ɼ� ���� 3 Ÿ��
        public float optionStat_3_value = 0;                                        // �ɼ� ���� 3 ��
        public EquipmentOptionStat optionStat_4_Type = EquipmentOptionStat.NONE;    // �ɼ� ���� 4 Ÿ��
        public float optionStat_4_value = 0;                                        // �ɼ� ���� 4 ��
    }
}