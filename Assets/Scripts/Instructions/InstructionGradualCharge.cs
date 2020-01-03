namespace PipelineDreams
{
    public class InstructionGradualCharge : Instruction {
        
        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionGradualChargeTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionGradualChargeTask : InstructionTask
        {
            protected override void OnRunStart()
            {
                var b = Op.Holder.GetComponent<EntityBuff>();
                if (b != null)
                    b.BuffContainer.AddItem("BuffInstDamage",0.2f,10f);
            }
        }
    }

}
