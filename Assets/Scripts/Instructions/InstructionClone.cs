namespace PipelineDreams
{
    public class InstructionClone : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionCloneTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionCloneTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
