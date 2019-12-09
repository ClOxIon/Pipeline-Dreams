using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "BuData", menuName = "ScriptableObjects/BuffData", order = 4)]
    public class BuffDataset : ScriptableObject {

        public List<BuffData> Dataset;

    }
    [System.Serializable]
    public class BuffData : Data {
        public float baseDuration;


    }
}