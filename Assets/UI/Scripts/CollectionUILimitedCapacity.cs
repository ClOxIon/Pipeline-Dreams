namespace PipelineDreams
{
    /// <summary>
    /// Collection UI with limited number of objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CollectionUILimitedCapacity<T> : ObjectContainerUI<T> where T: PDObject
    {
        protected IIndividualUI<T> _TemporarySlot;
        protected override void Awake()
        {
            base.Awake();
            if (_TemporarySlot != null)
            {
                ItemUIs.Remove(_TemporarySlot);
                ItemUIs.Insert(0, _TemporarySlot);
            }
            (PI as PDObjectContainerLimitedCapacity<T>).OnContainerCapacityChanged += PI_OnRefreshItemSlotUI;
        }

        private void PI_OnRefreshItemSlotUI(int num)
        {
            for (int i = ItemUIs.Count - 1; i >= 0; i--)
            {
                ItemUIs[i].SetVisible(i < num);
            }
        }


    }
}