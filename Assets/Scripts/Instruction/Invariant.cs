namespace PipelineDreams.Instruction
{
    public class Invariant : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionInvariantTask(), startClock);
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
