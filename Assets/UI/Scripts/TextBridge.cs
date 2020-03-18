using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    [RequireComponent(typeof(Text))]
    public class TextBridge : MonoBehaviour {
        Text text;
        string value;
        private void Awake() {
            text = GetComponent<Text>();
            value = text.text;
        }
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if (text.text != value)
                GetComponent<Lean.Localization.LeanLocalizedText>().TranslationName = value;
            value = text.text;

        }
    }
}