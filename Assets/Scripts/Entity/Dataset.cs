using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PipelineDreams.Entity {
    [CreateAssetMenu(fileName = "EnData", menuName = "ScriptableObjects/EntityData", order = 2)]
    public class Dataset : ScriptableObject, IPDDataSet {

        public List<PDData> DataSet => (from x in dataSet
                                        select (PDData)x).ToList();
        [SerializeField] private List<Data> dataSet;

    }
    [System.Serializable]
    public class Data : PDData {

        [SerializeField] private int maxHP;
        [SerializeField] private int damage;
        [SerializeField] private EntityType type;
        [SerializeField] private bool occupySpace;
        [SerializeField] private YarnProgram dialogue;

        [SerializeField]private Entity prefab;

        public int MaxHP { get => maxHP; }
        public EntityType Type { get => type; }
        public Entity Prefab { get => prefab; }
        public YarnProgram Dialogue { get => dialogue; }
        
        public bool OccupySpace { get => occupySpace; set => occupySpace = value; }
    }
}