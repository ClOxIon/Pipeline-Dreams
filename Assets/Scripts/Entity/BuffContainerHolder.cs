using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Entity
{


    public class BuffContainerHolder : MonoBehaviour {
        [Tooltip("every entity, normally activated, will hold a unique copy of this Buff Container.")] public Buff.Container BuffContainer;

        [Tooltip("Share the assigned container, rather than copying it.")] public bool KeepPrefab;
        private void Awake()
        {
            GetComponent<Entity>().OnInit += (tm, ec) => 
            { 
                if(!KeepPrefab) 
                    BuffContainer = Instantiate(BuffContainer);
                BuffContainer.Init(tm, GetComponent<Entity>());
            };
        }
    }
}