namespace PipelineDreams
{
    public class InstructionRearrange : Instruction {
        public InstructionRearrange(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionRearrangeTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionRearrangeTask : InstructionTask
        {
            protected override void OnRunStart()
            {
                Op.PC.PushCommand(Op.OpData.Commands[0]);
            }
        }
    }

}