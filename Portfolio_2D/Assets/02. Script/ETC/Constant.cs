namespace Portfolio
{
    public static class Constant
    {
        //===========================================================
        // GameValue
        //===========================================================
        public const float MAX_REINFORCE_COUNT = 15;
        public static readonly int[] reinforceConsumeGoldValues = { 100, 500, 1000, 2000, 4000, 10000, 15000, 30000,75000,100000,150000,300000,750000,1500000,2000000 };

        //===========================================================
        // RandomValue
        //===========================================================
        public const float normalSummonPercent = 0.70f;
        public const float rareSummonPercent = 0.25f;
        public const float uniqueSummonPercent = 0.05f;
        public static readonly float[] reinforceProbabilitys = { 1f, 0.95f, 0.9f, 0.85f, 0.8f, 0.78f, 0.75f, 0.73f, 0.71f, 0.7f, 0.69f, 0.67f, 0.65f, 0.63f, 0.6f};

        //===========================================================
        // BattleValue
        //===========================================================
        public const int MAX_MANA_COUNT = 4;

        //===========================================================
        // AIValue
        //===========================================================
        public const float UNIT_DAMAGED_PERCENT = 0.8f;

        //===========================================================
        // Path
        //===========================================================
        public const string UserSLName = @"User";

        public const string dataTablePath = @"\05. DataTable\";
        public const string resorucesDataPath = @"\Resources\Data\";
        public const string skillDataTableName = @"SkillDataTable";
        public const string activeSkillJsonName = @"ActiveSkillData";
        public const string passiveSkillJsonName = @"PassiveSkillData";
        public const string unitDataTableName = @"UnitDataTable";
        public const string unitDataJsonName = @"UnitData";
        public const string conditionDataTableName = @"ConditionDataTable";
        public const string conditionDataJsonName = @"ConditionData";
        public const string mapDataTableName = @"MapDataTable";
        public const string mapDataJsonName = @"MapData";
        public const string stageDataJsonName = @"StageData";
        
    }
}
