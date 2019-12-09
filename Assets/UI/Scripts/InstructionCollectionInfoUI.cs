using UnityEngine;

namespace PipelineDreams {
    public class InstructionCollectionInfoUI : CollectionInfoUI<Instruction> {
        [SerializeField]InstructionContainer InstructionContainer;
        protected override void Awake() {
            base.Awake();

            IC = InstructionContainer;

        }
        void OnInstructionSelect(object value) {
            OnSelection((int)value);
        }
    }
}