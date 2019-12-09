using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    public class Buff {
        public BuffData BuData;
        [SerializeField] protected TaskManager CM;
        protected Entity Subject;
        public event Action OnDestroy;
        public Buff(Entity subject, BuffData buffData) {
            Subject = subject;
            BuData = buffData;

            CM.OnClockModified += EffectByTime;

        }
        protected virtual void EffectByTime(float Time) {

        }
        public virtual void Destroy() {
            OnDestroy?.Invoke();
        }
    }
    public class BuffTargeted : Buff {
        public BuffTargeted(Entity subject, BuffData buffData) : base(subject, buffData) {
            Subject.GetComponent<EntityHealth>().damageRecieveCoef *= 2f;
        }
        public override void Destroy() {
            base.Destroy();

            Subject.GetComponent<EntityHealth>().damageRecieveCoef /= 2f;
        }
    }
    public class TimedBuff : Buff {
        public float TimeLeft { get; private set; }
        protected float initialTime;
        public TimedBuff(Entity subject, BuffData buffData) : base(subject, buffData) {
            initialTime = CM.Clock;
        }
        protected override void EffectByTime(float Time) {
            TimeLeft = initialTime + BuData.baseDuration - Time;
            if (TimeLeft < 0)
                Destroy();
        }
    }


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


        public void SwapItem(int index1, int index2) {
            Debug.LogError("SwapItem Called at EntityBuff");
        }

        public void InvokeUIRefresh() {
            OnRefreshItems?.Invoke(Buffs.ToArray());
        }

        public Data GetItemInfo(int index) {
            return Buffs[index].BuData;
        }

        public Buff PopItem(int index) {

            Debug.LogError("PopItem Called at EntityBuff");
            return null;
        }

        public void PushItem(Buff item) {
            Debug.LogError("PushItem Called at EntityBuff");
        }

        public void ChangeActivatedSlots(int after) {
            Debug.LogError("ChangeActivatedSlots Called at EntityBuff");
        }
    }
}