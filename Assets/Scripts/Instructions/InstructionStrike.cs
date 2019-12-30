namespace PipelineDreams
{
    public class InstructionStrike : Instruction {
        public InstructionStrike(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionStrikeTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionStrikeTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
