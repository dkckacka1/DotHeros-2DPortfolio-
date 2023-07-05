/*
 * 스킬 데이터 클래스
 */

namespace Portfolio.skill
{
    public abstract class SkillData : Data
    {
        public string skillClassName;       // 스킬 클래스 이름
        public string skillName;            // 스킬 이름
        public string skillDesc;            // 스킬 설명
        public float skillLevelValue_1;     // 스킬 레벨당 변하게 될 변수
        public float skillLevelValue_2;     // 스킬 레벨당 변하게 될 변수
        public float skillLevelValue_3;     // 스킬 레벨당 변하게 될 변수
        public string skillIconSpriteName;  // 스킬 이미지 스프라이트 이름

        public SkillType skillType;         // 스킬 타입

        public int conditinID_1;            // 스킬이 가지고 있는 상태이상 ID
        public int conditinID_2;            // 스킬이 가지고 있는 상태이상 ID
        public int conditinID_3;            // 스킬이 가지고 있는 상태이상 ID
    }
}