namespace PipelineDreams
{
    public class InstructionDisintegration : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionDisintegrationTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionDisintegrationTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
