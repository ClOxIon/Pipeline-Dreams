namespace PipelineDreams
{
    public class InstructionRearrange : Instruction {
        
        public override IClockTask Operation(float startClock)
        {
            return PassParam(new InstructionRearrangeTask(), startClock);
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
                Op.PC.PushCommand((Op.Data as InstructionData).Commands[0]);
            }
        }
    }

}