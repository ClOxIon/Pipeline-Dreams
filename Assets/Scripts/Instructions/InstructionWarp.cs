namespace PipelineDreams
{
    public class InstructionWarp : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionWarpTask() {MDC = MDC }, startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionWarpTask : InstructionTask
        {
            public MapDataContainer MDC;
            public int WarpLength;
            protected override void OnRunStart()
            {
                /*
                var m = Op.Subject.GetComponent<EntityMove>();
                if (m == null)
                    return;
                for (int i = WarpLength;i>0;i--)
                    if(m.CanStay()
                Op.Subject.GetComponent<EntityMove>().MoveWarp()
                */
            }
        }
    }

}
