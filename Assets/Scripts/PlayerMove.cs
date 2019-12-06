using System;
using UnityEngine;


public class PlayerMove : MonoBehaviour {
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
    /// l,u,r,d, space
    /// </summary>
    public event Action<Command> OnCommandKeyPressed;
    MapManager mManager;
    ClockManager CM;
    EntityManager EM;
    Entity Player;




    private void Awake() {
        EM = GetComponent<EntityManager>();
        mManager = GetComponent<MapManager>();
        CM = GetComponent<ClockManager>();
        FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();

    }
    // Start is called before the first frame update
    void Start() {
    }
    

    void PlayerRotate(Quaternion deltaQ) {

        EM.Player.GetComponent<EntityMove>().Face(Util.LHQToFace(Player.IdealRotation * deltaQ), CM.Clock);

    }

    void PlayerTranslateForward() {
        EM.Player.GetComponent<EntityMove>().MoveToward(Util.LHQToLHUnitVector(EM.Player.IdealRotation), CM.Clock);

    }

    
    
    /// <summary>
    /// These are using message sent by PlayerInput.
    /// </summary>
    /// <param name="value"></param>
    private void OnMoveForward(object value) {
        if (Player.GetComponent<EntityMove>().CanMove(Util.LHQToLHUnitVector(Player.IdealRotation))) {

            PlayerTranslateForward();
            OnCommandKeyPressed(Command.space);
        }
    }
    private void OnTurnLeft(object value) {
        if (Player.GetComponent<EntityMove>().CanRotate(Player.IdealRotation * Util.TurnLeft)) {
            PlayerRotate(Util.TurnLeft);
            OnCommandKeyPressed(Command.left);
        }
    }
    private void OnTurnRight(object value) {
        if (Player.GetComponent<EntityMove>().CanRotate(Player.IdealRotation * Util.TurnRight)) {
            PlayerRotate(Util.TurnRight);
            OnCommandKeyPressed(Command.right);
        }
    }
    private void OnTurnUp(object value) {
        if (Player.GetComponent<EntityMove>().CanRotate(Player.IdealRotation * Util.TurnUp)) {
            PlayerRotate(Util.TurnUp);
            OnCommandKeyPressed(Command.up);
        }
    }
    private void OnTurnDown(object value) {
        if (Player.GetComponent<EntityMove>().CanRotate(Player.IdealRotation * Util.TurnDown)) {
            PlayerRotate(Util.TurnDown);
            OnCommandKeyPressed(Command.down);
        }
    }
}
