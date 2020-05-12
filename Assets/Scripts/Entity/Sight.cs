using System.Linq;
using UnityEngine;

namespace PipelineDreams.Entity {
    public class Sight : MonoBehaviour, ISensoryDevice {
        protected Entity entity;
        protected TaskManager CM;
        protected Container ec;

        public CommunicationMode mode => CommunicationMode.vision;

        /// <summary>
        /// Normal entities could only see entities in their line of sight.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual bool IsVisible(Entity e) {
            var v = e.IdealPosition - entity.IdealPosition;
            return ec.IsLineOfSight(entity.IdealPosition, e.IdealPosition)&&Util.LHQToLHUnitVector(entity.IdealRotation)==v;
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

        protected virtual void Entity_OnInit(TaskManager arg1, Container arg3)
        {
            CM = arg1;
            ec = arg3;
        }
    }
}