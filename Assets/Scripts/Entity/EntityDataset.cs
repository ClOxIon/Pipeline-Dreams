using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "EnData", menuName = "ScriptableObjects/EntityData", order = 2)]
    public class EntityDataset : ScriptableObject, IPDDataSet {

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
        [SerializeField] private List<EntityData> dataSet;

    }
    [System.Serializable]
    public class EntityData : PDData {

        private int maxHP;
        private int damage;
        private EntityType type;
        private Entity prefab;

        public int MaxHP { get => maxHP; set => maxHP = value; }
        public EntityType Type { get => type; set => type = value; }
        public Entity Prefab { get => prefab; set => prefab = value; }
    }
}