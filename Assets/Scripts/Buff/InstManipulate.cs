namespace PipelineDreams.Buff
{
    public class InstManipulate : Buff {
        public override void SetEnabled(bool enabled)
        {
            base.SetEnabled(enabled);
            var m = Holder.GetComponent<Entity.WeaponHolder>();
            if (enabled)
                m.OnDamagePacketDepart += BuffInstManipulate_OnDamagePacketEvaluation;
            else
                m.OnDamagePacketDepart -= BuffInstManipulate_OnDamagePacketEvaluation;
        }
       
        private void BuffInstManipulate_OnDamagePacketEvaluation(DamagePacket obj)
        {
            obj.damage.AddFunction(new MutableValue.Multiplication() { Value = 2f });
            SetEnabled(false);//This buff is single use.
            Remove();
        }

    }
}