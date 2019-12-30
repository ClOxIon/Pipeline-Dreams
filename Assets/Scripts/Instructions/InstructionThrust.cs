namespace PipelineDreams
{
    public class InstructionThrust : Instruction {
        public InstructionThrust(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionThrustTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionThrustTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
