namespace PipelineDreams
{
    public class BuffTargeted : Buff {
        public BuffTargeted(Entity subject, BuffData buffData, TaskManager tm) : base(subject, buffData, tm) {
            Subject.GetComponent<EntityHealth>().DamageRecieveCoef *= 2f;
        }
        public override void Destroy() {
            base.Destroy();

            Subject.GetComponent<EntityHealth>().DamageRecieveCoef /= 2f;
        }
    }
}