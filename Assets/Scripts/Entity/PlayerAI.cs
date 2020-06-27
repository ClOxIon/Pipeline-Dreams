namespace PipelineDreams.Entity
{
    public class PlayerAI : AI {

        public override float EntityClock {
            get {
                return CM.Clock;
            }
            set {
                if (value - CM.Clock > 0)
                    CM.AddSequentialTask(new Instruction.Container.InstCheckTask() { StartClock = value, cont = GetComponent<InstructionContainerHolder>().AbilityContainer });
                CM.AddTime(value - CM.Clock);
                
            }
        }

        protected override void Act() {
            //Do nothing.
        }
    }
}