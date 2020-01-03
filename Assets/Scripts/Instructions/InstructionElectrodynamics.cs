namespace PipelineDreams
{
    public class InstructionElectrodynamics : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionElectrodynamicsTask());
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
