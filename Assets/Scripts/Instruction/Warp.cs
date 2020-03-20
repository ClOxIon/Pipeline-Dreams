namespace PipelineDreams.Instruction
{
    public class Warp : Instruction {

        public override IClockTask Operation(float startClock)
        {

            return PassParam(new WarpTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class WarpTask : InstructionTask
        {
            
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
