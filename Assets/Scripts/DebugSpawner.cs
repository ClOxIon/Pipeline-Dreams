using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Entity {
    public class DebugSpawner : MonoBehaviour
    {
        [SerializeField] string SpawnedEntity;
        [SerializeField] Vector3Int SpawnPosition;
        [SerializeField] Container EM;
        [SerializeField] TaskManager CM;
        private void Start()
        {
            Spawn();
        }
        public void Spawn() {
            Vector3Int position = SpawnPosition;
            
            if (EM.GetEntityDataFromName(SpawnedEntity).OccupySpace)
                foreach (var x in EM.FindEntities((x) => x.IdealPosition == position))
                    if (x.Data.OccupySpace)
                    {
                        Debug.Log("Spawn Failed: " + position);
                        return;//Spawn failed because no two OccupySpace Entities in the same cell.
                    }
            EM.AddEntityInScene(position, Quaternion.identity, SpawnedEntity, CM, 0);
        }

        
    }
}