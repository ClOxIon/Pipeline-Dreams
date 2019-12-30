namespace PipelineDreams
{
    public class InstructionCircuitBreaker : Instruction {
        public InstructionCircuitBreaker(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionCircuitBreakerTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionCircuitBreakerTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
