using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstructionCollectionUI : MonoBehaviour
{
    List<InstructionUI> InstUIs;
    InstructionCollection PI;
    private void Awake() {
        PI = (InstructionCollection)FindObjectOfType(typeof(InstructionCollection));
        InstUIs = new List<InstructionUI>(GetComponentsInChildren<InstructionUI>());
        PI.OnRefreshItems += PI_OnRefreshUI;
    }


    private void PI_OnRefreshUI(Instruction[] obj) {

        for (int i = InstUIs.Count - 1; i >= obj.Length; i--) {
            
            InstUIs[i].gameObject.SetActive(false);
        }
        for (int i = obj.Length - 1; i >= 0; i--) {
            InstUIs[i].AssignHotkeyUI(FindObjectOfType<PlayerInput>().actions.FindAction("Instruction").bindings[i].path);
            InstUIs[i].gameObject.SetActive(true);
            InstUIs[i].Refresh(obj[i]);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
