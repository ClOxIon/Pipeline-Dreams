namespace PipelineDreams
{
    public class InstructionReversedConnect : Instruction {
        public InstructionReversedConnect(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionReversedConnectTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionReversedConnectTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
