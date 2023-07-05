/*
 * ��ų ������ Ŭ����
 */

namespace Portfolio.skill
{
    public abstract class SkillData : Data
    {
        public string skillClassName;       // ��ų Ŭ���� �̸�
        public string skillName;            // ��ų �̸�
        public string skillDesc;            // ��ų ����
        public float skillLevelValue_1;     // ��ų ������ ���ϰ� �� ����
        public float skillLevelValue_2;     // ��ų ������ ���ϰ� �� ����
        public float skillLevelValue_3;     // ��ų ������ ���ϰ� �� ����
        public string skillIconSpriteName;  // ��ų �̹��� ��������Ʈ �̸�

        public SkillType skillType;         // ��ų Ÿ��

        public int conditinID_1;            // ��ų�� ������ �ִ� �����̻� ID
        public int conditinID_2;            // ��ų�� ������ �ִ� �����̻� ID
        public int conditinID_3;            // ��ų�� ������ �ִ� �����̻� ID
    }
}