/*
 * 모든 Enum을 모아놓은 클래스
 */

namespace Portfolio
{
    // 전투 상태
    public enum BattleState
    { 
        NONE = -1,
        PLAY,           // 전투중
        PAUSE,          // 멈춤
        SETSTAGE,       // 스테이지 세팅중
        BATTLESTART,    // 전투 시작
        WIN,            // 승리
        DEFEAT,         // 패배
    }

    // 현재 턴 상태
    public enum TurnType
    {
        NONE = -1,
        WAITUNITTURN, // 턴 대기중
        PLAYER,       // 플레이어 턴
        ENEMY,        // 적 턴
    }

    // 열 타입
    public enum LineType
    {
        FrontLine,  // 전열
        RearLine    // 후열
    }

    // 속성 타입
    public enum ElementalType
    {
        Fire,       // 불
        Water,      // 물
        Ground,     // 땅
        Light,      // 빛
        Dark        // 어둠
    }

    // 장비 타입
    public enum EquipmentItemType
    {
        Weapon, // 무기
        Helmet, // 헬멧
        Armor,  // 갑옷
        Amulet, // 목걸이
        Ring,   // 반지
        Shoe    // 신발
    }

    // 세트 타입
    public enum SetType
    {
        Critical,       // 치명
        Hit,            // 적중
        Speed,          // 속도
        Attack,         // 공격
        Defence,        // 방어
        Health,         // 체력
        Resistance,     // 저항
        Destruction,    // 파멸
        Count
    }

    // 등급 타입
    public enum GradeType
    {
        Normal,     // 평범
        Rare,       // 희귀
        Unique,     // 고유
        Legendary   // 전설
    }

    // 옵션 스탯 종류
    public enum EquipmentOptionStat
    {
        NONE = -1,
        AttackPoint,                // 공격력
        AttackPercent,              // 공격력(%)
        HealthPoint,                // 생명력
        HealthPercent,              // 생명력(%)
        DefencePoint,               // 방어력
        DefencePercent,             // 방어력(%)
        CriticalPercent,            // 치명타 확률
        CriticalDamagePercent,      // 치명타 공격력
        Speed,                      // 속도
        EffectHitPercent,           // 효과 적중률
        EffectResistancePercent,    // 효과 저항력
    }

    // 스킬 타입
    public enum SkillType
    {
        ActiveSkill, // 액티브 스킬
        PassiveSkill // 패시브 스킬
    }

    // 유닛의 스킬 종류
    public enum UnitSkillType
    {
        BaseAttack,         // 기본 공격 스킬
        ActiveSkill_1,      // 액티브 스킬 1
        ActiveSkill_2,      // 액티브 스킬 2
        PassiveSkill_1,     // 패시브 스킬 1
        PassiveSkill_2,     // 패시브 스킬 2
    }

    // 액티브 스킬의 종류
    public enum ActiveSkillType
    {
        NONE = -1,
        BasicAttack,    // 기본 공격
        Firstpriority,  // 최우선
        SingleAttack,   // 단일 공격
        MultipleAttack, // 광역 공격
        SingleHeal,     // 단일 힐
        MultipleHeal    // 광역 힐
    }

    // 상태이상 종류
    public enum ConditionType
    {
        Continuation = 0, // 지속형
        Tick    // 틱형
    }

    // 결제 수단 종류
    public enum PaymentType
    {
        Cash,
        Diamond,
        Gold
    }
}