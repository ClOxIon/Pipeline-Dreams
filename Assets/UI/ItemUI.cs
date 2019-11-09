using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
    [SerializeField]Image Icon;
    Item item;
    private void Awake() {
        Icon.enabled = false;
    }
    internal void Refresh(Item testIt) {
        item = testIt;
        if (item == null) {
            Clear();
            return;
        }
        Icon.sprite = testIt.ItData.Icon;
        Icon.enabled = true;
    }
    public void Clear() {
        Icon.sprite = null;
        Icon.enabled = false;
    }
}