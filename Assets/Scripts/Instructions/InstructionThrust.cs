namespace PipelineDreams
{
    public class InstructionThrust : Instruction {
        public InstructionThrust(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {
            throw new System.NotImplementedException();
        }
    }

}