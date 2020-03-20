namespace PipelineDreams.Instruction
{
    public class SpatialRotation : Instruction {
        
        public override IClockTask Operation(float startClock)
        {

            return PassParam(new SpatialRotationTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class SpatialRotationTask : BasicRangedTask
        {
            protected override void OnRunStart()
            {
                var b = Op.Holder.GetComponent<Entity.BuffContainerHolder>();
                if (b != null)
                    b.BuffContainer.AddItem("BuffFreeTranslation");
            }
        }
    }

}
