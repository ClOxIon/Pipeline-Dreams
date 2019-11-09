using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[Flags] public enum PlayerInputFlag {
    NONE,SYSTEM, PLAYERCONTROLLER,CLOCKMANAGER=4, INFOUI=8, PLAYERITEM = 16, OPERATORCONTAINER = 32, UIPANEL = 64

}
public class PlayerController : MonoBehaviour
{
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
    public bool PlayerDead = false;
    /// <summary>
    /// l,u,r,d, space
    /// </summary>
    public event Action<Command> OnCommandKeyPressed;
    [Range(0,10)][SerializeField] float RotationSpeed;
    [Range(0, 10)] [SerializeField] float Speed;
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
    void Start()
    {

        
        //CameraBody.GetComponentInParent<CameraRootAnimEvents>().OnCameraWarp+= ()=>OnPlayerTranslation(Vector3Int.RoundToInt(IdealCameraQ * AlgebraUtil.ToLHChart(new Vector3(1, 0, 0)))); 
    }
    /*
    public GameObject GetCameraRootObject() {
        return CameraBody;
    }
    */


    void PlayerRotate(Quaternion deltaQ) {
        
        EM.Player.GetComponent<EntityMove>().Face(Util.LHQToFace(Player.IdealRotation * deltaQ),CM.Clock);

    }
    
    void PlayerTranslateForward() {

        
        EM.Player.GetComponent<EntityMove>().MoveToward(Util.LHQToLHUnitVector(EM.Player.IdealRotation), CM.Clock);
        
    }

    public void EnableInput(PlayerInputFlag flag) {
        InputEnabled = (InputEnabled & (uint.MaxValue-(uint)flag))+ (uint)flag;
        InputDisabledIndicator.text = ((PlayerInputFlag)(uint.MaxValue - InputEnabled)).ToString();
    }

    public void DisableInput(PlayerInputFlag flag) {
        InputEnabled = (InputEnabled & (uint.MaxValue - (uint)flag));
        InputDisabledIndicator.text = ((PlayerInputFlag)(uint.MaxValue - InputEnabled)).ToString();
    }
    // Update is called once per frame
    /// <summary>
    /// All In-Game Input should be given here.
    /// </summary>
    void Update() {
        if (InputEnabled==uint.MaxValue&&!PlayerDead) {
            if (Input.GetKeyDown(KeyCode.Space)) {


                if (Player.GetComponent<EntityMove>().CanMove(Util.LHQToLHUnitVector(Player.IdealRotation))) {

                    PlayerTranslateForward();
                    OnCommandKeyPressed(Command.space);
                }
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                if (Player.GetComponent<EntityMove>().CanRotate(Player.IdealRotation * Util.TurnDown)) {
                    PlayerRotate(Util.TurnDown);
                    OnCommandKeyPressed(Command.down);
                }

            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                if (Player.GetComponent<EntityMove>().CanRotate(Player.IdealRotation * Util.TurnUp)) {
                    PlayerRotate(Util.TurnUp);
                    OnCommandKeyPressed(Command.up);
                }
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if (Player.GetComponent<EntityMove>().CanRotate(Player.IdealRotation * Util.TurnRight)) {
                    PlayerRotate(Util.TurnRight);
                    OnCommandKeyPressed(Command.right);
                }
            } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (Player.GetComponent<EntityMove>().CanRotate(Player.IdealRotation * Util.TurnLeft)) {
                    PlayerRotate(Util.TurnLeft);
                    OnCommandKeyPressed(Command.left);
                }
            } else if (Input.GetKeyDown(KeyCode.Q)) {
                OnOperatorKeyPressed(0);
            } else if (Input.GetKeyDown(KeyCode.A)) {
                OnOperatorKeyPressed(1);
            } else if (Input.GetKeyDown(KeyCode.Z)) {
                OnOperatorKeyPressed(2);
            } else if (Input.GetKeyDown(KeyCode.W)) {
                OnOperatorKeyPressed(3);
            } else if (Input.GetKeyDown(KeyCode.S)) {
                OnOperatorKeyPressed(4);
            } else if (Input.GetKeyDown(KeyCode.X)) {
                OnOperatorKeyPressed(5);
            }


        }
       
    }

    IEnumerator Translation(Vector3Int v) {
       
        Player.IdealPosition += v;
        OnPlayerTranslation(Vector3Int.RoundToInt(Player.IdealRotation * Vector3.forward));
        var v0 = Player.transform.position;
        var v1 = (Vector3)Player.IdealPosition * SceneObjectManager.worldscale;
        float ratio = 0;
        while(ratio<1) {
            Player.transform.position = Vector3.Lerp(v0, v1, ratio);

            ratio += Speed* Time.deltaTime;
            yield return null;
        }
        Player.transform.position = v1;
        OnAfterPlayerTranslation();
        CM.AddTime(100);
        
    }
    IEnumerator Rotation(Quaternion q) {
       
        Player.IdealRotation = Player.IdealRotation * q;
        OnPlayerRotation(Player.IdealRotation);
        var q0 = Player.transform.rotation;
        var q1 = Player.IdealRotation;
        float ratio = 0;
        while (ratio < 1) {
            Player.transform.rotation = Quaternion.Slerp(q0, q1, ratio);

            ratio += RotationSpeed * Time.deltaTime;
            yield return null;
        }
        Player.transform.rotation = q1;
        
        CM.AddTime(50);
    }


}
