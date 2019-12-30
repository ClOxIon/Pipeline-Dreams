namespace PipelineDreams
{
    public class InstructionConversion : Instruction {
        public InstructionConversion(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionConversionTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionConversionTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
