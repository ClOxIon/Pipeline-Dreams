using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {

    public class ItemCollectionInfoUI : CollectionInfoUI<Item.Item> {
        [SerializeField] Entity.Entity Player;
        [SerializeField] Item.ContainerPlayer ItemContainer;
        protected override void Awake() {
            base.Awake();

            IC = ItemContainer;
            PW = Player.GetComponent<Entity.WeaponHolder>();
        }
        Entity.WeaponHolder PW;

        List<Button> ItemActionButtons = new List<Button>();

        [SerializeField] SelectableWeaponUI WeaponSlot;
        [SerializeField] Button ItemActionButtonPrefab;
        [SerializeField] Transform ItemActionButtonPanel;
        public event Action OnWeaponSelected;
        public event Action<Item.Item, string> OnButtonPressed;
        protected override void RefreshInfo() {
            base.RefreshInfo();

            SetItemButtons();
        }
        void OnWeaponSlot(object value) {

            SelectedItemIndex = -1;
            SelectedItemData = PW.WeaponData;
            OnWeaponSelected?.Invoke();
            RefreshInfo();

        }
        void SetItemButtons() {
            if (SelectedItemData == null) {
                for (int i = 0; i < ItemActionButtons.Count; i++) {

                    ItemActionButtons[i].gameObject.SetActive(false);
                }
                return;
            } else {
                var data = SelectedItemData as Item.Data;
                if (ItemActionButtons.Count < data.ItemActions.Length)
                    for (int i = ItemActionButtons.Count; i < data.ItemActions.Length; i++)
                        ItemActionButtons.Add(Instantiate(ItemActionButtonPrefab, ItemActionButtonPanel));
                for (int i = 0; i < data.ItemActions.Length; i++) {
                    ItemActionButtons[i].gameObject.SetActive(true);
                    var t = data.ItemActions[i];
                    ItemActionButtons[i].onClick.AddListener(() => (IC as Item.ContainerPlayer).InvokeItemAction(SelectedItemIndex, t));
                    ItemActionButtons[i].GetComponentInChildren<Text>().text = t;


                }
                for (int i = data.ItemActions.Length; i < ItemActionButtons.Count; i++) {
                    ItemActionButtons[i].gameObject.SetActive(false);
                }
            }
        }
        /// <summary>
        /// Receives input.
        /// </summary>
        /// <param name="value"></param>
        void OnItemSelect(object value) {
            OnSelection((int)value);
        }
        protected override void RefreshSelectionMarker(int i) {
            base.RefreshSelectionMarker(i);
            WeaponSlot.SetSelection(i == -1);
        }
    }
}