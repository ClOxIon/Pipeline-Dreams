namespace PipelineDreams.Instruction
{
    public class ReversedConnect : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionReversedConnectTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionReversedConnectTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
