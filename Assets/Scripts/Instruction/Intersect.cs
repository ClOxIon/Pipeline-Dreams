namespace PipelineDreams.Instruction
{
    public class Intersect : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionIntersectTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionIntersectTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
