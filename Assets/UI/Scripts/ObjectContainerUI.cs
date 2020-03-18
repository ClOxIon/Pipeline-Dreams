using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PipelineDreams
{
    public abstract class ObjectContainerUI<T> : MonoBehaviour where T: PDObject{
        protected List<IIndividualUI<T>> ItemUIs = new List<IIndividualUI<T>>();
        protected PDObjectContainer<T> PI;
        public event Action<int> OnItemUIClick;
        protected virtual void Awake() {
            FindItemUIs();
            for (int i = 0; i < ItemUIs.Count; i++)
                ItemUIs[i].OnClick += () => OnItemUIClick?.Invoke(i);
            PI.OnRefreshItems += PI_OnRefreshUI;
        }
        private void FindItemUIs() {

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