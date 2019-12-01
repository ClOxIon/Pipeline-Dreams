using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EntityType {
PLAYER,ENEMY,NPC
}
public class Entity : MonoBehaviour
{
    MapManager mManager;

    /// <summary>
    /// Do not modify this value through script.
    /// </summary>
    [SerializeField] public EntityType Type;
    public event Action OnInit;
    public Vector3Int IdealPosition;
    public Quaternion IdealRotation;
    public EntityData Data { get; private set; }
    public bool IsActive = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="InitPosition">RH VectorInt</param>
    /// <param name="InitQ">RH Quaternion</param>
    public void Initialize(Vector3Int InitPosition, Quaternion InitQ, EntityData data) {
        Type = data.Type;
        IdealPosition = InitPosition;
        IdealRotation = InitQ;
        Data = data;
        IsActive = true;
        transform.localPosition = SceneObjectManager.worldscale*(Vector3)IdealPosition ;
        transform.localRotation = InitQ;
        
        OnInit?.Invoke();
    }
    private void Awake() {
       
        //IdealPosition = Vector3Int.RoundToInt(transform.localPosition/SceneObjectManager.worldscale);
        //IdealRotation = transform.localRotation;
        mManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MapManager>();
        
    }
   

}
