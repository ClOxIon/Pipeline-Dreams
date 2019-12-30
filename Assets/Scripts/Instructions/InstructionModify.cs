namespace PipelineDreams
{
    public class InstructionModify : Instruction {
        public InstructionModify(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionModifyTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionModifyTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
