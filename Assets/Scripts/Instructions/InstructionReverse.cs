namespace PipelineDreams
{
    public class InstructionReverse : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionReverseTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionReverseTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
