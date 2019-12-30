namespace PipelineDreams
{
    public class InstructionBypass : Instruction {
        public InstructionBypass(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionBypassTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionBypassTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
