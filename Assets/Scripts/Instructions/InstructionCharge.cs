namespace PipelineDreams
{
    public class InstructionCharge : Instruction {
        public InstructionCharge(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionChargeTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionChargeTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
