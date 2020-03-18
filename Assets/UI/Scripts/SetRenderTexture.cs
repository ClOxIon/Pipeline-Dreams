using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class SetRenderTexture : MonoBehaviour {
        [SerializeField] RenderTextureScaler RTS;
        private void Awake() {

        }

        // Start is called before the first frame update
        void Start() {
            GetComponent<RawImage>().texture = RTS.Target;
            RTS.OnResize += () => { GetComponent<RawImage>().texture = RTS.Target; };
        }

        // Update is called once per frame
        void Update() {

        }
    }
}