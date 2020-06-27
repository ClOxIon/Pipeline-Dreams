using System.Linq;
using UnityEngine;
using System;

namespace PipelineDreams.Entity {
    public class Sight : MonoBehaviour, ISensoryDevice {
        protected Entity entity;
        /// <summary>
        /// Target of ranged attack. 
        /// </summary>
        public Entity Target { get; protected set; }
        protected TaskManager CM;
        protected Container ec;

        public CommunicationMode mode => CommunicationMode.vision;
        public event Action<Entity> OnTargetChange;
        /// <summary>
        /// Normal entities could only see entities in their line of sight.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual bool IsVisible(Entity e) {
            return true;
            //return ec.FindLineOfSightEntityOnAxis(Util.QToFace(entity.IdealRotation), entity) == e;
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
            CM.OnTaskEnd += CheckTarget;
            ec = arg3;
        }
        protected void ChangeTarget(Entity NewTarget) {
            if (!IsVisible(NewTarget))
                Debug.LogError("The target set is not visible!");
            Target = NewTarget;
            OnTargetChange?.Invoke(Target);
        }
        /// <summary>
        /// The visibility of the target is checked at every OnTaskEnd. If the target is not visible, it is set to closest visible enemy. If there are no visible enemies, it is set to closest visible entity.
        /// </summary>
        private void CheckTarget()
        {
            
            //if(!IsVisible(Target))
            //TODO
                
        }
    }
}