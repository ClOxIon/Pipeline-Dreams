namespace PipelineDreams.Item
{
    /// <summary>
    /// Debug Item.
    /// </summary>
    public class ItemSpeedUp : Item {

        Entity.Move PlayerMove;
        public override void SetEnabled(bool enabled)
        {
            base.SetEnabled(enabled);
            PlayerMove = Holder.GetComponent<Entity.Move>();

            if (PlayerMove != null)
                if(enabled)
                PlayerMove.TTimeModifier.OnEvalRequest += TTimeModifier_OnValueRequested;
            else
                    PlayerMove.TTimeModifier.OnEvalRequest -= TTimeModifier_OnValueRequested;
        }


        private void TTimeModifier_OnValueRequested()
        {
            PlayerMove.TTimeModifier.AddFunction(new MutableValue.Multiplication() { Value = 0.666f });
        }



    }
}