using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIPanelController : MonoBehaviour
{
    [SerializeField] GameObject HUD;
    [SerializeField]DialogueUI DUI;
    [SerializeField] ItemPanelUI IUI;
    [SerializeField] InstructionPanelUI OUI;
    [SerializeField] MapPanelUI MUI;
    PlayerInput PI;
    PlayerInputBroadcaster PC;
    private void Awake() {
        PC = FindObjectOfType<PlayerInputBroadcaster>();
        PC.Subscribe(gameObject);
       
        PI = GetComponent<PlayerInput>();
        
    }
    // Start is called before the first frame update
    void Start()
    {


        //We sequancially activate all UI panels to initialize them.
        IUI.ShowDialogue();
        OUI.ShowDialogue();
        DUI.ShowDialogue();
        MUI.ShowDialogue();
        OnHUD();
    }

    
    void OnHUD() {
        HUD.SetActive(true);
        IUI.HideDialogue();
        OUI.HideDialogue();
        MUI.HideDialogue();
        DUI.HideDialogue();
        PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, true);
    }
    void OnItemMenu() {
        HUD.SetActive(false);
        IUI.ShowDialogue();
        OUI.HideDialogue();
        MUI.HideDialogue();
        DUI.HideDialogue();
        PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, false);
    }
    void OnInstructionMenu() {
        HUD.SetActive(false);
        IUI.HideDialogue();
        OUI.ShowDialogue();
        MUI.HideDialogue();
        DUI.HideDialogue();
        PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, false);
    }
    void OnMinimap() {
        HUD.SetActive(false);
        IUI.HideDialogue();
        OUI.HideDialogue();
        MUI.ShowDialogue();
        DUI.HideDialogue();
        PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, false);
    }
    void OnInteraction() {
        HUD.SetActive(false);
        IUI.HideDialogue();
        OUI.HideDialogue();
        MUI.HideDialogue();
        DUI.ShowDialogue();
        PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, false);
    }
}
