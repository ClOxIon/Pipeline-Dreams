using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{
    public abstract class PDObjectContainer<T> : ScriptableObject where T:PDObject {
        [SerializeField] protected ScriptableObject dataObj;
        [SerializeField] protected EntityDataContainer EM;
        protected Entity Holder;
        protected TaskManager TM;
        protected List<T> objs = new List<T>();
        public event Action<T[]> OnRefreshItems;
        public event Action OnContainerInit;
        public virtual void Init(TaskManager tm, Entity holder)
        {
            TM = tm;
            Holder = holder;
            OnContainerInit?.Invoke();
        }
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
                Debug.LogError("InstructionCollection.AddInstruction(): Cannot find Instruction named " + name0);
                return;
            }

            //Should double check here when the namespace is null, but we assume that our namespace is not null.
            var tp = Type.GetType(typeof(T).Namespace + "." + typeof(T).Name + name0);
            if (tp != null)
                AddedItem = (T)Activator.CreateInstance(tp);

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
            if (index < objs.Count)
                return objs[index]?.Data;
            return null;
        }
        /// <summary>
        /// This method is used to move an item from a container to somewhere else.
        /// Notice that Remove() is still called.
        /// </summary>
        /// <param name="index"></param>
        public T PopItem(int index)
        {
            var i = objs[index];
            i.Remove();

            return i;
        }

        
        public T PopItem(string name)
        {
            var b = objs.Find((x) => x.Data.Name == typeof(T).Name + name);
            objs.Remove(b);
            return b;
        }

        /// <summary>
        /// This method is used to move an item into a container.
        /// </summary>
        /// <param name="item"></param>
        public virtual void PushItem(T item) {
            item.Obtain(Holder, TM);
                objs.Add(item);
        }
        /// <summary>
        /// Subscribe to the destroy event of an object;
        /// </summary>
        /// <param name="i"></param>
        /// <param name="position"></param>
        protected void SetRemoveCallback(T i)
        {
            i.OnRemove += () => {

                objs[objs.IndexOf(i)] = null;
                InvokeUIRefresh();
            };
        }
    }
}