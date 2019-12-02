using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[Flags] public enum PlayerInputFlag {
    NONE,SYSTEM, PLAYERCONTROLLER,CLOCKMANAGER=4, INFOUI=8, PLAYERITEM = 16, OPERATORCONTAINER = 32, UIPANEL = 64, GAMEOVER = 128

}
public class PlayerController : MonoBehaviour {
    [SerializeField] Text InputDisabledIndicator;
    /// <summary>
    /// LH vector
    /// </summary>
    public event Action<Vector3Int> OnPlayerTranslation;
    public event Action OnAfterPlayerTranslation;

    /// <summary>
    /// LHQ of the camera.
    /// </summary>
    public event Action<Quaternion> OnPlayerRotation;


    /// <summary>
    /// Input Flag.
    /// </summary>
    public uint InputEnabled { get; private set; } = uint.MaxValue;
    /// <summary>
    /// l,u,r,d, space
    /// </summary>
    public event Action<Command> OnCommandKeyPressed;
    public event Action<int> OnOperatorKeyPressed;
    MapManager mManager;
    ClockManager CM;
    EntityManager EM;
    Entity Player;




    private void Awake() {
        EM = GetComponent<EntityManager>();
        mManager = GetComponent<MapManager>();
        CM = GetComponent<ClockManager>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();

    }
    // Start is called before the first frame update
    void Start() {
    }
    

    void PlayerRotate(Quaternion deltaQ) {

        EM.Player.GetComponent<EntityMove>().Face(Util.LHQToFace(Player.IdealRotation * deltaQ), CM.Clock);
        Debug.Log(Util.LHQToFace(Player.IdealRotation * deltaQ));
        Debug.Log(Util.LHQToLHUnitVector(Player.IdealRotation * deltaQ));
    }

    void PlayerTranslateForward() {
        EM.Player.GetComponent<EntityMove>().MoveToward(Util.LHQToLHUnitVector(EM.Player.IdealRotation), CM.Clock);

    }

    public void SetInputEnabled(PlayerInputFlag flag, bool value) {
        InputEnabled = (InputEnabled & (uint.MaxValue - (uint)flag)) + (value?((uint)flag):0);
        InputDisabledIndicator.text = ((PlayerInputFlag)(uint.MaxValue - InputEnabled)).ToString();
        if (InputEnabled == uint.MaxValue)
            GetComponent<PlayerInput>().enabled = true;
        else
            GetComponent<PlayerInput>().enabled = false;
    }
    
    /// <summary>
    /// These are using message sent by PlayerInput.
    /// </summary>
    /// <param name="value"></param>
    private void OnInstruction1(InputValue value){
        OnOperatorKeyPressed(0);
    }
    private void OnInstruction2(InputValue value) {
        OnOperatorKeyPressed(1);
    }
    private void OnInstruction3(InputValue value) {
        OnOperatorKeyPressed(2);
    }
    private void OnInstruction4(InputValue value) {
        OnOperatorKeyPressed(3);
    }
    private void OnInstruction5(InputValue value) {
        OnOperatorKeyPressed(4);
    }
    private void OnInstruction6(InputValue value) {
        OnOperatorKeyPressed(5);
    }
    private void OnInstruction7(InputValue value) {
        OnOperatorKeyPressed(6);
    }
    private void OnInstruction8(InputValue value) {
        OnOperatorKeyPressed(7);
    }
    private void OnMoveForward(InputValue value) {
        if (Player.GetComponent<EntityMove>().CanMove(Util.LHQToLHUnitVector(Player.IdealRotation))) {

            PlayerTranslateForward();
            OnCommandKeyPressed(Command.space);
        }
    }
    private void OnTurnLeft(InputValue value) {
        if (Player.GetComponent<EntityMove>().CanRotate(Player.IdealRotation * Util.TurnLeft)) {
            PlayerRotate(Util.TurnLeft);
            OnCommandKeyPressed(Command.left);
        }
    }
    private void OnTurnRight(InputValue value) {
        if (Player.GetComponent<EntityMove>().CanRotate(Player.IdealRotation * Util.TurnRight)) {
            PlayerRotate(Util.TurnRight);
            OnCommandKeyPressed(Command.right);
        }
    }
    private void OnTurnUp(InputValue value) {
        if (Player.GetComponent<EntityMove>().CanRotate(Player.IdealRotation * Util.TurnUp)) {
            PlayerRotate(Util.TurnUp);
            OnCommandKeyPressed(Command.up);
        }
    }
    private void OnTurnDown(InputValue value) {
        if (Player.GetComponent<EntityMove>().CanRotate(Player.IdealRotation * Util.TurnDown)) {
            PlayerRotate(Util.TurnDown);
            OnCommandKeyPressed(Command.down);
        }
    }
}
