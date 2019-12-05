using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    PlayerController PC;
    [SerializeField] GameObject HUD;
    [SerializeField]DialogueUI DUI;
    [SerializeField] ItemPanelUI IUI;
    [SerializeField] InstructionPanelUI OUI;
    [SerializeField] MapPanelUI MUI;
    // Start is called before the first frame update
    void Start()
    {
        PC = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<PlayerController>();
        HUD.SetActive(true);
        IUI.HideDialogue();
        OUI.HideDialogue();
        MUI.HideDialogue();
        DUI.HideDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if ((PC.InputEnabled | (int)PlayerInputFlag.UIPANEL) == uint.MaxValue) 
        if (Input.GetKeyDown(KeyCode.F1)) {
                HUD.SetActive(true);
                IUI.HideDialogue();
                OUI.HideDialogue();
                MUI.HideDialogue();
                DUI.HideDialogue();
                PC.SetInputEnabled(PlayerInputFlag.UIPANEL, true);
            } else if (Input.GetKeyDown(KeyCode.F2)) {
                HUD.SetActive(false);
                IUI.ShowDialogue();
                OUI.HideDialogue();
                MUI.HideDialogue();
                DUI.HideDialogue();
                PC.SetInputEnabled(PlayerInputFlag.UIPANEL, false);
            } else if (Input.GetKeyDown(KeyCode.F3)) {
                HUD.SetActive(false);
                IUI.HideDialogue();
                OUI.ShowDialogue();
                MUI.HideDialogue();
                DUI.HideDialogue();
                PC.SetInputEnabled(PlayerInputFlag.UIPANEL,false);
            } else if (Input.GetKeyDown(KeyCode.F4)) {
                HUD.SetActive(false);
                IUI.HideDialogue();
                OUI.HideDialogue();
                MUI.ShowDialogue();
                DUI.HideDialogue();
                PC.SetInputEnabled(PlayerInputFlag.UIPANEL, false);
            } else if (Input.GetKeyDown(KeyCode.F5)) {
                HUD.SetActive(false);
                IUI.HideDialogue();
                OUI.HideDialogue();
                MUI.HideDialogue();
                DUI.ShowDialogue();
                PC.SetInputEnabled(PlayerInputFlag.UIPANEL, false);
            }
    }
}
