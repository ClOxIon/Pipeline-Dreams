using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PipelineDreams
{
    [CreateAssetMenu(fileName = "EntityDataContainer", menuName = "ScriptableObjects/Manager/EntityDataContainer")]
    public class EntityDataContainer : ScriptableObject {
        /// <summary>
        /// This is not called for player entity.
        /// </summary>
        public event Action<Entity> OnNewEntitySpawn;
        public event Action<Entity> OnEntityDeath;
        [SerializeField] EntityDataset EDataContainer;

        Dictionary<Guid, Entity> EntitiesInScene = new Dictionary<Guid, Entity>();
        // Start is called before the first frame update
        
        public EntityData GetEntityDataFromName(string name) {
            if (!EDataContainer.DataSet.Any((x) => { return x.Name.Equals(name); }))
                Debug.LogWarning($"EntityDataContainer.GetEntityDataFromName: No Entity Named {name}");
            return EDataContainer.DataSet.Find((x) => { return x.Name.Equals(name); }) as EntityData;
            
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
        public void AddEntityInScene(Vector3Int pos, Quaternion rot, string name, TaskManager tm) {
            var data = GetEntityDataFromName(name);
            var e = Instantiate(data.Prefab, GraphicalConstants.WORLDSCALE* (Vector3)pos  , rot);
            e.Initialize(pos, rot, data, tm, this);
            if (EntitiesInScene.Values.Any((x) =>x.IdealPosition == pos && Util.LHQToFace(x.IdealRotation) == Util.LHQToFace(rot)))
                Debug.LogWarning("Overlapping Tile Detected!:" + e.IdealPosition);
            EntitiesInScene.Add(Guid.NewGuid(), e);
            OnNewEntitySpawn?.Invoke(e);
            e.OnEntityDeath += EntityDataContainer_OnEntityDeath;
           
        }

        private void EntityDataContainer_OnEntityDeath(Entity obj)
        {
            var id = EntitiesInScene.First((x) => x.Value == obj).Key;
            EntitiesInScene.Remove(id);
            OnEntityDeath?.Invoke(obj);
        }

        /*This codebase will be moved.
void SpawnEnemy(string name, Vector3Int Position, Quaternion Rotation) {

   try {


       if (FindEntityInPosition(Position) != null) return;
       var dt = GetEntityDataFromName(name);
       var mob = Instantiate(dt.Prefab);
       mob.Initialize(Position, Rotation, dt);
       EntitiesInScene.Add(mob);
       OnNewEntitySpawn?.Invoke(mob);
       var d = mob.GetComponent<EntityDeath>();
       if (d != null) d.OnEntityDeath += (e) => { EntitiesInScene.Remove(e); OnEntityDeath?.Invoke(e); };
   }
   catch (ArgumentOutOfRangeException e) {

   }
}
*/
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
        
        public Entity FindEntityOnAxis(int f, Entity origin, int searchDistance = 100) {
           
            for (int i = 1; i <= searchDistance; i++) {
                //OrderBy uses type as priority
                foreach (var x in FindEntities((x) => x.IdealPosition == origin.IdealPosition+Util.FaceToLHVector(f)*i).OrderBy((x)=>x.Data.Type)) {
                    //Check if the entities in between are invisible in our axis of interest.
                    if (!x.Data.InvisibleOn(Quaternion.Inverse(x.IdealRotation) * Util.FaceToLHQ(f)))
                        return x;
                }
            }
            return null;
        }
        
        public Entity[] FindEntitiesOfType(EntityType type) {
            return EntitiesInScene.Values.Where((x) => x.Data.Type == type).ToArray();
        }
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
        
    }
}