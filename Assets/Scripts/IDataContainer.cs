using System;

namespace PipelineDreams {
    public interface IDataContainer<T> {

        event Action<T[]> OnRefreshItems;
        event Action<int> OnChangeItemSlotAvailability;
        void AddItem(string name);
        void SwapItem(int index1, int index2);
        void InvokeUIRefresh();
        Data GetItemInfo(int index);
        T PopItem(int index);
        void PushItem(T item);
        void ChangeActivatedSlots(int after);
    }
}