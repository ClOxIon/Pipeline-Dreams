namespace PipelineDreams
{
    public class BuffInstManipulate : Buff {
        public BuffInstManipulate(Entity subject, BuffData buffData, TaskManager tm) : base(subject, buffData, tm) {
            Subject.GetComponent<EntityWeapon>().OnDamagePacketDepart+= BuffInstManipulate_OnDamagePacketEvaluation;
        }

        private void BuffInstManipulate_OnDamagePacketEvaluation(DamagePacket obj)
        {
            obj.damage.AddFunction(new MutableValue.Multiplication() { Value = 2f });
        }

        public override void Destroy() {
            base.Destroy();
            Subject.GetComponent<EntityWeapon>().OnDamagePacketDepart -= BuffInstManipulate_OnDamagePacketEvaluation;

        }
    }
}