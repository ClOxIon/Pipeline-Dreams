using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnData", menuName = "ScriptableObjects/EntityData", order = 2)]
public class EntityDataset : ScriptableObject {

    public List<EntityData> Dataset;

}
[System.Serializable]
public struct EntityData {
    public string Name;
    [TextArea(5,10)]
    public string Description;
    public int MaxHP;
    public int Damage;
    public int Value1;
    public int Value2;
    public EntityType Type;
    public bool HasDialogue;
    public Sprite Icon;
    public Entity Prefab;
    public string loot;
    public float lootChance;
}