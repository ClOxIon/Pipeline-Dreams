namespace PipelineDreams.Instruction
{
    public class Eject : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionEjectTask(), startClock);
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
