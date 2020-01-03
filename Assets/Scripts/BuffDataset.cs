using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "BuData", menuName = "ScriptableObjects/BuffData", order = 4)]
    public class BuffDataset : ScriptableObject, IPDDataSet
    {
        public List<PDData> DataSet
        {
            get
            {
                var d = new List<PDData>();
                foreach (var x in dataSet)
                    d.Add(x);
                return d;
            }
        }

        [SerializeField] private List<BuffData> dataSet;
    }
    [System.Serializable]
    public class BuffData : PDData {
        [SerializeField]private float baseDuration;

        public float BaseDuration { get => baseDuration; set => baseDuration = value; }
    }
}