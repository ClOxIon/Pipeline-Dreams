namespace PipelineDreams
{
    /// <summary>
    /// Debug Item.
    /// </summary>
    public class ItemSpeedUp : Item {

        EntityMove PlayerMove;
        public override void SetEnabled(bool enabled)
        {
            base.SetEnabled(enabled);
            PlayerMove = Holder.GetComponent<EntityMove>();

            if (PlayerMove != null)
                if(enabled)
                PlayerMove.TTimeModifier.OnValueRequested += TTimeModifier_OnValueRequested;
            else
                    PlayerMove.TTimeModifier.OnValueRequested -= TTimeModifier_OnValueRequested;
        }


        private void TTimeModifier_OnValueRequested()
        {
            PlayerMove.TTimeModifier.AddFunction(new MutableValue.Multiplication() { Value = 0.666f });
        }



    }
}