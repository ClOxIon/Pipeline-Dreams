namespace PipelineDreams
{
    public class BuffWithDuration : Buff {
        public float TimeLeft { get; private set; }
        protected float initialTime;
        public BuffWithDuration(Entity subject, BuffData buffData, TaskManager tm) : base(subject, buffData, tm) {
            initialTime = CM.Clock;
        }
        protected override void EffectByTime(float Time) {
            TimeLeft = initialTime + BuData.baseDuration - Time;
            if (TimeLeft < 0)
                Destroy();
        }
    }
}