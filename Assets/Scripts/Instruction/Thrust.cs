namespace PipelineDreams.Instruction
{
    public class Thrust : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionThrustTask(), startClock);
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
