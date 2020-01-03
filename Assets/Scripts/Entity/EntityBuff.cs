using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{


    public class EntityBuff : MonoBehaviour {
        [Tooltip("every entity, normally activated, will hold a unique copy of this Buff Container.")] public BuffContainer BuffContainer;

        [Tooltip("Share the assigned container, rather than copying it.")] public bool KeepPrefab;
        private void Awake()
        {
            GetComponent<Entity>().OnInit += (tm, mc, ec) => 
            { 
                if(!KeepPrefab) 
                    BuffContainer = Instantiate(BuffContainer);
                BuffContainer.Init(tm, GetComponent<Entity>());
            };
        }
    }
}