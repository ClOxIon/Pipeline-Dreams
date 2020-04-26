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
        [Tooltip("The entity has a small cross-section when looked through its X axis; from its sides.")] [SerializeField] private bool invisibleX;
        [Tooltip("The entity has a small cross-section looked through its Z axis; from its front and back.")] [SerializeField] private bool invisibleZ;
        [Tooltip("The entity has a small cross-section when looked through its Y axis; from its up and down.")] [SerializeField] private bool invisibleY;
        [SerializeField]private Entity prefab;

        public int MaxHP { get => maxHP; }
        public EntityType Type { get => type; }
        public Entity Prefab { get => prefab; }
        public YarnProgram Dialogue { get => dialogue; }
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