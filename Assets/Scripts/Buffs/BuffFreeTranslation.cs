namespace PipelineDreams
{
    public class BuffFreeTranslation : Buff {
        EntityMove m;
        public override void SetEnabled(bool enabled)
        {
            base.SetEnabled(enabled);
            m = Holder.GetComponent<EntityMove>();
            if (enabled)
                m.RTimeModifier.OnValueRequested += RTimeModifier_OnValueRequested;
            else
                m.RTimeModifier.OnValueRequested -= RTimeModifier_OnValueRequested;
        }

        private void RTimeModifier_OnValueRequested()
        {
            m.TTimeModifier.AddFunction(new MutableValue.Multiplication() { Value = 0 });
            SetEnabled(false);//This buff is single use.
            Remove();
        }

        
    }
}