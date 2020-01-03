namespace PipelineDreams
{
    public class InstructionFluctuation : Instruction
    {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionFluctuationTask());
        }

    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionFluctuationTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
