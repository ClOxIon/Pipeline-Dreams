namespace PipelineDreams
{
    public class InstructionTrace : Instruction {
        public InstructionTrace(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionTraceTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionTraceTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
