namespace PipelineDreams
{
    public class InstructionInvariant : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionInvariantTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionInvariantTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
