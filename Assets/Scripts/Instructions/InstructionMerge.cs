namespace PipelineDreams
{
    public class InstructionMerge : Instruction {
        public InstructionMerge(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionMergeTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionMergeTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
