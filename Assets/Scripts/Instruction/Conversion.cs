namespace PipelineDreams.Instruction
{
    public class Conversion : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionConversionTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionConversionTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
