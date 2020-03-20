using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PipelineDreams.Buff {
    [CreateAssetMenu(fileName = "BuData", menuName = "ScriptableObjects/BuffData", order = 4)]
    public class Dataset : ScriptableObject, IPDDataSet
    {
        public List<PDData> DataSet => (from x in dataSet
                                        select (PDData)x).ToList();

        [SerializeField] private List<Data> dataSet;
    }
    [System.Serializable]
    public class Data : PDData {
        [SerializeField]private float baseDuration;

        public float BaseDuration { get => baseDuration; set => baseDuration = value; }
    }
}