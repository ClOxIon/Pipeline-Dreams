namespace PipelineDreams
{
    public class InstructionElectrodynamics : Instruction {
        public InstructionElectrodynamics(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {

            return new InstructionElectrodynamicsTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionElectrodynamicsTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
