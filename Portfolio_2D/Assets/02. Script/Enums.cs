namespace Portfolio
{
    public enum BattleState
    { 
        NONE = -1,
        WATTINGTURN,
        PLAYERTURN,
        ENEMYTURN,
        PAUSE,
        SETTING,
        COUNT
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

}