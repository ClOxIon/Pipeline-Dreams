namespace PipelineDreams
{
    public class InstructionRotationalCharge : Instruction {
        public InstructionRotationalCharge(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {
            throw new System.NotImplementedException();
        }
    }

}
