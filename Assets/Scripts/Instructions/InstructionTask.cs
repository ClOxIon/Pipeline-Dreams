using System.Collections;
using UnityEngine;

namespace PipelineDreams
{
    public abstract partial class Instruction {
        protected abstract class InstructionTask : IClockTask
        {
            public Priority Priority { get; set; }
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
            protected abstract void OnRunStart();

        }
    }

}