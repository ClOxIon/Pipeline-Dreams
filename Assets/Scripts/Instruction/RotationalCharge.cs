namespace PipelineDreams.Instruction
{
    public class RotationalCharge : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionRotationalChargeTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionRotationalChargeTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
