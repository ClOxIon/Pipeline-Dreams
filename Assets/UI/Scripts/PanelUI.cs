using System;
using UnityEngine;

namespace PipelineDreams {
    public class PanelUI : MonoBehaviour {
        bool visible;
        [SerializeField] GameObject Panel;
        public event Action<bool> OnVisibilityChange;
        public void HideDialogue() {

            visible = false;

            OnVisibilityChange?.Invoke(visible);
            Panel.SetActive(visible);
        }
        public void ShowDialogue() {

            visible = true;

            OnVisibilityChange?.Invoke(visible);
            Panel.SetActive(visible);

        }
    }
}