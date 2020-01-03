namespace PipelineDreams
{
    public class InstructionRestore : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionRestoreTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionRestoreTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
