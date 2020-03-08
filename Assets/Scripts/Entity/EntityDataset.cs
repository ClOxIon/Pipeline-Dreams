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

        [SerializeField] private int maxHP;
        [SerializeField] private int damage;
        [SerializeField] private EntityType type;
        [SerializeField] private bool occupySpace;
        [Tooltip("The entity has a small cross-section when looked through its X axis; from its sides.")] [SerializeField] private bool invisibleX;
        [Tooltip("The entity has a small cross-section looked through its Z axis; from its front and back.")] [SerializeField] private bool invisibleZ;
        [Tooltip("The entity has a small cross-section when looked through its Y axis; from its up and down.")] [SerializeField] private bool invisibleY;
        [SerializeField]private Entity prefab;

        public int MaxHP { get => maxHP; set => maxHP = value; }
        public EntityType Type { get => type; set => type = value; }
        public Entity Prefab { get => prefab; set => prefab = value; }
        public bool InvisibleY { get => invisibleY; set => invisibleY = value; }
        public bool InvisibleZ { get => invisibleZ; set => invisibleZ = value; }
        public bool InvisibleX { get => invisibleX; set => invisibleX = value; }
        public bool InvisibleOn(Quaternion q) { switch (Util.LHQToFace(q)) {
            case 0: return InvisibleX; 
            case 1: return InvisibleX;
            case 2: return InvisibleY; 
            case 3: return InvisibleY; 
            case 4: return InvisibleZ; 
            case 5: return InvisibleZ; 
            }
            return false;
        }
        
        public bool OccupySpace { get => occupySpace; set => occupySpace = value; }
    }
}