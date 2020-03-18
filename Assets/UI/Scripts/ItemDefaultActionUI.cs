using UnityEngine;

namespace PipelineDreams {
    public class ItemDefaultActionUI : MonoBehaviour {
        [SerializeField] ItemContainerPlayer IC;
        ItemCollectionUI ICU;
        private void Awake() {
            FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
            ICU = GetComponent<ItemCollectionUI>();
            ICU.OnItemUIClick += (x) => OnItemUse(x);
        }
        // Start is called before the first frame update
        void Start() {

        }

        void OnItemUse(object value) {
            var i = (int)value;
            IC.InvokeItemAction(i, "DEFAULT");


        }
    }
}