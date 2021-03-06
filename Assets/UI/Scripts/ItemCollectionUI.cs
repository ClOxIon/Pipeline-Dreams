﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace PipelineDreams {
    public class ItemCollectionUI : CollectionUILimitedCapacity<Item.Item> {
        [SerializeField] ItemUI TemporaryUI;
        [SerializeField] Item.ContainerPlayer ItemContainer;
        protected override void Awake() {
            _TemporarySlot = TemporaryUI as IIndividualUI<Item.Item>;
            PI = ItemContainer;
            base.Awake();
        }

        protected override void PI_OnRefreshUI(Item.Item[] obj) {
            for (int i = ItemUIs.Count - 1; i >= obj.Length; i--) {
                ItemUIs[i].AssignHotkeyUI(FindObjectOfType<PlayerInput>().actions.FindAction("ItemUse").bindings[i].path);
                ItemUIs[i].Clear();
            }
            for (int i = obj.Length - 1; i >= 0; i--) {
                ItemUIs[i].AssignHotkeyUI(FindObjectOfType<PlayerInput>().actions.FindAction("ItemUse").bindings[i].path);
                ItemUIs[i].Refresh(obj[i]);
            }
        }
    }
}