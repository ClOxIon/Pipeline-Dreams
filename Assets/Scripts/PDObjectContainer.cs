using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{
    public abstract class PDObjectContainer<T> : ScriptableObject where T:PDObject {
        [SerializeField] protected ScriptableObject dataObj;
        [SerializeField] protected Entity.Container EM;
        protected Entity.Entity Holder;
        protected TaskManager TM;
        protected List<T> objs = new List<T>();
        public event Action<T[]> OnRefreshItems;
        public event Action OnContainerInit;
        public virtual void Init(TaskManager tm, Entity.Entity holder)
        {
            TM = tm;
            Holder = holder;
            OnContainerInit?.Invoke();
        }

        /// <summary>
        /// Create new instance of an item and push into this container.
        /// </summary>
        public void AddItem(string name, params object[] args)
        {

            T AddedItem = default;
            PDData AddedItemData;
            var s = name.Split(' ');
            string variant = "";
            var name0 = s[0];
            if (s.Length > 1)
                variant = s[1];

            AddedItemData = (dataObj as IPDDataSet).DataSet.Find((x) => { return x.Name.Equals(name0); });
            if (AddedItemData == null)
            {
                Debug.LogError("Cannot find Item Data named " + name0);
                return;
            }

            //Should double check here when the namespace is null, but we assume that our namespace is not null.
            var tp = Type.GetType(typeof(T).Namespace + "." + name0);
            if (tp != null)
                AddedItem = (T)Activator.CreateInstance(tp);
            else
                Debug.LogError("Cannot find Class named " + typeof(T).Namespace + "." + name0);

            AddedItem.Init(AddedItemData, args);
            PushItem(AddedItem);

        }

        /// <summary>
        /// This method is called to swap the position of two items.
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public void SwapItem(int index1, int index2) 
        { 
            var tmp = objs[index1]; objs[index1] = objs[index2]; objs[index2] = tmp; InvokeUIRefresh(); 
        }
        public void InvokeUIRefresh() {
            OnRefreshItems?.Invoke(objs.ToArray());
        }
        public PDData GetItemInfo(int index) {
            return objs[index].Data;
            
        }
        /// <summary>
        /// This method is used to move an item from a container to somewhere else.
        /// Notice that Remove() is still called.
        /// </summary>
        /// <param name="index"></param>
        public virtual T PopItem(int index)
        {
            var b = objs[index];
            objs.Remove(b);
            b.Remove();
            InvokeUIRefresh();
            return b;
        }

        
        public T PopItem(string name)
        {
            var index = objs.FindIndex((x) => x.Data.Name == typeof(T).Name + name);
            return PopItem(index);
        }

        /// <summary>
        /// This method is used to move an item into the container.
        /// </summary>
        /// <param name="item"></param>
        public virtual void PushItem(T item) {
            item.Obtain(Holder, TM);
            objs.Add(item);
            InvokeUIRefresh();
        }
        
    }
}