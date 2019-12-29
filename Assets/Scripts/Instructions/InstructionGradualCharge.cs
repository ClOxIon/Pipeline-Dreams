namespace PipelineDreams
{
    public class InstructionGradualCharge : Instruction {
        public InstructionGradualCharge(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {
            throw new System.NotImplementedException();
        }
    }

}
