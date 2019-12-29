namespace PipelineDreams
{
    public class InstructionCircuitBreaker : Instruction {
        public InstructionCircuitBreaker(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {
            throw new System.NotImplementedException();
        }
    }

}
