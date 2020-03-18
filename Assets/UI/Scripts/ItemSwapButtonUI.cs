using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class ItemSwapButtonUI : MonoBehaviour {
        [SerializeField] Text Hotkey;
        Button b;
        private void Awake() {
            b = GetComponent<Button>();
            b.onClick.AddListener((FindObjectsOfType<MonoBehaviour>().First((x) => x is ICollectionInfoUI) as ICollectionInfoUI).OnItemSwap);
        }

        public void AssignHotkeyUI(string keypath) {

            Hotkey.text = keypath.Split('/')[1].Substring(0, 1);
        }
    }
}