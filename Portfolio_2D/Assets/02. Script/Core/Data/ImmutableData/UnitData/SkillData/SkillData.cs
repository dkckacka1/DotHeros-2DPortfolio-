namespace Portfolio.skill
{
    public abstract class SkillData : Data
    {
        public string skillClassName;
        public string skillName;
        public string skillDesc;
        public float skillLevelValue_1; // 스킬 레벨당 변하게 될 변수
        public float skillLevelValue_2; // 스킬 레벨당 변하게 될 변수
        public float skillLevelValue_3; // 스킬 레벨당 변하게 될 변수
        public string skillIconSpriteName;

        public SkillType skillType;

        public int conditinID_1;
        public int conditinID_2;
        public int conditinID_3;
    }
}