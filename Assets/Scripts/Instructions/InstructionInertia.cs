namespace PipelineDreams
{
    public class InstructionInertia : Instruction {

        public InstructionInertia(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant)
        {
        }
        public override IClockTask Operation(float startClock)
        {
            return new InstructionBasicRangedTask() { Op = this, StartClock = startClock, Priority = Priority.PLAYER , EffectDuration = OpData.effectDuration };
        }
    }

}