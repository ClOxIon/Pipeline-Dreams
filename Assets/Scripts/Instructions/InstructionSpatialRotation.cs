namespace PipelineDreams
{
    public class InstructionSpatialRotation : Instruction {
        
        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionSpatialRotationTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionSpatialRotationTask : InstructionBasicRangedTask
        {
            protected override void OnRunStart()
            {
                var b = Op.Holder.GetComponent<EntityBuff>();
                if (b != null)
                    b.BuffContainer.AddItem("BuffFreeTranslation");
            }
        }
    }

}
