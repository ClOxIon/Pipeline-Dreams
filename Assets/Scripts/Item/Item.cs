using System;

namespace PipelineDreams.Item {
    public class Item : PDObject {
        
        
        public virtual void EffectByTime(float time) { }
        
        
        public virtual void InvokeItemAction(string actionName) {
            if (actionName == "DEFAULT" && (Data as Data).DefaultAction != null)
                InvokeItemAction((Data as Data).DefaultAction);
            if (actionName == "Destroy")
                Remove();
        }
    }
}