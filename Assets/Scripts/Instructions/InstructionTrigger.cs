namespace PipelineDreams
{
    public class InstructionTrigger : Instruction {
        public InstructionTrigger(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionTriggerTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionTriggerTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
