﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class CollectionInfoUI<T> : MonoBehaviour, ICollectionInfoUI where T:PDObject {
        public event Action<int> OnItemSelected;
        [SerializeField] Text DescriptionText;
        [SerializeField] Text DirectionText;

        [SerializeField] Text AdditionalDescriptionText;
        List<ISelectableIndividualUI<T>> ItemUIs = new List<ISelectableIndividualUI<T>>();
        protected PDObjectContainer<T> IC;
        protected ObjectContainerUI<T> ICU;
        [SerializeField] Text TitleText;

        protected PDData SelectedItemData;
        protected int SelectedItemIndex;
        bool SwapMode = false;
        protected virtual void Awake() {
            ICU = GetComponent<ObjectContainerUI<T>>();
            SerializeItemUIs();
            OnItemSelected += RefreshSelectionMarker;
            FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
            GetComponentInParent<PanelUI>().OnVisibilityChange += (b) => { if (!b) StopSwap(); };
        }
        private void SerializeItemUIs() {

            foreach (var x in GetComponentsInChildren<MonoBehaviour>().ToList()) {
                if (x is ISelectableIndividualUI<T> l)
                    ItemUIs.Add(l);
            }
        }
        private void Start() {
            OnSelection(0);
        }
        protected virtual void RefreshInfo() {
            if (SelectedItemData == null) {
                DescriptionText.text = "";
                AdditionalDescriptionText.text = "";
                TitleText.text = "Please Select an Item to Examine It.";
            } else {

                DescriptionText.text = SelectedItemData.Description;
                TitleText.text = SelectedItemData.Name;
            }
        }
        protected virtual void RefreshSelectionMarker(int i) {
            for (int j = 0; j < ItemUIs.Count; j++)
                ItemUIs[j].SetSelection(i == j);


        }

        protected void OnSelection(int i) {
            
            if (SwapMode) {
                StopSwap();
                IC.SwapItem(SelectedItemIndex, i);
            }
            SelectedItemData = IC.GetItemInfo(i);

            SelectedItemIndex = i;

            OnItemSelected?.Invoke(i);

            RefreshInfo();
        }

        public void OnItemSwap() {
            SwapMode = true;
            DirectionText.text = "SELECT A SLOT TO SWAP";
        }
        private void StopSwap() {
            SwapMode = false;
            DirectionText.text = "";
        }

    }
    public interface ICollectionInfoUI {
        void OnItemSwap();
    }
}