using UnityEngine;

namespace PipelineDreams {
    public class InstructionCollectionInfoUI : CollectionInfoUI<Instruction.Instruction> {
        [SerializeField]Instruction.ContainerPlayer InstructionContainer;
        protected override void Awake() {
            base.Awake();

            IC = InstructionContainer;

        }
        /// <summary>
        /// Receives input
        /// </summary>
        /// <param name="value"></param>
        void OnInstructionSelect(object value) {
            OnSelection((int)value);
        }
    }
}