namespace PipelineDreams
{
    public class InstructionCharge : Instruction {
        
        public override IClockTask Operation(float startClock)
        {

            return new InstructionChargeTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionChargeTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
