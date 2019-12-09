using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PipelineDreams {
    public abstract class CollectionUI<T> : MonoBehaviour {
        protected List<IIndividualUI<T>> ItemUIs = new List<IIndividualUI<T>>();
        protected IDataContainer<T> PI;
        protected IIndividualUI<T> _TemporarySlot;
        public event Action<int> OnItemUIClick;
        protected virtual void Awake() {
            SerializeItemUIs();
            ItemUIs.Remove(_TemporarySlot);
            ItemUIs.Insert(0, _TemporarySlot);
            for (int i = 0; i < ItemUIs.Count; i++)
                ItemUIs[i].OnClick += () => OnItemUIClick?.Invoke(i);
            PI.OnRefreshItems += PI_OnRefreshUI;
            PI.OnChangeItemSlotAvailability += PI_OnRefreshItemSlotUI;
        }
        private void SerializeItemUIs() {

            foreach (var x in GetComponentsInChildren<MonoBehaviour>().ToList()) {
                if (x is IIndividualUI<T> l)
                    ItemUIs.Add(l);
            }
        }
        private void Start() {

            PI.InvokeUIRefresh();
        }

        private void PI_OnRefreshItemSlotUI(int num) {
            for (int i = ItemUIs.Count - 1; i >= 0; i--) {
                ItemUIs[i].SetVisible(i < num);
            }
        }

        protected abstract void PI_OnRefreshUI(T[] obj);


    }
}