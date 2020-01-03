namespace PipelineDreams
{
    public class InstructionRay : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionRayTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionRayTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
