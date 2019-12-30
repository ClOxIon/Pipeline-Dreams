namespace PipelineDreams
{
    public class InstructionRotationalCharge : Instruction {
        public InstructionRotationalCharge(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionRotationalChargeTask();
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
