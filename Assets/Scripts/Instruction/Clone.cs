namespace PipelineDreams.Instruction
{
    public class Clone : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionCloneTask(), startClock);
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
