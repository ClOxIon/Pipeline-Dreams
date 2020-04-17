using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Entity
{
    public class RendererDisable : MonoBehaviour
    {
        protected Entity entity;
        protected TaskManager CM;
        protected Container ec;
        private void Awake()
        {
            entity = GetComponent<Entity>();
            entity.OnInit += Entity_OnInit;
        }

        protected virtual void Entity_OnInit(TaskManager arg1, Container arg3)
        {
            CM = arg1;
            ec = arg3;
           
        }
        private void Start()
        {
            
             ec.FindEntitiesOfType(EntityType.PLAYER)[0].GetComponent<Move>().SubscribeOnMove(RendererEnableDisable);
        }
        IEnumerator RendererEnableDisable(Vector3Int x, Vector3Int y) {
            
            var v = y - entity.IdealPosition;
            if (v.x * v.y != 0 || v.x * v.z != 0 || v.z * v.y != 0)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
            return null;
        }
        
    }
}
