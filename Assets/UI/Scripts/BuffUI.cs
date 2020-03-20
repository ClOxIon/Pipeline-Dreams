using System;
using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class BuffUI : MonoBehaviour, IIndividualUI<Buff.Buff> {
        [SerializeField] Image Icon;

        Buff.Buff item;

        public event Action OnClick;

        private void Awake() {
            Icon.enabled = false;
        }
        public void Clear() {
            Icon.sprite = null;
            Icon.enabled = false;
        }

        public void AssignHotkeyUI(string keypath) {
            Debug.LogError("AssignHotkey Called at BuffUI.");
        }

        void IIndividualUI<Buff.Buff>.Refresh(Buff.Buff item) {

            if (item == null) {
                Clear();
                return;
            }
            Icon.sprite = item.Data.Icon;
            Icon.color = new Color(1, 1, 1, 0.7f);
            Icon.enabled = true;
            if (item is Buff.BuffWithDuration)
                Icon.fillAmount = ((Buff.BuffWithDuration)item).TimeLeft / (item.Data as Buff.Data).BaseDuration;
        }

        public void SetVisible(bool b) {
            Debug.LogError("SetVisible Called at BuffUI.");
        }
    }
}