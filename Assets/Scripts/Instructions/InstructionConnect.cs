namespace PipelineDreams
{
    public class InstructionConnect : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionConnectTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionConnectTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
