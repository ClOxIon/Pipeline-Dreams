using UnityEngine;
using System.Collections;

namespace PipelineDreams
{
    public class BuffShield : Buff
    {
        public float TimeLeft { get; private set; }
        protected float lossPerTurn;
        int shield;
        float realShield;
        float lastclock;
        public BuffShield(Entity subject, BuffData buffData, TaskManager tm, params object[] args) : base(subject, buffData, tm)
        {
            if((args[0] as int?).HasValue)
            shield = (args[0] as int?).Value;
            realShield = shield;
            lossPerTurn = BuData.FindParameterFloat("LossPerTurn");
            lastclock = CM.Clock;
        }

        public override void ReInflict(params object[] args)
        {
            EffectByTime(CM.Clock);
            if ((args[0] as int?).HasValue)
                shield = (args[0] as int?).Value;
            realShield = shield;
        }
        protected override void EffectByTime(float Time)
        {
            realShield -= lossPerTurn * (Time - lastclock);
            shield = (int)realShield;
            lastclock = CM.Clock;
            if (realShield <= 0)
                Destroy();
        }
    }
}
