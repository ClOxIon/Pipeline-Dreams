using System.Collections;

namespace PipelineDreams.Instruction
{
    public class Trace : Instruction {
        
        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionTraceTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionTraceTask : BasicRangedTask
        {
            protected override IEnumerator OnRun()
            {
                var b = Op.Holder.GetComponent<Entity.BuffContainerHolder>();
                if (b != null)
                b.BuffContainer.AddItem("BuffFreeTranslation");
                return null;
            }
        }
    }

}
