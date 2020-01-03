namespace PipelineDreams
{
    public class InstructionModify : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionModifyTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionModifyTask : InstructionTask
        {
            protected override void OnRunStart()
            {
            }
        }
    }

}
