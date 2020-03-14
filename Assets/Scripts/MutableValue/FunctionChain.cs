using System;
using System.Linq;
using System.Collections.Generic;

namespace PipelineDreams.MutableValue
{
    public enum FunctionChainPriority
    {
        Constant = 0, Addition = 10, Multiplication = 20, Delayed = 50
    }

    /// <summary>
    /// This class is used mainly for mutable stats.
    /// </summary>
    public class FunctionChain
    {
        /// <summary>
        /// Subscribe to this event to add functions by calling AddFunction.
        /// </summary>
        public event Action OnEvalRequest;
        public event Action<float> OnEvalComplete;
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

            //Beware of lambda capture!!
            //hpVar.CurrentValue.OnValueRequested += () => { hpVar.CurrentValue.AddFunction(new MutableValue.Multiplication() { Value = mhpVar.CurrentValue.Value }); };
            if (x != null)
                Functions.Add(x);
        }
        private float value;

        /// <summary>
        /// Implemented for performance.
        /// If this variable is marked true, this functionchain is evaluated once at the next value request.
        /// IT IS THE CHANGERS' RESPONSIBILITY TO EVALUATE THE FUNCTION!
        /// </summary>
        public bool EvalAtNextGet = false;
        /// <summary>
        /// Getting this field immidiately evaluates the functionchain and clears it, if and only if EvalAtNextGet is true. Otherwise, it returns the value of the last eval.
        /// </summary>
        public virtual float Value
        {
            get
            {
                if (EvalAtNextGet) {
                    OnEvalRequest?.Invoke();
                    Functions.OrderBy((x) => x.Priority);
                    float var = 0;
                    foreach (var x in Functions) {
                        var = x.Func(var);
                    }
                    Functions.Clear();//Functions are only used once. They should be added again at next evaluation.
                    value = var;
                    EvalAtNextGet = false;
                    OnEvalComplete?.Invoke(value);
                }
                return value;
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

        public Addition()
        {
        }

        public Addition(float value)
        {
            Value = value;
        }

        public float Func(float x)
        {
            return x + Value;
        }
    }
    public class Multiplication : IFunction
    {
        public FunctionChainPriority Priority { get; set; } = FunctionChainPriority.Multiplication;
        public float Value;

        public Multiplication()
        {
        }

        public Multiplication(float value)
        {
            Value = value;
        }

        public float Func(float x)
        {
            return x * Value;
        }
    }
    public class Constant : IFunction
    {
        public FunctionChainPriority Priority { get; set; } = FunctionChainPriority.Constant;
        public float Value;

        public Constant()
        {
        }

        public Constant(float value)
        {
            Value = value;
        }

        public float Func(float x)
        {
            return Value;
        }
    }
}