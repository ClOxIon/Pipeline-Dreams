using UnityEngine;
using UnityEngine.InputSystem;

namespace PipelineDreams {
    public class InstructionCollectionUI : CollectionUI<Instruction> {
        [SerializeField] InstructionUI TemporaryUI;
        [SerializeField] InstructionContainer InstructionContainer;
        protected override void Awake() {
            _TemporarySlot = TemporaryUI as IIndividualUI<Instruction>;
            PI = InstructionContainer;
            base.Awake();


        }

        protected override void PI_OnRefreshUI(Instruction[] obj) {
            for (int i = ItemUIs.Count - 1; i >= obj.Length; i--) {
                ItemUIs[i].AssignHotkeyUI(FindObjectOfType<PlayerInput>().actions.FindAction("Instruction").bindings[i].path);
                ItemUIs[i].Clear();
            }
            for (int i = obj.Length - 1; i >= 0; i--) {
                ItemUIs[i].AssignHotkeyUI(FindObjectOfType<PlayerInput>().actions.FindAction("Instruction").bindings[i].path);
                ItemUIs[i].Refresh(obj[i]);
            }
        }
    }
}