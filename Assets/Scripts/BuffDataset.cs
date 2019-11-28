using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BuData", menuName = "ScriptableObjects/BuffData", order = 4)]
public class BuffDataset : ScriptableObject {

    public List<BuffData> Dataset;

}
[System.Serializable]
public struct BuffData {
    public string Name;
    [TextArea(5, 10)]
    public string Description;
    public Sprite Icon;
    public float baseDuration;


}