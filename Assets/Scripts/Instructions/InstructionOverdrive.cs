namespace PipelineDreams
{
    public class InstructionOverdrive : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionOverdriveTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionOverdriveTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
