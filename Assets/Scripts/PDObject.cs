using System;

namespace PipelineDreams
{
    /// <summary>
    /// the base class of every object in this game that is held by some container, namely, item, instruction, buff.
    /// </summary>
    public abstract class PDObject
    {

        public event Action OnRemove;
        protected TaskManager CM;
        protected Entity.Entity Holder;
        public bool Enabled { get; private set; }
        public PDData Data { get; protected set; }

        /// <summary>
        /// Called when this object is enabled or disabled. For example, an item in a storage is disabled, while an item held by a player is enabled.
        /// </summary>
        /// <param name="enabled"></param>
        public virtual void SetEnabled(bool enabled) {
            Enabled = enabled;
        }
        /// <summary>
        /// Called when this object is moved into a container.
        /// </summary>
        /// <param name="data"></param>
        public virtual void Obtain(Entity.Entity holder, TaskManager cM) {
            
            CM = cM;
            Holder = holder;
        }
        public virtual void Init(PDData data, params object[] args)
        {
            Data = data;
        }
        /// <summary>
        /// Called when this object is removed from a container.
        /// </summary>
        public virtual void Remove() {
            OnRemove?.Invoke();
        }
    }
}