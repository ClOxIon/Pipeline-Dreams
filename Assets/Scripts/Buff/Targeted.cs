namespace PipelineDreams.Buff
{
    public class Targeted : Buff {
        private void BuffTargeted_OnDamagePacketEvaluation(DamagePacket obj)
        {
            obj.damage.AddFunction(new MutableValue.Multiplication() { Value = 2f });
        }
        public override void SetEnabled(bool enabled)
        {
            base.SetEnabled(enabled);
            var h = Holder.GetComponent<Entity.Health>();
            if (h != null) 
            if(enabled)
                    h.OnDamagePacketArrive+= BuffTargeted_OnDamagePacketEvaluation;
            else
                    h.OnDamagePacketArrive-= BuffTargeted_OnDamagePacketEvaluation;
        }
    }
}