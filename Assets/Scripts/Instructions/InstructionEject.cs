namespace PipelineDreams
{
    public class InstructionEject : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionEjectTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionEjectTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
