using System.Collections;
using UnityEngine;

namespace PipelineDreams
{
    public class InstructionManipulate : Instruction {
        public InstructionManipulate(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {
            return new DirectionalFieldInstructionTask(){ Op = this, StartClock = startClock, Priority = Priority.PLAYER };
        }
    }
    public abstract partial class Instruction {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class DirectionalFieldInstructionTask : IClockTask
        {
            public Priority Priority { get; set; }
            public Instruction Op;
            public float StartClock { get; set; }
            public float Accuracy = 0;
            /// <summary>
            /// Duration of the animation, seconds
            /// </summary>
            float Duration = 1f;
            public IEnumerator Run()
            {
                var _entity = Op.EM.FindEntityInLine(Util.LHQToFace(Op.Player.IdealRotation), Op.Player);
                if (_entity != null)
                    Op.Player.GetComponent<EntityWeapon>().PerformAttack(_entity, StartClock, 0, 0, Op.OpData.fieldCoef, Accuracy);
                if (Op.gun != null)
                    Op.gun.trigger = true;
                float time = 0;
                //Animation events could be called here.
                while (time < Duration)
                {

                    yield return null;
                    time += Time.deltaTime;
                }
            }


        }
    }
}