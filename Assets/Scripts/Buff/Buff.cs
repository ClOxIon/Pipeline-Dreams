using System;
using UnityEngine;

namespace PipelineDreams.Buff
{
    public abstract class Buff : PDObject {
        public override void SetEnabled(bool enabled) {
            base.SetEnabled(enabled);
            if (enabled)
                CM.OnClockModified += EffectByTime;
            else
                CM.OnClockModified -= EffectByTime;
        }

        
        /// <summary>
        /// Called by taskmanager.
        /// </summary>
        /// <param name="Time">Current clock of the taskmanager.</param>
        protected virtual void EffectByTime(float Time) {

        }
        
        /// <summary>
        /// When the same kind of buff is inflicted when a buff is already active, this method of already existing buff is called instead of instantiating new buff.
        /// </summary>
        /// <param name="duration"></param>
        public virtual void ReInflict(params object[] args)
        {
           
        }
    }
}