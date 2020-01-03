using System;
using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class ItemUI : MonoBehaviour, IIndividualUI<Item> {
        [SerializeField] Image Icon;
        [SerializeField] Text Hotkey;
        [SerializeField] Text Name;
        public event Action OnClick;
        Button b;
        protected Item item;
        protected virtual void Awake() {
            Icon.enabled = false;
            b = GetComponentInChildren<Button>();
            b.onClick.AddListener(() => OnClick?.Invoke());

        }
        public void Refresh(Item testIt) {
            item = testIt;

            if (item == null) {
                Clear();
                return;
            }

            Name.text = testIt.Data.Name;
            Icon.sprite = testIt.Data.Icon;
            Icon.color = new Color(1, 1, 1, 0.7f);
            Icon.enabled = true;
            Name.enabled = true;
        }
        public void Clear() {
            Icon.sprite = null;
            Name.enabled = false;
            Icon.enabled = false;
        }
        public void AssignHotkeyUI(string keypath) {

            Hotkey.text = keypath.Split('/')[1].Substring(0, 1).ToUpper();
        }
        public void SetVisible(bool b) {
            gameObject.SetActive(b);
        }
    }
}