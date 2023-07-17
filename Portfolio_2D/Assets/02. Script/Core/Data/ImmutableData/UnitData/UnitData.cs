/*
 * ���� ������ Ŭ����
 */

namespace Portfolio
{
    public class UnitData : Data
    {
        // DefualtProperty
        public string unitName;                 // ���� �̸�
        public eElementalType elementalType;     // ���� �Ӽ�
        public bool isUserUnit = true;          // ������ ������ ��밡������
        public int defaultGrade = 1;            // ���� �⺻ ���

        // UnitAttribute
        public float maxHP = 100;               // ���� �����
        public float attackPoint = 10f;         // ���� ���ݷ�
        public float defencePoint = 0f;         // ���� ����
        public float speed = 100f;              // ���� �ӵ�
        public float criticalPercent = 0f;      // ���� ġ��Ÿ Ȯ��
        public float criticalDamage = 0f;       // ���� ġ��Ÿ ���ݷ�
        public float effectHit = 0f;            // ���� ȿ�� ����
        public float effectResistance = 0f;     // ���� ȿ�� ���׷�
        public float levelValue = 0.2f;         // ������ ���� ���� ��ġ
        public float gradeValue = 0.5f;         // ��޴� ���� ���� ��ġ
                                                // 
        // Skill
        public int basicAttackSKillID = 10000;  // �⺻ ���� ��ų ID
        public int activeSkillID_1 = -1;        // ��Ƽ�� ��ų 1 ID
        public int activeSkillID_2 = -1;        // ��Ƽ�� ��ų 2 ID
        public int passiveSkillID_1 = -1;       // �нú� ��ų 1 ID
        public int passiveSkillID_2 = -1;       // �нú� ��ų 2 ID

        // Apparence
        public string portraitImageName;        // ���� ��Ʈ���� �̹���
        public string animationName;            // ���� �ִϸ��̼� ��Ʈ�ѷ� �̸�
    }

}