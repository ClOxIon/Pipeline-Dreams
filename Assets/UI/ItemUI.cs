﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
    [SerializeField]Image Icon;
    Text text;
    Item item;
    private void Awake() {
        Icon.enabled = false;
        text = GetComponentInChildren<Text>();
    }
    internal void Refresh(Item testIt) {
        item = testIt;
        
        if (item == null) {
            Clear();
            return;
        }
        text.text = testIt.ItData.Name;
        Icon.sprite = testIt.ItData.Icon;
        Icon.enabled = true;
        text.enabled = true;
    }
    public void Clear() {
        Icon.sprite = null;
        text.enabled = false;
        Icon.enabled = false;
    }
}