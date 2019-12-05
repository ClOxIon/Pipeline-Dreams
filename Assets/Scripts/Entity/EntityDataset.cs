using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnData", menuName = "ScriptableObjects/EntityData", order = 2)]
public class EntityDataset : ScriptableObject {

    public List<EntityData> Dataset;

}
[System.Serializable]
public class EntityData : Data {
    
    public int MaxHP;
    public int Damage;
    public EntityType Type;
    public Entity Prefab;
}