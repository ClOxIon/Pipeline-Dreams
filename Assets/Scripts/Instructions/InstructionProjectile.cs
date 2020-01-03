namespace PipelineDreams
{
    public class InstructionProjectile : Instruction {

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
