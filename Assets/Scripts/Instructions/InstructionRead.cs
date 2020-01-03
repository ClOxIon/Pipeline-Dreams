namespace PipelineDreams
{
    public class InstructionRead : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionReadTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionReadTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
