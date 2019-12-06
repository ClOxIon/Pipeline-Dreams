using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemDefaultActionUI : MonoBehaviour {
    ItemCollection IC;
    ItemCollectionUI ICU;
    private void Awake() {
        FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
        IC = FindObjectOfType<ItemCollection>();
        ICU = GetComponent<ItemCollectionUI>();
        ICU.OnItemUIClick += (x)=>OnItemUse(x);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnItemUse(object value) {
        var i = (int)value;
        IC.InvokeItemAction(i, "DEFAULT");


    }
}
