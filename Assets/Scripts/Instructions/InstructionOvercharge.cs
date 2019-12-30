namespace PipelineDreams
{
    public class InstructionOvercharge : Instruction {
        public InstructionOvercharge(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionOverchargeTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionOverchargeTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
