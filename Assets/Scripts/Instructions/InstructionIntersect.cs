namespace PipelineDreams
{
    public class InstructionIntersect : Instruction {
        public InstructionIntersect(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionIntersectTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionIntersectTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
