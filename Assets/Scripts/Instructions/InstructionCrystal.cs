namespace PipelineDreams
{
    public class InstructionCrystal : Instruction {
        public InstructionCrystal(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionCrystalTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionCrystalTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
