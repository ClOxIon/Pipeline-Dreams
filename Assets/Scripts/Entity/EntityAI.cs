using UnityEngine;

namespace PipelineDreams
{
    public enum EntityEmotion {
        None = 0, Surprised = 1, Request = 2, Confused = 3, Angry = 4

    }
    public enum EntityAIState {
        Wander = 1, Chase = 2, Attack = 3, Confused = 4

    }
    public abstract class EntityAI : MonoBehaviour {
        public EntityEmotion Emotion { get; protected set; }
        protected Entity entity;
        protected TaskManager CM;
        protected EntityDataContainer EM;
        protected EntitySight ES;
        protected EntityMove move;
        public EntityAIState State { get; protected set; } = EntityAIState.Wander;
        protected EntityAbility EA;
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Time consumed in action</returns>
        protected abstract void Act();
        public virtual float EntityClock { get; set; }
        protected void Awake() {
            entity = GetComponent<Entity>();
            ES = GetComponent<EntitySight>();
            move = GetComponent<EntityMove>();
            EA = GetComponent<EntityAbility>();
            entity.OnInit += (tm, ec) => { CM = tm; EntityClock = CM.Clock; Act(); EM = ec; };
        }

    }
}