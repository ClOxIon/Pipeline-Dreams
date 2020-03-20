using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    public class EntitySpawner : MonoBehaviour
    {
        [SerializeField] string SpawnedEntity;
        [SerializeField] Entity.Container TargetContainer;
        [SerializeField] TaskManager tm;
        [SerializeField] Vector3Int SpawnPosition;
        [SerializeField] bool SpawnWhenStart;
        Entity.Entity entity;
        // Start is called before the first frame update
        void Start() {
            if (SpawnWhenStart)
                Spawn();
            entity = GetComponent<Entity.Entity>();
        }
        public void Spawn() {
            TargetContainer.AddEntityInScene(SpawnPosition, Quaternion.identity, SpawnedEntity, tm);
        }
        // Update is called once per frame
        void Update() {

        }
    }
}