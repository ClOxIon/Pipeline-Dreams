using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    /// <summary>
    /// Determines the type of the entity.
    /// </summary>
    public enum EntityType {
        PLAYER, ENEMY, NPC, TILE
    }
    public class Entity : MonoBehaviour {
        public event Action<TaskManager, EntityDataContainer> OnInit;
        public Vector3Int IdealPosition;
        public Quaternion IdealRotation;
        public EntityData Data { get; private set; }
        public Dictionary<string, float> Parameters = new Dictionary<string, float>();//Active Parameters. Parameters are simple float values, and are intended to be only add/subtracted from current value.
        public Dictionary<string, MutableValue.FunctionChain> Stats = new Dictionary<string, MutableValue.FunctionChain>();//Active Stats

        /// <summary>
        /// Name of the parameter, and the final value.
        /// </summary>
        public event Action<string, float> OnParamChange;
        public bool IsActive = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InitPosition">RH VectorInt</param>
        /// <param name="InitQ">RH Quaternion</param>
        public void Initialize(Vector3Int InitPosition, Quaternion InitQ, EntityData data, TaskManager tm, EntityDataContainer ec) {
            IdealPosition = InitPosition;
            IdealRotation = InitQ;
            Data = data;
            IsActive = true;
            OnInit?.Invoke(tm, ec);
        }
    }
}