/*
 * 액티스 스킬 데이터 클래스
 */

namespace Portfolio.skill
{
    public class ActiveSkillData : SkillData
    {
        public ActiveSkillType activeSkillType; // 액티브 스킬 타입
        public int skillCoolTime;               // 스킬 쿨타임
        public int consumeManaValue;            // 소비 마나량
        public int targetNum;                   // 스킬 대상 갯수
    }
}
