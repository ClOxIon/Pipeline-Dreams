namespace PipelineDreams {
    public class PlayerAI : EntityAI {

        public override float EntityClock {
            get {
                return CM.Clock;
            }
            set {
                if (value != CM.Clock)
                    CM.AddTime(value - CM.Clock);
            }
        }

        protected override void Act() {
            //Do nothing.
        }

    }
}