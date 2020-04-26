using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PipelineDreams.Entity
{
    [CreateAssetMenu(fileName = "EntityDataContainer", menuName = "ScriptableObjects/Manager/EntityDataContainer")]
    public class Container : ScriptableObject {
        /// <summary>
        /// This is not called for player entity.
        /// </summary>
        public event Action<Entity> OnNewEntitySpawn;
        public event Action<Entity> OnEntityDeath;
        [SerializeField] Dataset EDataContainer;

        Dictionary<Guid, Entity> EntitiesInScene = new Dictionary<Guid, Entity>();
        // Start is called before the first frame update
        
        public Data GetEntityDataFromName(string name) {
            if (!EDataContainer.DataSet.Any((x) => { return x.Name.Equals(name); }))
                Debug.LogWarning($"EntityDataContainer.GetEntityDataFromName: No Entity Named {name}");
            return EDataContainer.DataSet.Find((x) => { return x.Name.Equals(name); }) as Data;
            
        }
        public void Initialize() {
            EntitiesInScene = new Dictionary<Guid, Entity>();
        }
        /// <summary>
        /// Add already initialized player to the list
        /// </summary>
        /// <param name="Player"></param>
        public void AddPlayer(Entity Player) {
            EntitiesInScene.Add(Guid.NewGuid(), Player);
        }
        public void AddEntityInScene(Vector3Int pos, Quaternion rot, string name, TaskManager tm, float Clock) {
            tm.AddSequentialTask(new EntityAddTask() { Name = name, Pos = pos, Rot = rot, EM = this, StartClock = Clock, TM = tm });
           
        }

        private void EntityDataContainer_OnEntityDeath(Entity obj)
        {
            var id = EntitiesInScene.First((x) => x.Value == obj).Key;
            EntitiesInScene.Remove(id);
            OnEntityDeath?.Invoke(obj);
        }

        public List<Entity> FindEntities(Predicate<Entity> match) {
            return EntitiesInScene.Values.ToList().FindAll(match);
        }
        public Entity FindEntityRelative(Vector3Int v, int face, Entity origin) {

            return FindEntity(origin.IdealPosition + v, face);
        }
        public Entity FindEntityRelative(Vector3Int v, int face, EntityType type, Entity origin) {

            return FindEntity(origin.IdealPosition + v, face, type);
        }
        public Entity FindEntityRelative(Vector3Int v, EntityType type, Entity origin) {

            return FindEntity(origin.IdealPosition + v, type);
        }
        public Entity FindEntityRelative(Vector3Int v, Entity origin) {

            return FindEntity(origin.IdealPosition + v);
        }
        
        public Entity FindVisibleEntityOnAxis(int f, Entity observer, int searchDistance = 100) {
            //first examine entities in zero distance
            foreach (var x in FindEntities((x) => x.IdealPosition == observer.IdealPosition && Util.LHQToFace(x.IdealRotation) == f && x != observer).OrderBy((x) => x.Data.Type)) {
                if (IsEntityVisible(f, x, observer))//You cannot see yourself.
                    return x;
            }

            for (int i = 1; i <= searchDistance; i++) {
                //OrderBy uses type as priority
                foreach (var x in FindEntities((x) => x.IdealPosition == observer.IdealPosition+Util.FaceToLHVector(f)*i).OrderBy((x)=>x.Data.Type)) {
                    //Check if the entities in between are invisible in our axis of interest.
                    if (IsEntityVisible(f, x, observer))
                        return x;
                }
            }
            return null;
        }
        bool IsEntityVisible(int f, Entity x, Entity observer) {
            if (!x.Data.InvisibleOn(Quaternion.Inverse(x.IdealRotation) * Util.FaceToLHQ(f)))
                return true;
            return false;
        }
        
        public Entity[] FindEntitiesOfType(EntityType type) {
            return EntitiesInScene.Values.Where((x) => x.Data.Type == type).ToArray();
        }
        /// <summary>
        /// Returns true if v2 is in line of sight with v1. Note that the line should be axial.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool IsLineOfSight(Vector3Int v1, Vector3Int v2) {
            var v = v1 - v2;


            if (v.x * v.y != 0 || v.y * v.z != 0 || v.z * v.x != 0)
                return false;
            var m = v.magnitude;
            v.Clamp(Vector3Int.one * (-1), Vector3Int.one);
            var f = Util.LHUnitVectorToFace(v);
            for (int i = 1; i < m; i++)
                foreach (var x in FindEntities((x) => x.IdealPosition == v2 + v * i)) {
                    //Check if the entities in between are invisible in our axis of interest.
                    if(!x.Data.InvisibleOn(Quaternion.Inverse(x.IdealRotation) * Util.FaceToLHQ(f)))
                    return false;
                }
            return true;

        }
        

        public Entity FindEntity(Vector3Int v) {
            return FindEntity(v.x, v.y, v.z);
        }
        public Entity FindEntity(Vector3Int v, int f) {
            return FindEntity(v.x, v.y, v.z, f);
        }
        public Entity FindEntity(Vector3Int v, EntityType type) {
            return FindEntity(v.x, v.y, v.z, type);
        }
        public Entity FindEntity(Vector3Int v, int f, EntityType type) {
            return FindEntity(v.x, v.y, v.z, f, type);
        }
        public Entity FindEntity(int i, int j, int k, EntityType type) {
            return EntitiesInScene.Values.First((x) => x.IdealPosition == new Vector3Int(i, j, k) && x.Data.Type == type);
        }
        public Entity FindEntity(int i, int j, int k, int f, EntityType type) {
            return EntitiesInScene.Values.First((x) => x.IdealPosition == new Vector3Int(i, j, k) && Util.LHQToFace(x.IdealRotation) == f && x.Data.Type == type);
        }
        public Entity FindEntity(int i, int j, int k, int f) {
            return EntitiesInScene.Values.First((x) => x.IdealPosition == new Vector3Int(i, j, k) && Util.LHQToFace(x.IdealRotation) == f);
        }
        public Entity FindEntity(int i, int j, int k) {
            return EntitiesInScene.Values.First((x) => x.IdealPosition == new Vector3Int(i, j, k));
        }
        class EntityAddTask : IClockTask
        {
            public TaskPriority Priority => TaskPriority.SPAWNER;
            public Container EM;
            public string Name;
            public TaskManager TM;
            public Vector3Int Pos;
            public Quaternion Rot;
            public float StartClock { get; set; }
            
            public IEnumerator Run()
            {
                var data = EM.GetEntityDataFromName(Name);
                var e = Instantiate(data.Prefab, GraphicalConstants.WORLDSCALE * (Vector3)Pos, Rot);
                e.Initialize(Pos, Rot, data, TM, EM);
                if (data.Type == EntityType.TILE && data.OccupySpace && EM.EntitiesInScene.Values.Any((x) => x.IdealPosition == Pos && Util.LHQToFace(x.IdealRotation) == Util.LHQToFace(Rot) && x.Data.Type == EntityType.TILE && x.Data.OccupySpace))
                    Debug.LogWarning("Overlapping Tile Detected!:" + e.IdealPosition);
                EM.EntitiesInScene.Add(Guid.NewGuid(), e);
                EM.OnNewEntitySpawn?.Invoke(e);
                e.OnEntityDeath += EM.EntityDataContainer_OnEntityDeath;
                return null;
            }
        }
    }
}