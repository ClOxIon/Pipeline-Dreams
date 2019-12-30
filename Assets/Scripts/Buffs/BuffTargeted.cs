namespace PipelineDreams
{
    public class BuffTargeted : Buff {
        public BuffTargeted(Entity subject, BuffData buffData, TaskManager tm) : base(subject, buffData, tm) {
            Subject.GetComponent<EntityHealth>().OnDamagePacketEvaluation += BuffTargeted_OnDamagePacketEvaluation;
        }

        private void BuffTargeted_OnDamagePacketEvaluation(DamagePacket obj)
        {
            obj.damage.AddFunction(new MutableValue.Multiplication() { Value = 2f });
        }

        public override void Destroy() {
            base.Destroy();
            Subject.GetComponent<EntityHealth>().OnDamagePacketEvaluation -= BuffTargeted_OnDamagePacketEvaluation;
        }
    }
}