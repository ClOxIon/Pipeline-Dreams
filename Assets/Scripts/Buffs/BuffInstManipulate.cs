namespace PipelineDreams
{
    public class BuffInstManipulate : Buff {
        public BuffInstManipulate(Entity subject, BuffData buffData, TaskManager tm) : base(subject, buffData, tm) {
            Subject.GetComponent<EntityHealth>().DamageRecieveCoef *= 2f;
        }
        public override void Destroy() {
            base.Destroy();

            Subject.GetComponent<EntityHealth>().DamageRecieveCoef /= 2f;
        }
    }
}