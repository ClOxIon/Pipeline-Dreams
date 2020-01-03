namespace PipelineDreams
{
    public class InstructionInertia : Instruction {
        public override IClockTask Operation(float startClock)
        {
            return PassParam(new InstructionBasicRangedTask());
        }
    }

}