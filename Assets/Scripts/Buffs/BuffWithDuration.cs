using UnityEngine;

namespace PipelineDreams
{
    public class BuffWithDuration : Buff {
        public float TimeLeft { get; private set; }
        protected float initialTime;
        protected float duration;
        public override void Init(PDData data, params object[] args)
        {
            base.Init(data, args);
            initialTime = CM.Clock;
            if ((args[0] as float?).HasValue)
                this.duration = (args[0] as float?).Value;
            else
                Debug.LogError("BuffWithDuration inflicted without float duration at the 0th argument: " + Data.Name);

        }
        public override void ReInflict(params object[] args)
        {
            base.ReInflict(args);
            var dur = args[0] as float?;
            if (!dur.HasValue)
                Debug.LogError("BuffWithDuration Reinflicted without float duration at the 0th argument: "+Data.Name);
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
            {
                SetEnabled(false);
                Remove();
            }
        }
    }
}