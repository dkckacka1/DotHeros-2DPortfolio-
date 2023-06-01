namespace Portfolio
{
    public enum BattleState
    { 
        NONE = -1,
        PLAY,
        PAUSE,
        SETSTAGE,
        BATTLESTART,
        WIN,
        DEFEAT,
        COUNT
    }

    public enum TurnType
    {
        NONE = -1,
        WAITTING,
        PLAYER,
        ENEMY
    }

    public enum LineType
    {
        FrontLine,
        RearLine
    }

    public enum ElementalType
    {
        Fire,
        Water,
        Ground,
        Light,
        Dark
    }

    public enum EquipmentItemType
    {
        Weapon,
        Helmet,
        Armor,
        Amulet,
        Ring,
        Shoe
    }

    public enum SetType
    {
        Critical,
        Hit,
        Speed,
        Attack,
        Defence,
        Health,
        Resistance,
        Destruction
    }

    public enum ItemGrade
    {
        Normal,
        Rare,
        Unique,
        Legendary
    }

    public enum EquipmentOptionStat
    {
        NONE = -1,
        AttackPoint,
        AttackPercent,
        HealthPoint,
        HealthPercent,
        DefencePoint,
        DefencePercent,
        CriticalPercent,
        CriticalDamagePercent,
        Speed,
        EffectHitPercent,
        EffectResistancePercent,
    }

    public enum AutoPeerTargetType
    {
        NONE = -1,
        AllyFirst,
        EnemyFirst,
    }

    public enum AutoProcessionTargetType
    {
        NONE = -1,
        FrontLineFirst,
        RearLineFirst
    }

    public enum SkillType
    {
        ActiveSkill,
        PassiveSkill
    }

    public enum ActiveSkillType
    {
        NONE = -1,
        BasicAttack,    // 기본 공격
        Firstpriority,  // 최우선
        Singleattack,   // 단일 공격
        MultipleAttack, // 광역 공격
        SingleHeal,     // 단일 힐
        MultipleHeal    // 광역 힐
    }

    public enum ConditionType
    {
        Continuation = 0, // 지속형
        Tick    // 틱형
    }
}