using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    [SerializeField]GameObject InfoPanel;
    [SerializeField]GameObject CenterPanel;
    [SerializeField] Text DescriptionText;
    [SerializeField] Text TitleText;
    [SerializeField] Image Icon;
    
    bool visible = false;
    // Start is called before the first frame update
    PlayerController PC;
    EntityManager EM;
    PlayerItem PI;
    OperatorContainer OC;
    MapManager mManager;
    private void Awake() {
        PC = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<PlayerController>();
        EM = PC.GetComponent<EntityManager>();
        PI = GameObject.FindGameObjectWithTag("ItemPanel").GetComponent<PlayerItem>();
        OC = GameObject.FindGameObjectWithTag("OperatorRack").GetComponent<OperatorContainer>();
        mManager = PC.GetComponent<MapManager>();
    }
    void Start()
    {
        
    }

    private void ShowInfo() {
        visible = !visible;
        InfoPanel.SetActive(visible);
        CenterPanel.SetActive(!visible);
        if (visible) PC.DisableInput(PlayerInputFlag.INFOUI); else PC.EnableInput(PlayerInputFlag.INFOUI);
    }
    private void ShowEntityInfo(EntityData data) {
        DescriptionText.text = data.Description;
        TitleText.text = data.Name;
        Icon.sprite = data.Icon;
        Icon.enabled = true;
    }
    private void ShowOperatorInfo(OperatorData data) {
        DescriptionText.text = data.Description;
        TitleText.text = data.Name;
        Icon.sprite = data.Icon;
        Icon.enabled = true;
    }
    private void ShowItemInfo(ItemData data) {
        DescriptionText.text = data.Description;
        TitleText.text = data.Name;
        Icon.sprite = data.Icon;
        Icon.enabled = true;
    }
    private void ShowTileInfo(TileData data) {
        DescriptionText.text = data.Description;
        TitleText.text = data.Name;
        Icon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)&&(PC.InputEnabled|(int)PlayerInputFlag.INFOUI)==uint.MaxValue) { ShowInfo();
            var e = EM.FindEntityInLine(Util.LHQToFace(EM.Player.IdealRotation), EM.Player);
            if (e != null)
                ShowEntityInfo(e.Data);
            else {
                var t = mManager.GetTileRelative(Vector3Int.zero, Util.LHQToFace(EM.Player.IdealRotation), EM.Player);
                ShowTileInfo(mManager.Dataset.Dataset.Find((x) => x.Type == t));
            }
            
        }
        if (visible) {
            if(Input.GetKeyDown(KeyCode.Space))
            { var e= EM.FindEntityInLine(Util.LHQToFace(EM.Player.IdealRotation), EM.Player);
                if (e != null)
                    ShowEntityInfo(e.Data);
                else {
                    var t = mManager.GetTileRelative(Vector3Int.zero, Util.LHQToFace(EM.Player.IdealRotation), EM.Player);
                    ShowTileInfo(mManager.Dataset.Dataset.Find((x) => x.Type == t));
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                var e = EM.FindEntityInLine(Util.LHQToFace(EM.Player.IdealRotation*Util.TurnUp), EM.Player);
                if (e != null)
                    ShowEntityInfo(e.Data);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                var e = EM.FindEntityInLine(Util.LHQToFace(EM.Player.IdealRotation * Util.TurnDown), EM.Player);
                if (e != null)
                    ShowEntityInfo(e.Data);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                var e = EM.FindEntityInLine(Util.LHQToFace(EM.Player.IdealRotation * Util.TurnLeft), EM.Player);
                if (e != null)
                    ShowEntityInfo(e.Data);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                var e = EM.FindEntityInLine(Util.LHQToFace(EM.Player.IdealRotation * Util.TurnRight), EM.Player);
                if (e != null)
                    ShowEntityInfo(e.Data);
            }
            if (Input.GetKeyDown(KeyCode.Q)) {
                var o = OC.Operators[0];
                if (o != null)
                    ShowOperatorInfo(o.OpData);
            }
            if (Input.GetKeyDown(KeyCode.A)) {
                var o = OC.Operators[1];
                if (o != null)
                    ShowOperatorInfo(o.OpData);
            }
            if (Input.GetKeyDown(KeyCode.Z)) {
                var o = OC.Operators[2];
                if (o != null)
                    ShowOperatorInfo(o.OpData);
            }
            if (Input.GetKeyDown(KeyCode.W)) {
                var o = OC.Operators[3];
                if (o != null)
                    ShowOperatorInfo(o.OpData);
            }
            if (Input.GetKeyDown(KeyCode.S)) {
                var o = OC.Operators[4];
                if (o != null)
                    ShowOperatorInfo(o.OpData);
            }
            if (Input.GetKeyDown(KeyCode.X)) {
                var o = OC.Operators[5];
                if (o != null)
                    ShowOperatorInfo(o.OpData);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                var i = PI.Items[0];
                if (i != null)
                    ShowItemInfo(i.ItData);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                var i = PI.Items[1];
                if (i != null)
                    ShowItemInfo(i.ItData);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                var i = PI.Items[2];
                if (i != null)
                    ShowItemInfo(i.ItData);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4)) {
                var i = PI.Items[3];
                if (i != null)
                    ShowItemInfo(i.ItData);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5)) {
                var i = PI.Items[4];
                if (i != null)
                    ShowItemInfo(i.ItData);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6)) {
                var i = PI.Items[5];
                if (i != null)
                    ShowItemInfo(i.ItData);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7)) {
                var i = PI.Items[6];
                if (i != null)
                    ShowItemInfo(i.ItData);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8)) {
                var i = PI.Items[7];
                if (i != null)
                    ShowItemInfo(i.ItData);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9)) {
                var i = PI.Items[8];
                if (i != null)
                    ShowItemInfo(i.ItData);
            }
        }
    }
}
