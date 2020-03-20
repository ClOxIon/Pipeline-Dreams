namespace PipelineDreams.Instruction
{
    public class Ray : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionRayTask(), startClock);
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
