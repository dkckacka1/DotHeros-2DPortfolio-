namespace Portfolio.skill
{
    public class ActiveSkillData : SkillData
    {
        public bool isAutoTarget;

        public int skillCoolTime;
        public int consumeManaValue;

        public bool isPlayerTarget;
        public bool isEnemyTarget;
        public bool isFrontTarget;
        public bool isRearTarget;
        public int targetNum;

        public AutoPeerTargetType autoPeerTargetType;
        public AutoProcessionTargetType autoProcessionTargetType;

        public string moduleName_1;
        public string moduleName_2;
        public string moduleName_3;
    }
}
