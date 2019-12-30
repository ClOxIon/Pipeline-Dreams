namespace PipelineDreams
{
    public class InstructionInfuse : Instruction {
        public InstructionInfuse(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionInfuseTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionInfuseTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
