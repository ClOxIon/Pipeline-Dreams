namespace PipelineDreams
{
    public class BuffFreeRotation : Buff {
        EntityMove m;
        public override void SetEnabled(bool enabled)
        {
            base.SetEnabled(enabled);
            m = Holder.GetComponent<EntityMove>();
            if(enabled)
            m.RTimeModifier.OnEvalRequest += RTimeModifier_OnValueRequested;
            else
                m.RTimeModifier.OnEvalRequest -= RTimeModifier_OnValueRequested;
        }

        private void RTimeModifier_OnValueRequested()
        {
            m.RTimeModifier.AddFunction(new MutableValue.Multiplication() { Value = 0 });
            SetEnabled(false);//This buff is single use.
            Remove();
        }

    }
}