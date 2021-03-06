﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Entity {
    /// <summary>
    /// Determines the type of the entity. It is also the search priority of various search functions.
    /// </summary>
    public enum EntityType {
        PLAYER, ENEMY, NPC, TILE
    }
    public class Entity : MonoBehaviour {
        public event Action<TaskManager, Container> OnInit;
        public Vector3Int IdealPosition;
        public Quaternion IdealRotation;

        /// <summary>
        /// The read-only data of this entity. This data could not be modified but instead could be replaced with another data in EnData, if wanted.
        /// </summary>
        public Data Data { get; private set; }
        public EntityParameterDictionary Parameters;//Active Parameters. Parameters are simple float values, and are intended to be only add/subtracted from current value.
        public Dictionary<string, MutableValue.FunctionChain> Stats = new Dictionary<string, MutableValue.FunctionChain>();//Active Stats

        /// <summary>
        /// Name of the parameter, and the final value.
        /// </summary>
        public event Action<string, float> OnParamChange;
        public bool IsActive = false;

        public event Action<Entity> OnEntityDeath;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InitPosition">RH VectorInt</param>
        /// <param name="InitQ">RH Quaternion</param>
        public void Initialize(Vector3Int InitPosition, Quaternion InitQ, Data data, TaskManager tm, Container ec) {
            Parameters = new EntityParameterDictionary(this);
            IdealPosition = InitPosition;
            IdealRotation = InitQ;
            Data = data;
            IsActive = true;
            OnInit?.Invoke(tm, ec);
            Parameters.InvokeAllParamChange();
        }
        public void Death()
        {
            OnEntityDeath?.Invoke(GetComponent<Entity>());
            GetComponent<Entity>().IsActive = false;
            GetComponent<Animator>()?.InvokeAnimation("Death", true);
        }
        public class EntityParameterDictionary
        {
            private Entity Holder;
            private readonly IDictionary<string, float> parameters = new Dictionary<string, float>();

            public EntityParameterDictionary(Entity holder) => Holder = holder;

            public float this[string key]
            {
                // returns value if exists
                get { return parameters[key]; }

                // updates if exists, adds if doesn't exist
                set { parameters[key] = value; Holder.OnParamChange?.Invoke(key, value); }
            }
            public void Add(string key, float value) => parameters.Add(key, value);
            public void InvokeAllParamChange() {
                foreach (var x in parameters)
                    Holder.OnParamChange?.Invoke(x.Key, x.Value);
            }
        }
    }

    
}