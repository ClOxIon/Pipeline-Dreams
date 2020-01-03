namespace PipelineDreams
{
    public class InstructionInfuse : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionInfuseTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionInfuseTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
