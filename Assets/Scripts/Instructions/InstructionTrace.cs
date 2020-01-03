namespace PipelineDreams
{
    public class InstructionTrace : Instruction {
        
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
        protected class InstructionTraceTask : InstructionBasicRangedTask
        {
            protected override void OnRunEnd()
            {
                var b = Op.Holder.GetComponent<EntityBuff>();
                if (b != null)
                b.BuffContainer.AddItem("BuffFreeTranslation");
            }
        }
    }

}
