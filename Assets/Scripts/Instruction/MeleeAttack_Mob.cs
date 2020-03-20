namespace PipelineDreams.Instruction
{
    public class MeleeAttack_Mob : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new BasicMeleeTask(), startClock);
        }
        
    }
    

}
