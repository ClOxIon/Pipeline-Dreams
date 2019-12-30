using System;
using System.Linq;
using System.Collections.Generic;

namespace PipelineDreams.MutableValue
{
    public enum FunctionChainPriority
    {
        Constant = 0, Addition = 10, Multiplication = 20
    }

    /// <summary>
    /// This class is used mainly for mutable stats.
    /// </summary>
    public class FunctionChain
    {
        /// <summary>
        /// Subscribe to this event to add functions by calling AddFunction.
        /// </summary>
        public event Action OnValueRequested;
        List<IFunction> Functions = new List<IFunction>();
        public FunctionChain()
        {

        }
        /// <summary>
        /// This function could be only safely called during OnValueRequested callback.
        /// </summary>
        /// <param name="x"></param>
        public void AddFunction(IFunction x)
        {
            if (x != null)
                Functions.Add(x);
        }
        public virtual float Value
        {
            get
            {
                OnValueRequested?.Invoke();
                Functions.OrderBy((x) => x.Priority);
                float var = 0;
                foreach (var x in Functions)
                {
                    var = x.Func(var);
                }
                Functions.Clear();//Functions are only used once. They should be added again at next evaluation.
                return var;
            }
        }
    }
    public interface IFunction
    {
        FunctionChainPriority Priority { get; }
        float Func(float x);
    }
    public class Addition : IFunction
    {
        public FunctionChainPriority Priority { get; set; } = FunctionChainPriority.Addition;
        public float Value;
        public float Func(float x)
        {
            return x + Value;
        }
    }
    public class Multiplication : IFunction
    {
        public FunctionChainPriority Priority { get; set; } = FunctionChainPriority.Multiplication;
        public float Value;
        public float Func(float x)
        {
            return x * Value;
        }
    }
    public class Constant : IFunction
    {
        public FunctionChainPriority Priority { get; set; } = FunctionChainPriority.Constant;
        public float Value;
        public float Func(float x)
        {
            return Value;
        }
    }
}