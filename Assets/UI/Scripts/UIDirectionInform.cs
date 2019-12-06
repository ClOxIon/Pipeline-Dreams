using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDirectionInform : MonoBehaviour
{
    /// <summary>
    /// x+, x-, y+, y-, z+, z-
    /// </summary>
    [SerializeField] List<Sprite> Arrows;
    [SerializeField] Image LeftFrame;
    [SerializeField] Image RightFrame;
    [SerializeField] Image UpFrame;
    [SerializeField] Image DownFrame;
    [SerializeField] Text CenterText;
    [SerializeField] Image CenterBar;
    [SerializeField] Text CenterInfoText;
    [SerializeField] Text LeftText;
    [SerializeField] Text RightText;
    [SerializeField] Text UpText;
    [SerializeField] Text DownText;
    [SerializeField] Text LeftInfoText;
    [SerializeField] Text RightInfoText;
    [SerializeField] Text UpInfoText;
    [SerializeField] Text DownInfoText;
    string[] Directions = {"E", "W", "U", "D", "N", "S" };
    EntityManager EM;
    MapManager mManager;
    Quaternion rotation;
    private void Awake() {
        var CM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<PlayerMove>();
       

        var clock = CM.GetComponent<TaskManager>();
        clock.OnTaskEnd += UpdateUIInfo;
        
        EM = CM.GetComponent<EntityManager>();
        EM.OnPlayerInitPosition += UpdateUIInfo;
        mManager = CM.GetComponent<MapManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    void UpdateUIInfo() {
        var q = EM.Player.IdealRotation;
        CenterText.text = Directions[Util.LHQToFace(q)];
        var f = Vector3Int.RoundToInt(q * new Vector3(0, 0, 1));
        var entity = EM.FindEntityInLine(Util.LHUnitVectorToFace(f), EM.Player, SceneObjectManager.sightscale);
        if (entity == null|| mManager.GetTileRelative(Vector3Int.zero, Util.LHUnitVectorToFace(f), EM.Player)==Tile.wall) {
            CenterInfoText.text = "HEADING";
            CenterText.color = new Color(0, 1, 0, 170f / 255);
            CenterBar.color = new Color(0, 1, 0, 200f / 255);
            CenterInfoText.color = new Color(0, 1, 0, 240f / 255);
        } else {
            CenterInfoText.text = "ENEMY";
            CenterText.color = new Color(1, 0, 0, 170f / 255);
            CenterBar.color = new Color(1, 0, 0, 200f / 255);
            CenterInfoText.color = new Color(1, 0, 0, 240f / 255);

        }
        var e = Util.LHUnitVectorToFace(Vector3Int.RoundToInt(q * new Vector3(-1,0,0)));
        LeftFrame.sprite = Arrows[e];
        LeftText.text = Directions[e];
        var w = Util.LHUnitVectorToFace(Vector3Int.RoundToInt(q * new Vector3(1, 0, 0)));
        RightFrame.sprite = Arrows[w];
        RightText.text = Directions[w];
        var u = Util.LHUnitVectorToFace(Vector3Int.RoundToInt(q * new Vector3(0, 1, 0)));
        UpFrame.sprite = Arrows[u];
        UpText.text = Directions[u];
        var d = Util.LHUnitVectorToFace(Vector3Int.RoundToInt(q * new Vector3(0, -1, 0)));
        DownFrame.sprite = Arrows[d];
        DownText.text = Directions[d];
        rotation = q;
        UpdateInfoText(Vector3Int.RoundToInt(rotation * new Vector3(-1, 0, 0)), LeftInfoText, LeftText, LeftFrame);
        UpdateInfoText(Vector3Int.RoundToInt(rotation * new Vector3(1, 0, 0)), RightInfoText, RightText, RightFrame);
        UpdateInfoText(Vector3Int.RoundToInt(rotation * new Vector3(0, 1, 0)), UpInfoText, UpText, UpFrame);
        UpdateInfoText(Vector3Int.RoundToInt(rotation * new Vector3(0, -1, 0)), DownInfoText, DownText, DownFrame);


    }

    void UpdateInfoText(Vector3Int e, Text t, Text m, Image i) {
        var entity = EM.FindEntityInLine(Util.LHUnitVectorToFace(e), EM.Player, SceneObjectManager.sightscale);
        var wall = mManager.GetTileRelative(Vector3Int.zero, Util.LHUnitVectorToFace(e), EM.Player);
        if (wall == Tile.wall) {
            t.text = "BLOCKED"; t.color = new Color(0, 1, 0, 240f/255);
            m.color = new Color(0, 1, 0, 180f / 255);
            i.color = new Color(0, 1, 0, 180f / 255);
        } 
        else if (entity != null) {
            t.text = entity.Type.ToString();
            t.color = new Color(1, 0, 0, 240f / 255);
            m.color = new Color(1, 0, 0, 180f / 255);
            i.color = new Color(1, 0, 0, 180f / 255);
        } 
        else {
            t.text = "CLEAR";
            t.color = new Color(0, 1, 0, 240f / 255);
            m.color = new Color(0, 1, 0, 180f / 255);
            i.color = new Color(0, 1, 0, 180f / 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
