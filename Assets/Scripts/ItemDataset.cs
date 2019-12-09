using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "ItData", menuName = "ScriptableObjects/ItemData", order = 3)]
    public class ItemDataset : ScriptableObject {

        public List<ItemData> Dataset;

    }
    [System.Serializable]
    public class ItemData : Data {
        [NonSerialized] public string[] ItemActions;
        [NonSerialized] public string DefaultAction;

    }
    [System.Serializable]
    public struct Parameter {
        public string Name;
        public string Value;
    }
}