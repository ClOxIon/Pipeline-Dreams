using System;

namespace PipelineDreams
{
    /// <summary>
    /// Container with limited number of objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PDObjectContainerLimitedCapacity<T> : PDObjectContainer<T> where T : PDObject {
        public event Action<int> OnContainerCapacityChanged;
        readonly int temporarySlotIndex = 0;
        int capacity;
        public void ChangeCapacity(int after)
        {
            capacity = after;
            if (capacity < objs.Count)
            {
                for (int i = capacity; i < objs.Count; i++)
                {
                    objs[i]?.Remove();
                }
                objs.RemoveRange(after, objs.Count - after);
            }
            OnContainerCapacityChanged?.Invoke(after);
            InvokeUIRefresh();
        }
        /// <summary>
        /// This method is used to move an item into inventory.
        /// </summary>
        /// <param name="item"></param>
        public override void PushItem(T item)
        {
            bool flag = true;
            for (int i = 1; i < objs.Count; i++)
            {
                if (objs[i] == null)
                {
                    objs[i] = item;
                    SetRemoveCallback(item);
                    flag = false;
                    break;
                }

            }
            if (flag)
            {
                //Instead of opening Item destroy prompt, the item in the temporary slot will be overwritten without prompt.
                var i = temporarySlotIndex;//Temporary slot
                objs[i]?.Remove();
                objs[i] = item;
                SetRemoveCallback(item);

            }
            item.Obtain(Holder, TM);
            InvokeUIRefresh();
        }
    }
}