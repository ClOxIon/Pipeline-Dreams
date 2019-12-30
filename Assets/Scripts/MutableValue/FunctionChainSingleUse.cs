using System;
using System.Linq;
using System.Collections.Generic;

namespace PipelineDreams.MutableValue
{

    /// <summary>
    /// This class is used mainly for mutable stats. The value can be only evaluated once, and is archived.
    /// </summary>
    public class FunctionChainSingleUse:FunctionChain
    {
        bool _eval = false;
        float _value = 0;
        
        public override float Value
        {
            get
            {
                if(!_eval)
                _value = base.Value;
                _eval = true;
                return _value;
            }
        }
        /// <summary>
        /// Calling this method archives the value and returns new inevaluated FunctionChainSingleUse, reflecting the value as a constant function in the returned object.
        /// </summary>
        /// <returns></returns>
        public FunctionChainSingleUse EvaluateAsConstFunc() {
            var f = new FunctionChainSingleUse();
            f.AddFunction(new Constant() { Value = Value });
            return f;
        }
    }
    
}