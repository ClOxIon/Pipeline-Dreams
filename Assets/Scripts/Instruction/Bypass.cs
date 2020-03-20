namespace PipelineDreams.Instruction
{
    public class Bypass : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionBypassTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionBypassTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
