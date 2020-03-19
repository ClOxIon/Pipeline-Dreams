using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{

    public class EntitySoulCollector : MonoBehaviour
    {

        Entity entity;
        
        private void Awake() {

            entity = GetComponent<Entity>();
            entity.OnInit += (tm, ec) => {
                entity.Parameters.Add("Souls", 0);
                
            };
           

        }
        public void AddSoul(int i) => entity.Parameters["Souls"] += i;

    }
}