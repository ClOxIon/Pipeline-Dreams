namespace PipelineDreams.Instruction
{
    public class Projectile : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionProjectileTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionProjectileTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
