using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{
    [CreateAssetMenu(fileName = "EntityDataContainer", menuName = "ScriptableObjects/Manager/EntityDataContainer")]
    public class EntityDataContainer : ScriptableObject {
        public event Action<Entity> OnNewEntitySpawn;
        public event Action<Entity> OnEntityDeath;
        [SerializeField] EntityDataset EDataContainer;
        List<Entity> EntitiesInScene = new List<Entity>();
        // Start is called before the first frame update
        
        public EntityData GetEntityDataFromName(string name) {
            return EDataContainer.DataSet.Find((x) => { return x.Name.Equals(name); }) as EntityData;

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
            return EntitiesInScene.FindAll(match);
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
                foreach (var x in FindEntities((x) => x.IdealPosition == origin.IdealPosition+Util.FaceToLHVector(f)*i)) {
                    //Check if the entities in between are invisible in our axis of interest.
                    if (!x.Data.InvisibleOn(Quaternion.Inverse(x.IdealRotation) * Util.FaceToLHQ(f)))
                        return x;
                }
            }
            return null;
        }
        
        public Entity[] FindEntitiesOfType(EntityType type) {
            return EntitiesInScene.FindAll((x) => x.Data.Type == type).ToArray();
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
            return EntitiesInScene.Find((x) => x.IdealPosition == new Vector3Int(i, j, k) && x.Data.Type == type);
        }
        public Entity FindEntity(int i, int j, int k, int f, EntityType type) {
            return EntitiesInScene.Find((x) => x.IdealPosition == new Vector3Int(i, j, k) && Util.LHQToFace(x.IdealRotation) == f && x.Data.Type == type);
        }
        public Entity FindEntity(int i, int j, int k, int f) {
            return EntitiesInScene.Find((x) => x.IdealPosition == new Vector3Int(i, j, k) && Util.LHQToFace(x.IdealRotation) == f);
        }
        public Entity FindEntity(int i, int j, int k) {
            return EntitiesInScene.Find((x) => x.IdealPosition == new Vector3Int(i, j, k));
        }
        
    }
}