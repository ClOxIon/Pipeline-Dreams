namespace PipelineDreams
{
    public class InstructionThrust : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionThrustTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionThrustTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
