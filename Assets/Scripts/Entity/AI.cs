using UnityEngine;

namespace PipelineDreams.Entity
{
    public enum EntityEmotion {
        None = 0, Surprised = 1, Request = 2, Confused = 3, Angry = 4

    }
    public enum AIState {
        Wander = 1, Chase = 2, Attack = 3, Confused = 4

    }
    public abstract class AI : MonoBehaviour {
        public EntityEmotion Emotion { get; protected set; }
        protected Entity entity;
        protected TaskManager CM;
        protected Container EM;
        protected Sight ES;
        protected Move move;
        public AIState State { get; protected set; } = AIState.Wander;
        protected InstructionContainerHolder EA;
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Time consumed in action</returns>
        protected abstract void Act();
        public virtual float EntityClock { get; set; }
        protected void Awake() {
            entity = GetComponent<Entity>();
            ES = GetComponent<Sight>();
            move = GetComponent<Move>();
            EA = GetComponent<InstructionContainerHolder>();
            entity.OnInit += (tm, ec) => { 
                CM = tm; 
                EntityClock = tm.Clock; 
                Act(); 
                EM = ec; };
        }

    }
}