using System;
using UnityEngine;

namespace PipelineDreams
{
    public class Buff {
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
        protected virtual void EffectByTime(float Time) {

        }
        public virtual void Destroy() {
            OnDestroy?.Invoke();
        }
    }
}