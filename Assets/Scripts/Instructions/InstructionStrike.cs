namespace PipelineDreams
{
    public class InstructionStrike : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionStrikeTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionStrikeTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
