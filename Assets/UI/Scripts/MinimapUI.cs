using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class MinimapUI : MonoBehaviour {
        bool Enlarged = false;
        [SerializeField] RenderTextureScaler MinimapRTS;
        [SerializeField] RawImage Minimap;
        [SerializeField] GameObject MinimapFrame;
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.Tab)) Enlarge();
        }
        void Enlarge() {
            if (!Enlarged) {
                Enlarged = true;
                Minimap.color = new Color(1, 1, 1, 0.4f);
                MinimapRTS.u = 0.6f;
                MinimapRTS.v = 0.6f;
                MinimapRTS.Refresh();
                Minimap.rectTransform.anchorMin = new Vector2(0.2f, 0.2f);
                Minimap.rectTransform.anchorMax = new Vector2(0.8f, 0.8f);
                MinimapFrame.SetActive(false);
            } else {
                Enlarged = false;
                Minimap.color = new Color(1, 1, 1, 1f);
                MinimapRTS.u = 0.3f;
                MinimapRTS.v = 0.3f;
                MinimapRTS.Refresh();
                Minimap.rectTransform.anchorMin = new Vector2(0.7f, 0.7f);
                Minimap.rectTransform.anchorMax = new Vector2(1f, 1f);
                MinimapFrame.SetActive(true);


            }

        }
    }
}