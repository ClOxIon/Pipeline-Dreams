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
            /// <summary>
            /// Duration of the animation, seconds
            /// </summary>
            public float EffectDuration = 1f;
            public IEnumerator Run()
            {
                
               
                
                OnRunStart();
                yield return OnRun();
               
            }
            /// <summary>
            /// Called immidiately after this task is called. Do not call the base method when overridden.
            /// </summary>
            protected virtual void OnRunStart() { }
            /// <summary>
            /// Called after OnRunStart. Animation Coroutine could be implemented here. Do not call the base method when overridden.
            /// </summary>
            protected virtual IEnumerator OnRun() {
                return null;
                /*
                private IEnumerator WaitForAnimation ( Animation animation )
                {
                    do
                    {
                        yield return null;
                    } while ( animation.isPlaying );
                }
                
                */
            }
        }
    }

}