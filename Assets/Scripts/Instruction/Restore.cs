namespace PipelineDreams.Instruction
{
    public class Restore : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionRestoreTask(), startClock);
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
