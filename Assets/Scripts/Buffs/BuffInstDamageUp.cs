using UnityEngine;
using System.Collections;
using PipelineDreams.MutableValue;

namespace PipelineDreams
{
    public class BuffInstDamageUp : BuffWithDuration
    {
        float damageBonusAmount;
        public override void Init(PDData data, params object[] args)
        {
            base.Init(data, args);
        
            if ((args[1] as float?).HasValue)
                damageBonusAmount = (args[1] as float?).Value;
            else
                Debug.LogError("BuffInstDamageUp inflicted without float damageBonusAmount at the 1th argument: " + Data.Name);

            
            
        }
        public override void SetEnabled(bool enabled)
        {
            base.SetEnabled(enabled);
            var v = Holder.GetComponent<EntityWeapon>();
            if (v != null)
            {
                
                if (enabled)
                    v.OnDamagePacketDepart += V_OnDamagePacketEvaluation;
                else
                    v.OnDamagePacketDepart -= V_OnDamagePacketEvaluation;
            }
        }
        private void V_OnDamagePacketEvaluation(DamagePacket obj)
        {
            if(obj.damageCause==DamageCause.Instruction)
            obj.damage.AddFunction(new Multiplication(damageBonusAmount + 1f));
        }

        public override void ReInflict(params object[] args)
        {
            base.ReInflict(args);
            if ((args[1] as float?).HasValue)
                damageBonusAmount = (args[1] as float?).Value;
            else
                Debug.LogError("BuffInstDamageUp reinflicted without float damageBonusAmount at the 1th argument: " + Data.Name);

        }

    }
}
