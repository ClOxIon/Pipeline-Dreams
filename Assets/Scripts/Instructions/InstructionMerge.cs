namespace PipelineDreams
{
    public class InstructionMerge : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionMergeTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionMergeTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
