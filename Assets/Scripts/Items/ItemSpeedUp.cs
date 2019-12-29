namespace PipelineDreams
{
    /// <summary>
    /// Debug Item.
    /// </summary>
    public class ItemSpeedUp : Item {

        EntityMove PlayerMove;
        public ItemSpeedUp(Entity p, TaskManager cM, ItemData data) : base(p, cM, data) {

        }

        public override void Obtain() {
            base.Obtain();

            PlayerMove = Holder.GetComponent<EntityMove>();
            PlayerMove.SpeedModifier /= 1.5f;
        }
        public override void Remove() {
            base.Remove();
            PlayerMove.SpeedModifier *= 1.5f;


        }


    }
}