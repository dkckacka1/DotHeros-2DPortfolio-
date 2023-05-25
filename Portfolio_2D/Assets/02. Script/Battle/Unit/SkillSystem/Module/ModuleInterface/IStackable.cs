namespace Portfolio.skill
{
    public interface IStackable
    {
        public int StackCount { get; }
        public bool IsStackBuff { get; }
        public bool IsStackOverlap { get; }
        public bool IsStackSum { get; }
    }
}
