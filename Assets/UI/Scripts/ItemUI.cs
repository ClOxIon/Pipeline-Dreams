using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
    [SerializeField]Image Icon;
    Text text;
    protected Item item;
    protected virtual void Awake() {
        Icon.enabled = false;
        text = GetComponentInChildren<Text>();
    }
    internal void Refresh(Item testIt) {
        item = testIt;
        
        if (item == null) {
            Clear();
            return;
        }
        Debug.Log(text.text);
        text.text = testIt.ItData.Name;
        Icon.sprite = testIt.ItData.Icon;
        Icon.color = new Color(1,1,1,0.7f);
        Icon.enabled = true;
        text.enabled = true;
    }
    public void Clear() {
        Icon.sprite = null;
        text.enabled = false;
        Icon.enabled = false;
    }
}