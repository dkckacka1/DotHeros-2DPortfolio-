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
}