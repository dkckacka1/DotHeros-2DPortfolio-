/*
 * �����̻� ������ Ŭ����
 */

namespace Portfolio.condition
{
    public class ConditionData : Data
    {
        public string conditionName;                // �����̻� �̸�
        public string conditionDesc;                // �����̻� ����
        public string conditionClassName;           // �����̻� Ŭ���� �̸�
        public string conditionIconSpriteName;      // �����̻� �̹��� ��������Ʈ �̸�

        public eConditionType conditionType;         // �����̻� Ÿ��

        public bool isBuff;                         // �����̻� �����ΰ�
        public bool isOverlaping;                   // �����̻� ��ø����
        public bool isResetCount;                   // �����̻� �ʱ�ȭ ����

    }
}