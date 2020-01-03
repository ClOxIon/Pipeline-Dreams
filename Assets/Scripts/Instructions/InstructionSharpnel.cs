namespace PipelineDreams
{
    public class InstructionSharpnel : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionSharpnelTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionSharpnelTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
