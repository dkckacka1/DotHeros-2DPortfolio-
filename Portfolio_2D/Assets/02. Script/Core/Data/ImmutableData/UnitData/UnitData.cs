/*
 * 유닛 데이터 클래스
 */

namespace Portfolio
{
    public class UnitData : Data
    {
        // DefualtProperty
        public string unitName;                 // 유닛 이름
        public eElementalType elementalType;     // 유닛 속성
        public bool isUserUnit = true;          // 유닛이 유저가 사용가능한지
        public int defaultGrade = 1;            // 유닛 기본 등급

        // UnitAttribute
        public float maxHP = 100;               // 유닛 생명력
        public float attackPoint = 10f;         // 유닛 공격력
        public float defencePoint = 0f;         // 유닛 방어력
        public float speed = 100f;              // 유닛 속도
        public float criticalPercent = 0f;      // 유닛 치명타 확률
        public float criticalDamage = 0f;       // 유닛 치명타 공격력
        public float effectHit = 0f;            // 유닛 효과 적중
        public float effectResistance = 0f;     // 유닛 효과 저항렬
        public float levelValue = 0.2f;         // 레벨당 스텟 증가 수치
        public float gradeValue = 0.5f;         // 등급당 스텟 증가 수치
                                                // 
        // Skill
        public int basicAttackSKillID = 10000;  // 기본 공격 스킬 ID
        public int activeSkillID_1 = -1;        // 액티브 스킬 1 ID
        public int activeSkillID_2 = -1;        // 액티브 스킬 2 ID
        public int passiveSkillID_1 = -1;       // 패시브 스킬 1 ID
        public int passiveSkillID_2 = -1;       // 패시브 스킬 2 ID

        // Apparence
        public string portraitImageName;        // 유닛 포트레잇 이미지
        public string animationName;            // 유닛 애니메이션 컨트롤러 이름
    }

}