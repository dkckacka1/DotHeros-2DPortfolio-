namespace Portfolio.skill
{
    public class ActiveSkillData : SkillData
    {
        public ActiveSkillType activeSkillType;
        public bool isAutoTarget;

        public int skillCoolTime;
        public int consumeManaValue;

        public bool isAllyTarget;
        public bool isEnemyTarget;
        public bool isFrontTarget;
        public bool isRearTarget;
        public int targetNum;

        public AutoPeerTargetType autoPeerTargetType;
        public AutoProcessionTargetType autoProcessionTargetType;
    }
}
