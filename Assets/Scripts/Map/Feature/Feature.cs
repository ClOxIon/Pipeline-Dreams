using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Map.Feature
{
    [CreateAssetMenu(fileName = "Feature", menuName = "ScriptableObjects/Feature/Feature")]
    public class Feature : ScriptableObject {
        /// <summary>
        /// When multiple instances of a same feature exists in runtime, a unique index is given to each of them. 
        /// </summary>
        public int Index;
        /// <summary>
        /// The runtime position of the feature origin.
        /// </summary>
        public Vector3Int Position;
        /// <summary>
        /// The runtime conformation of the feature.
        /// </summary>
        public Quaternion Rotation = Quaternion.identity;

        [SerializeField] private Entity.Dataset EnData;
        /// <summary>
        /// The position of the cells that this feature occupies; relative to the feature origin.
        /// </summary>
        [SerializeField] protected List<Vector3Int> occupiedCells;
        
        /// <summary>
        /// The list of all entities, including all tiles, generated with this feature.
        /// </summary>
        [SerializeField] protected List<Z3QE> entities;

        /// <summary>
        /// The position of potential entrances to the feature; if not specified, then every occupied point could be an entrance. All specified entrances should NOT be in OccupiedCells, and point toward an OccupiedCell.
        /// Multiple entrances could exist in a cell if they all point to different points.
        /// </summary>
        public List<Z3Q> Entrances;
        /// <summary>
        /// Runtime variable used to store entrances that are actually used.
        /// </summary>
        public List<Z3Q> UsedEntrances;

        /// <summary>
        /// These attributed could be overridden for lazy runtime generation, opposed to using the information stored above.
        /// </summary>
        public virtual List<Vector3Int> OccupiedCells { get => occupiedCells; }
        
        public virtual List<Z3QE> Entities { get => entities; }

        private List<Entity.Entity> GeneratedEntities = new List<Entity.Entity>();
        [ContextMenu("Generate In Scene")] private void GenerateInScene() {
            RemoveFromScene();
            foreach (var x in Entities)
                GeneratedEntities.Add(Instantiate(((Entity.Data)EnData.DataSet.Find((en) => en.Name == x.EntityName)).Prefab, x.Position, x.Rotation, null));
        }
        [ContextMenu("Remove From Scene")]
        private void RemoveFromScene()
        {
            foreach (var x in GeneratedEntities)
                if (x != null)
                    DestroyImmediate(x.gameObject);
        }
        
    }
}