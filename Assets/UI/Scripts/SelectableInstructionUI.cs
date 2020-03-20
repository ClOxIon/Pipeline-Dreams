using UnityEngine;

namespace PipelineDreams {
    public class SelectableInstructionUI : InstructionUI, ISelectableIndividualUI<Instruction.Instruction> {

        [SerializeField] GameObject SelectionMarker;
        public void SetSelection(bool b) {
            SelectionMarker.SetActive(b && _operator != null);
        }
    }
}