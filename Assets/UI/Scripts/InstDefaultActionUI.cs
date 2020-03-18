using UnityEngine;
namespace PipelineDreams {
    public class InstDefaultActionUI : MonoBehaviour {
        [SerializeField] InstructionContainerPlayer IC;
        InstructionCollectionUI ICU;
        private void Awake() {
            FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
            ICU = GetComponent<InstructionCollectionUI>();

        }
        // Start is called before the first frame update
        void Start() {

        }

        void OnInstruction(object value) {
            var i = (int)value;
            IC.UseInstructionAt(i);


        }
    }
}
