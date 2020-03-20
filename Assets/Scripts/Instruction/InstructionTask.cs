using System.Collections;
using UnityEngine;

namespace PipelineDreams.Instruction
{
    public abstract partial class Instruction {
        protected abstract class InstructionTask : IClockTask
        {
            public TaskPriority Priority { get; set; }
            public Instruction Op;
            public float StartClock { get; set; }
            public float Accuracy = 0;
            /// <summary>
            /// Duration of the animation, seconds
            /// </summary>
            public float EffectDuration = 1f;
            public IEnumerator Run()
            {
                
                Op.TriggerEffect(true);
                float time = 0;
                OnRunStart();
                //Animation events could be called here.
                while (time < EffectDuration)
                {

                    yield return null;
                    time += Time.deltaTime;
                }
                Op.TriggerEffect(false);
            }
            /// <summary>
            /// Called immidiately after this task is called. Do not call bask method when overridden.
            /// </summary>
            protected abstract void OnRunStart();
            /// <summary>
            /// Called after any animation. Do not call bask method when overridden.
            /// </summary>
            protected virtual void OnRunEnd() { 
            
            }
        }
    }

}