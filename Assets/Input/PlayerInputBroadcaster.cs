using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
[Flags]
public enum PlayerInputFlag {
    NONE, SYSTEM, PLAYERMOVE, TASKMANAGER = 4, INFOUI = 8, PLAYERITEM = 16, OPERATORCONTAINER = 32, GAMEOVER = 64, UIPANEL = 128

}

public class PlayerInputBroadcaster : MonoBehaviour
{

    public event Action OnInputEnabledChange;


    [SerializeField] Text InputDisabledIndicator;
    List<GameObject> Subscribers = new List<GameObject>();
    PlayerInput PI;
    /// <summary>
    /// Input Flag.
    /// </summary>
    public uint InputEnabled { get; private set; } = uint.MaxValue;
    private void Awake() {
        PI = GetComponent<PlayerInput>();
        PI.onActionTriggered += PI_onActionTriggered;
    }
    public void SetPlayerInputEnabled(PlayerInputFlag flag, bool value) {
        InputEnabled = (InputEnabled & (uint.MaxValue - (uint)flag)) + (value ? ((uint)flag) : 0);
        InputDisabledIndicator.text = ((PlayerInputFlag)(uint.MaxValue - InputEnabled)).ToString();
        OnInputEnabledChange?.Invoke();
        if (InputEnabled == uint.MaxValue)
            PI.actions.FindActionMap("Player").Enable();
        else
            PI.actions.FindActionMap("Player").Disable();
    }
    public void SetInputEnabledDuringDialogue(bool value) {
        if(value)
            PI.actions.FindActionMap("UI").Enable();
        else
            PI.actions.FindActionMap("UI").Disable();
    }
    public void Subscribe(GameObject obj) { if(!Subscribers.Contains(obj))Subscribers.Add(obj); }
    public void UnSubscribe(GameObject obj) { Subscribers.Remove(obj); }

    private void PI_onActionTriggered(InputAction.CallbackContext obj) {
        if (obj.performed && obj.action.type == InputActionType.Button) {
            var i = Convert.ToInt32((float)obj.ReadValueAsObject());
            foreach (var x in Subscribers)
                x.SendMessage("On" + obj.action.name, i, SendMessageOptions.DontRequireReceiver);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //We are only using one PlayerInput, thus the action asset is unique.s
        foreach (var x in PI.actions.actionMaps)
            x.Enable();
        
    }
    
}
