namespace PipelineDreams.Instruction
{
    public class Advance : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionAdvanceTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionAdvanceTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
