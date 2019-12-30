using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{


    public class EntityBuff : MonoBehaviour, IDataContainer<Buff> {
        [SerializeField] BuffDataset DataContainer;
        List<Buff> Buffs;

        TaskManager CM;
        public event Action<Buff[]> OnRefreshItems;
        public event Action<int> OnChangeItemSlotAvailability;

        private void Awake() {
            Buffs = new List<Buff>();


            CM = (TaskManager)FindObjectOfType(typeof(TaskManager));

            CM.OnClockModified += CM_OnClockModified;
        }

        private void CM_OnClockModified(float obj) {
            OnRefreshItems?.Invoke(Buffs.ToArray());
        }

        /// <summary>
        /// Adds the Buff corresponding to the name to the inventory.
        /// </summary>
        /// <param name="name"></param>
        public void AddItem(string name) {
            
            if (Buffs.Find((x) => x.BuData.Name == "Buff" + name) != null)
                return;
            
            Buff AddedBuff;

            BuffData AddedBuffData = DataContainer.Dataset.Find((x) => { return x.Name == name; });
            if (typeof(Buff).Namespace != null)
                AddedBuff = (Buff)Activator.CreateInstance(Type.GetType(typeof(Buff).Namespace + ".Buff" + name), GetComponent<Entity>(), AddedBuffData);
            else {

                AddedBuff = (Buff)Activator.CreateInstance(Type.GetType("Buff" + name), GetComponent<Entity>(), AddedBuffData);
            }

            Buffs.Add(AddedBuff);
            OnRefreshItems?.Invoke(Buffs.ToArray());
        }
        /// <summary>
        /// Add Item with specific arguments.
        /// Float duration is always at the 0th, if the inflicted buff is BuffWithDuration.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="duration"></param>
        public void AddItem(string name, params object[] args)
        {
            var b = Buffs.Find((x) => x.BuData.Name == "Buff" + name);
            if (b != null)
            {
                b.ReInflict(args);
                return;
            }
            Buff AddedBuff;

            BuffData AddedBuffData = DataContainer.Dataset.Find((x) => { return x.Name == name; });
            if (typeof(Buff).Namespace != null)
                AddedBuff = (Buff)Activator.CreateInstance(Type.GetType(typeof(Buff).Namespace + ".Buff" + name), GetComponent<Entity>(), AddedBuffData, args);
            else
            {

                AddedBuff = (Buff)Activator.CreateInstance(Type.GetType("Buff" + name), GetComponent<Entity>(), AddedBuffData, args);
            }

            Buffs.Add(AddedBuff);
            SetRemoveCallback(AddedBuff);
            OnRefreshItems?.Invoke(Buffs.ToArray());
        }
        /// <summary>
        /// Return true when success.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemoveItem(string name) {
            var b = Buffs.Find((x) => x.BuData.Name == "Buff" + name);
            if (b == null)
                return false;
            b.Destroy();
            Buffs.Remove(b);
            return true;
        }
        public void Clear() {
            foreach (var x in Buffs) {
                x.Destroy();
            }
            Buffs.Clear();
        }
        /// <summary>
        /// Subscribe to the destroy event of an item.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="position"></param>
        private void SetRemoveCallback(Buff i)
        {
            i.OnDestroy += () => {

                Buffs.Remove(i);
                OnRefreshItems?.Invoke(Buffs.ToArray());
            };
        }

        public void SwapItem(int index1, int index2) {
            Debug.LogError("SwapItem Called at EntityBuff. This would be ignored.");
        }

        public void InvokeUIRefresh() {
            OnRefreshItems?.Invoke(Buffs.ToArray());
        }

        public Data GetItemInfo(int index) {
            return Buffs[index].BuData;
        }

        public Buff PopItem(int index) {

            Debug.LogError("PopItem Called at EntityBuff. This would be ignored.");
            return null;
        }

        public void PushItem(Buff item) {
            Debug.LogError("PushItem Called at EntityBuff. This would be ignored.");
        }

        public void ChangeActivatedSlots(int after) {
            Debug.LogError("ChangeActivatedSlots Called at EntityBuff. This would be ignored.");
        }
    }
}