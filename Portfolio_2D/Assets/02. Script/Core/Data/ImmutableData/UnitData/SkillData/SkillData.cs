namespace Portfolio.skill
{
    public abstract class SkillData : Data
    {
        public string skillClassName;
        public string skillName;
        public string skillDesc;
        public float skillLevelValue_1; // ��ų ������ ���ϰ� �� ����
        public float skillLevelValue_2; // ��ų ������ ���ϰ� �� ����
        public float skillLevelValue_3; // ��ų ������ ���ϰ� �� ����
        public string skillIconSpriteName;

        public SkillType skillType;

        public int conditinID_1;
        public int conditinID_2;
        public int conditinID_3;
    }
}