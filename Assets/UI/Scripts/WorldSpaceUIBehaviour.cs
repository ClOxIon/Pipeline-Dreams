using System;
using UnityEngine;

namespace PipelineDreams {
    public class WorldSpaceUIBehaviour : MonoBehaviour {
        // Start is called before the first frame update
        [SerializeField] Canvas canvas;
        public event Action<bool> OnUIEnable;
        
        public void EnableUI(bool value) {
            if (value)
                canvas.worldCamera = Camera.main;
            else
                canvas.worldCamera = null;


            OnUIEnable?.Invoke(value);
        }

        // Update is called once per frame
        void Update() {

        }
        public void Alert() {
            Debug.Log("Button Pressed");
        }
    }
}