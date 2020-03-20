namespace PipelineDreams.Instruction
{
    public class Substitute : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionSubstituteTask(), startClock);
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
