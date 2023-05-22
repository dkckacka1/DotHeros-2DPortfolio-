namespace Portfolio
{
    public enum BattleState
    { 
        NONE = -1,
        PLAY,
        PAUSE,
        SETTING,
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

    public enum UnitType
    {
        Player,
        Enemy
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

    public enum EquipmentOptionStat
    {
        NONE = -1,
        AttackPercent,
        AttackPoint,
        DefencePercent,
        DefencePoint,
        CriticalPercent,
        CriticalDamagePercent,
        Speed,
        EffectHitPercent,
        EffectResistancePercent,
        HealthPercent,
        HealthPoint,

    }
}