namespace PipelineDreams.Instruction
{
    public class Inertia : Instruction {
        public override IClockTask Operation(float startClock)
        {
            return PassParam(new BasicRangedTask(), startClock);
        }
    }

}