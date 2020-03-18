namespace PipelineDreams
{
    public class InstructionMeleeAttack_Mob : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionBasicMeleeTask(), startClock);
        }
        
    }
    

}
