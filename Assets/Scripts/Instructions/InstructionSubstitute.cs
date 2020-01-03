namespace PipelineDreams
{
    public class InstructionSubstitute : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionSubstituteTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionSubstituteTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
