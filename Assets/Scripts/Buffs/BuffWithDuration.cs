using UnityEngine;

namespace PipelineDreams
{
    public class BuffWithDuration : Buff {
        public float TimeLeft { get; private set; }
        protected float initialTime;
        protected float duration;
        public BuffWithDuration(Entity subject, BuffData buffData, TaskManager tm) : base(subject, buffData, tm) {
            initialTime = CM.Clock;
            duration = BuData.baseDuration;
        }
        public BuffWithDuration(Entity subject, BuffData buffData, TaskManager tm, float duration) : base(subject, buffData, tm)
        {
            initialTime = CM.Clock;
            this.duration = duration;
        }
        public override void ReInflict(params object[] args)
        {
            base.ReInflict(args);
            var dur = args[0] as float?;
            if (!dur.HasValue)
                Debug.LogError("BuffWithDuration Reinflicted without float duration at the 0th argument: "+BuData.Name);
            else
            if (TimeLeft < dur)
            {
                initialTime = CM.Clock;
                duration = dur.Value;
            }
        }
        /// <summary>
        /// Called by taskmanager event
        /// </summary>
        /// <param name="Time"></param>
        protected override void EffectByTime(float Time) {
            TimeLeft = initialTime + duration - Time;
            if (TimeLeft < 0)
                Destroy();
        }
    }
}