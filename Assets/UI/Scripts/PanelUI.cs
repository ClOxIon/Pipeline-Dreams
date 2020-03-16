using System;
using UnityEngine;

namespace PipelineDreams {
    public class PanelUI : MonoBehaviour {
        public bool visible { get; private set; }
        [SerializeField] GameObject Panel;
        public event Action<bool> OnVisibilityChange;
        public void HidePanel() {

            visible = false;

            //Beware that the event is called before the deactivation.
            OnVisibilityChange?.Invoke(visible);
            Panel.SetActive(visible);

        }
        public void ShowPanel() {

            visible = true;

            Panel.SetActive(visible);

            OnVisibilityChange?.Invoke(visible);

        }
    }
}