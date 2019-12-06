using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemDefaultActionUI : MonoBehaviour {
    public event Action<int> OnButtonPressed;
    ItemCollection IC;
    private void Awake() {
        FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
        IC = FindObjectOfType<ItemCollection>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnItemSlot(object value) {
        var i = (int)value;
        IC.InvokeItemAction(i, "DEFAULT");


    }
}
