namespace PipelineDreams
{
    public class InstructionOvercharge : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionOverchargeTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionOverchargeTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
