namespace PipelineDreams
{
    public class InstructionProjectile : Instruction {
        public InstructionProjectile(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionProjectileTask();
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
