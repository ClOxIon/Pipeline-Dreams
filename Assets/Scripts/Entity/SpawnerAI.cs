using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Entity {
    public class SpawnerAI : AI
    {
        [SerializeField] string SpawnedEntity;
        [SerializeField] Vector3Int SpawnPosition;
        [SerializeField] bool RelativeTo;
        [SerializeField] float SpawnPeriod;


        public void Spawn() {
            Vector3Int position = SpawnPosition;
            if (RelativeTo)
                position = SpawnPosition  + entity.IdealPosition;
            if (EM.GetEntityDataFromName(SpawnedEntity).OccupySpace)
                foreach (var x in EM.FindEntities((x) => x.IdealPosition == position))
                    if (x.Data.OccupySpace)
                    {
                        Debug.Log("Spawn Failed: " + position);
                        return;//Spawn failed because no two OccupySpace Entities in the same cell.
                    }
            EM.AddEntityInScene(position, entity.IdealRotation, SpawnedEntity, CM,0);
        }

        protected override void Act()
        {
            CM.AddSequentialTask(new SpawnerAITask()
            {
                AI = this,
                StartClock = EntityClock,
                Priority = TaskPriority.ENEMY,
                Act = () =>
                {
                    Spawn();
                    EntityClock += SpawnPeriod;
                    Act();
                }
            });
        }
        class SpawnerAITask : IClockTask
        {
            public TaskPriority Priority { get; set; }
            public SpawnerAI AI;
            public float StartClock { get; set; }
            public Action Act;
            public IEnumerator Run()
            {
                Act();
                return null;
            }


        }
    }
}