using System.Linq;
using UnityEngine;

namespace PipelineDreams {
    public class EntitySight : MonoBehaviour {
        Entity entity;
        
        EntityDataContainer ec;
        public virtual bool IsVisible(Entity e) {
            var v = e.IdealPosition - entity.IdealPosition;
            return ec.IsLineOfSight(entity.IdealPosition, e.IdealPosition) && Util.LHQToLHUnitVector(entity.IdealRotation) == Util.Normalize(v);
        }
        public Entity[] VisibleEntitiesOfType(EntityType type)
        {
            var l = ec.FindEntitiesOfType(type).ToList();
            l.RemoveAll((x) => !IsVisible(x));
            return l.ToArray();
        }

        private void Awake() {
            entity = GetComponent<Entity>();
            entity.OnInit += Entity_OnInit;
        }

        private void Entity_OnInit(TaskManager arg1, EntityDataContainer arg3)
        {
            ec = arg3;
        }
    }
}