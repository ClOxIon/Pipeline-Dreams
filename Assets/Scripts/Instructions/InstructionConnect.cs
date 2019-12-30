namespace PipelineDreams
{
    public class InstructionConnect : Instruction {
        public InstructionConnect(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionConnectTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionConnectTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
