namespace PipelineDreams
{
    public class InstructionOrbit : Instruction {
        public InstructionOrbit(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionOrbitTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionOrbitTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
