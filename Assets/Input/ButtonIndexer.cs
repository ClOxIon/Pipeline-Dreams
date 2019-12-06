using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class ButtonIndexer : InputProcessor<float> {
#if UNITY_EDITOR
    static ButtonIndexer() {
        Initialize();
    }
#endif
    [Tooltip("The value of this input would be this value")]
    public int Index = 0;
    [RuntimeInitializeOnLoadMethod]
    static void Initialize() {
        InputSystem.RegisterProcessor<ButtonIndexer>();
        
    }

    public override float Process(float value, InputControl control) {
        if (control is ButtonControl && control.IsActuated())
            return Index;
        return value;

    }

    //...
}