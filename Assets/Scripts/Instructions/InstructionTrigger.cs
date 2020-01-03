namespace PipelineDreams
{
    public class InstructionTrigger : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionTriggerTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionTriggerTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
