using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItData", menuName = "ScriptableObjects/ItemData", order = 3)]
public class ItemDataset : ScriptableObject {

    public List<ItemData> Dataset;

}
[System.Serializable]
public struct ItemData {
    public string Name;
    [TextArea(5, 10)]
    public string Description;    
    public Sprite Icon;
    public float value1;

    public float value2;

    public float value3;


}