using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "EntityDataContainer", menuName = "ScriptableObjects/Manager/EntityDataContainer")]
    public class EntityDataContainer : ScriptableObject {
        public event Action<Entity> OnNewEntitySpawn;
        public event Action<Entity> OnEntityDeath;
        [SerializeField] EntityDataset EDataContainer;
        List<Entity> EntitiesInScene = new List<Entity>();
        // Start is called before the first frame update
        [SerializeField] MapDataContainer mManager;
        
        public EntityData GetEntityDataFromName(string name) {
            return EDataContainer.Dataset.Find((x) => { return x.Name == name; });

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
        public Entity FindEntityInPosition(Vector3Int v) {

            foreach (var e in EntitiesInScene)
                if (e.IdealPosition == v) return e;
            return null;
        }
        public Entity FindEntityInPosition(int x, int y, int z) {
            return FindEntityInPosition(new Vector3Int(x, y, z));
        }
        public Entity FindEntityInRelativePosition(Vector3Int v, Entity origin) {

            foreach (var e in EntitiesInScene)
                if (e.IdealPosition == v + origin.IdealPosition) return e;
            return null;
        }
        public Entity FindEntityInRelativePosition(int x, int y, int z, Entity origin) {

            return FindEntityInRelativePosition(new Vector3Int(x, y, z), origin);
        }
        public Entity FindEntityInLine(int f, Entity origin, int Sightscale = TileContainer.sightscale) {
            Entity e;
            for (int i = 1; i <= Sightscale; i++) {
                if (mManager.GetTileRelative(Vector3Int.zero + Util.FaceToLHVector(f) * (i - 1), f, origin) == Tile.wall)
                    return null;
                e = FindEntityInRelativePosition(Util.FaceToLHVector(f) * i, origin);
                if (e != null)
                    return e;
            }
            return null;
        }
        public Entity[] FindEntitiesOfType(EntityType type) {
            return EntitiesInScene.FindAll((x) => x.Type == type).ToArray();
        }

    }
}