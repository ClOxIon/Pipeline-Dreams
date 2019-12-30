namespace PipelineDreams
{
    public class InstructionSubstitute : Instruction {
        public InstructionSubstitute(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionSubstituteTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionSubstituteTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
