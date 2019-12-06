using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemCollectionUI : MonoBehaviour
{
    List<ItemUI> ItemUIs;
    ItemCollection PI;
    [SerializeField] ItemUI TemporarySlot;
    public event Action<int> OnItemUIClick;
    private void Awake() {
        PI = (ItemCollection)FindObjectOfType(typeof(ItemCollection));
        ItemUIs = new List<ItemUI>(GetComponentsInChildren<ItemUI>());
        ItemUIs.Remove(TemporarySlot);
        ItemUIs.Insert(0, TemporarySlot);
        for (int i = 0; i < ItemUIs.Count; i++)
            ItemUIs[i].OnClick += () => OnItemUIClick?.Invoke(i);
        PI.OnRefreshItems += PI_OnRefreshUI;
        PI.OnChangeItemSlotAvailability += PI_OnRefreshItemSlotUI;
    }
    private void Start() {

        PI.InvokeUIRefresh();
    }

    private void PI_OnRefreshItemSlotUI(int num) {
        for (int i = ItemUIs.Count-1; i >= 0; i--) {
            ItemUIs[i].gameObject.SetActive(i<num);
        }
    }

    private void PI_OnRefreshUI(Item[] obj) {
       
        for (int i = ItemUIs.Count-1; i >= obj.Length; i--) {
            ItemUIs[i].AssignHotkeyUI(FindObjectOfType<PlayerInput>().actions.FindAction("ItemUse").bindings[i].path);
            ItemUIs[i].Clear();
        }
        for (int i = obj.Length-1; i >= 0; i--) {
            ItemUIs[i].AssignHotkeyUI(FindObjectOfType<PlayerInput>().actions.FindAction("ItemUse").bindings[i].path);
            ItemUIs[i].Refresh(obj[i]);
        }
    }

    
}
