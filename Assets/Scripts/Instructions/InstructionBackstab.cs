namespace PipelineDreams
{
    public class InstructionBackstab : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionBackstabTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionBackstabTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
