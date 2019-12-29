using System;

namespace PipelineDreams {
    public class Item {
        public ItemData ItData;
        public event Action OnRemove;
        protected TaskManager CM;
        protected Entity Holder;
        public Item(Entity holder, TaskManager cM, ItemData data) {
            Holder = holder;
            CM = cM;
            ItData = data;
            ItData.ItemActions = ItemActions;
            ItData.DefaultAction = DefaultAction;
        }
        public virtual string[] ItemActions {
            get {

                return new string[] { "Destroy" };
            }
        }
        public virtual string DefaultAction {
            get {

                return null;
            }
        }
        /// <summary>
        /// Called when the item is moved into player's inventory.
        /// </summary>
        /// <param name="data"></param>
        public virtual void Obtain() {
            
        }
        public virtual void EffectByTime(float time) { }
        /// <summary>
        /// Called when the item is removed from player's inventory.
        /// </summary>
        public virtual void Remove() {
            OnRemove?.Invoke();
        }
        public virtual void InvokeItemAction(string actionName) {
            if (actionName == "DEFAULT" && DefaultAction != null)
                InvokeItemAction(DefaultAction);
            if (actionName == "Destroy")
                Remove();
        }
    }
}