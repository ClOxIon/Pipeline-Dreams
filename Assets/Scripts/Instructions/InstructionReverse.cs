namespace PipelineDreams
{
    public class InstructionReverse : Instruction {
        public InstructionReverse(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionReverseTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionReverseTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
