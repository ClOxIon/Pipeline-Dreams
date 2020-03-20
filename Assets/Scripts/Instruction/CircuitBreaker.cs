namespace PipelineDreams.Instruction
{
    public class CircuitBreaker : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return new InstructionCircuitBreakerTask();
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionCircuitBreakerTask : InstructionTask
        {
            protected override void OnRunStart()
            {
                var b = Op.Holder.GetComponent<Entity.BuffContainerHolder>();
                if(b!=null)
                b.BuffContainer.AddItem("Shield", Op.Data.FindParameterInt("Shield"));
            }
        }
    }

}
