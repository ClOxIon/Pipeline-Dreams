using System;
using UnityEngine;

namespace PipelineDreams
{
    public abstract class Buff {
        public BuffData BuData;
        protected TaskManager CM;
        protected Entity Subject;
        public event Action OnDestroy;
        public Buff(Entity subject, BuffData buffData, TaskManager tm) {
            Subject = subject;
            BuData = buffData;
            CM = tm;
            CM.OnClockModified += EffectByTime;

        }
        /// <summary>
        /// Called by taskmanager.
        /// </summary>
        /// <param name="Time">Current clock of the taskmanager.</param>
        protected virtual void EffectByTime(float Time) {

        }
        public virtual void Destroy() {
            OnDestroy?.Invoke();
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