using System;
using UnityEngine;

namespace PipelineDreams {
    /// <summary>
    /// Determines the type of the entity.
    /// </summary>
    public enum EntityType {
        PLAYER, ENEMY, NPC
    }
    public class Entity : MonoBehaviour {
        /// <summary>
        /// Do not modify this value through script.
        /// </summary>
        [SerializeField] public EntityType Type;
        public event Action<TaskManager, MapDataContainer, EntityDataContainer> OnInit;
        public Vector3Int IdealPosition;
        public Quaternion IdealRotation;
        public EntityData Data { get; private set; }
        public bool IsActive = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InitPosition">RH VectorInt</param>
        /// <param name="InitQ">RH Quaternion</param>
        public void Initialize(Vector3Int InitPosition, Quaternion InitQ, EntityData data, TaskManager tm, MapDataContainer mc, EntityDataContainer ec) {
            Type = data.Type;
            IdealPosition = InitPosition;
            IdealRotation = InitQ;
            Data = data;
            IsActive = true;
            OnInit?.Invoke(tm, mc, ec);
        }
    }
}