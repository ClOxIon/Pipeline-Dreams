using System;
using UnityEngine;

namespace PipelineDreams {
    public class CameraRootAnimEvents : MonoBehaviour {
        public event Action OnCameraWarp;
        
        public void InvokeOnCameraWarp() {
            OnCameraWarp();
        }
        
    }
}