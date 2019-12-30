namespace PipelineDreams
{
    public class InstructionDisintegration : Instruction {
        public InstructionDisintegration(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionDisintegrationTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionDisintegrationTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
