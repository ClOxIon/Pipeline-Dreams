using UnityEngine;
using System.Collections;
using PipelineDreams.MutableValue;

namespace PipelineDreams
{
    public class BuffShield : Buff
    {
        public float TimeLeft { get; private set; }
        protected float lossPerTurn;
        int shield;
        float realShield;
        float lastclock;
        public override void Init(PDData data, params object[] args)
        {
            base.Init(data, args);
            if ((args[0] as int?).HasValue)
                shield = (args[0] as int?).Value;
            realShield = shield;
            lossPerTurn = Data.FindParameterFloat("LossPerTurn");
            lastclock = CM.Clock;
        }

        public override void SetEnabled(bool enabled)
        {
            var v = Holder.GetComponent<EntityHealth>();
            if (v != null)
                if (enabled)
                    v.OnDamagePacketEvaluation += V_OnDamagePacketEvaluation;
                else
                    v.OnDamagePacketEvaluation -= V_OnDamagePacketEvaluation;

        }


        private void V_OnDamagePacketEvaluation(DamagePacket obj)
        {
            obj.damage.AddFunction(new ShieldFunction(this));
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
            {
                SetEnabled(false);
                Remove();
            }
        }
        
        class ShieldFunction : MutableValue.IFunction
        {
            BuffShield b;

            public ShieldFunction(BuffShield b)
            {
                this.b = b;
            }

            public FunctionChainPriority Priority => FunctionChainPriority.Addition;

            public float Func(float x)
            {
                if (b.realShield <= x)
                {
                    x -= b.realShield;
                    b.realShield = 0;
                }
                else
                {
                    b.realShield -= x;
                    x = 0;
                }
                return x;
            }
        }
    }
}
