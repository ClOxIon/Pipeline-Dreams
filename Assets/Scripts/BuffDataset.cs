using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "BuData", menuName = "ScriptableObjects/BuffData", order = 4)]
    public class BuffDataset : ScriptableObject, IPDDataSet
    {
        public List<PDData> DataSet => (from x in dataSet
                                        select (PDData)x).ToList();

        [SerializeField] private List<BuffData> dataSet;
    }
    [System.Serializable]
    public class BuffData : PDData {
        [SerializeField]private float baseDuration;

        public float BaseDuration { get => baseDuration; set => baseDuration = value; }
    }
}