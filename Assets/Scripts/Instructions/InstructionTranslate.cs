namespace PipelineDreams
{
    public class InstructionTranslate : Instruction {
        public InstructionTranslate(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionTranslateTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionTranslateTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
