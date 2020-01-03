namespace PipelineDreams
{
    public class InstructionTranslate : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionTranslateTask());
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
