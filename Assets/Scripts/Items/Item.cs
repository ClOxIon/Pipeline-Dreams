using System;

namespace PipelineDreams {
    public class Item : PDObject {
        
        
        public virtual void EffectByTime(float time) { }
        
        
        public virtual void InvokeItemAction(string actionName) {
            if (actionName == "DEFAULT" && (Data as ItemData).DefaultAction != null)
                InvokeItemAction((Data as ItemData).DefaultAction);
            if (actionName == "Destroy")
                Remove();
        }
    }
}