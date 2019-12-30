namespace PipelineDreams
{
    public class InstructionInvariant : Instruction {
        public InstructionInvariant(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionInvariantTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionInvariantTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
