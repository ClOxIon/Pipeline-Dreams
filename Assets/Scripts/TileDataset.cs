using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TiData", menuName = "ScriptableObjects/TileData", order = 4)]
public class TileDataset : ScriptableObject {

    public List<TileData> Dataset;

}
[System.Serializable]
public struct TileData {
    public string Name;
    public Tile Type;
    [TextArea(5, 10)]
    public string Description;
    public bool HasDialogue;
    public int Value1;
    public int Value2;

    public SceneObject Prefab;

}