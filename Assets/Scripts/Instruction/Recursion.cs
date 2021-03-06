namespace PipelineDreams.Instruction
{
    public class Recursion : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionRecursionTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionRecursionTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
