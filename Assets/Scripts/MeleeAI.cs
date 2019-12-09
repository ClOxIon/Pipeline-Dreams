using System;
using System.Collections;
using UnityEngine;

namespace PipelineDreams {
    public enum EntityEmotion {
        None = 0, Surprised = 1, Request = 2, Confused = 3, Angry = 4

    }
    public enum EntityAIState {
        Wander = 1, Chase = 2, Attack = 3, Confused = 4

    }
    public abstract class EntityAI : MonoBehaviour {
        public EntityEmotion emotion { get; protected set; }
        protected Entity entity;
        [SerializeField] protected TaskManager CM;
        [SerializeField] protected MapDataContainer mManager;
        [SerializeField] protected EntityDataContainer EM;
        [SerializeField] protected Entity Player;
        protected EntitySight ES;
        protected EntityMove move;
        public EntityAIState state { get; protected set; } = EntityAIState.Wander;
        protected EntityWeapon EW;
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
            EW = GetComponent<EntityWeapon>();
            entity.OnInit += () => EntityClock = CM.Clock;
            entity.OnInit += () => Act();
        }

    }
    public class MeleeAI : EntityAI {
        /// <summary>
        /// 적의 위치를 기억하고 있는가?
        /// </summary>
        bool IsTargetSeen = false;
        bool IsRecentlyDamaged = false;
        Vector3Int LastTargetPositionSeen;
        int DamagedDirection;
        Entity Attacker;
        Entity Target;
        int memoryTime;
        //Vector3Int Target;
        protected new void Awake() {
            base.Awake();
            GetComponent<EntityHealth>().OnDamagedAmount += (x, y, e) => { if (x > 0) IsRecentlyDamaged = true; Attacker = e; DamagedDirection = Util.LHUnitVectorToFace(Util.Normalize(e.IdealPosition - entity.IdealPosition)); };
        }

        protected override void Act() {
            CM.AddSequentialTask(new MeleeAITask() {
                AI = this, StartClock = EntityClock, Priority = (int)entity.Type, Act = () => {
                    if (!entity.IsActive)
                        return;
                    memoryTime++;

                    if (memoryTime > 3) {
                        IsTargetSeen = false;
                        Target = null;

                    }
                    //Debug.Log(state.ToString());
                    emotion = EntityEmotion.None;
                    switch (state) {
                    case EntityAIState.Attack:
                        if (Target == null || !Target.enabled) {
                            Target = null;
                            state = EntityAIState.Wander;
                            emotion = EntityEmotion.Confused;
                            break;
                        }
                        if (!EW.CanAttack(Target))
                            if (IsTargetSeen) {
                                state = EntityAIState.Chase;
                            } else {
                                state = EntityAIState.Wander;
                                emotion = EntityEmotion.Confused;
                                Target = null;
                            }
                        break;
                    case EntityAIState.Chase:
                        if (Target == null || !Target.enabled) {
                            Target = null;
                            state = EntityAIState.Wander;
                            break;
                        }

                        if (EW.CanAttack(Target))
                            state = EntityAIState.Attack;
                        if (entity.IdealPosition == LastTargetPositionSeen && !ES.IsVisible(Target)) {
                            state = EntityAIState.Wander;
                            emotion = EntityEmotion.Confused;
                            Target = null;
                        }
                        break;
                    case EntityAIState.Wander:
                        if (IsRecentlyDamaged) {
                            //Target = EM.FindEntityInLine(DamagedDirection, entity, 6);

                            state = EntityAIState.Confused;
                            emotion = EntityEmotion.Angry;
                            break;
                        }
                        if (ES.IsVisible(Player)) {
                            IsTargetSeen = true;
                            memoryTime = 0;
                            Target = Player;
                            state = EntityAIState.Chase;
                            emotion = EntityEmotion.Surprised;
                            break;
                        }
                        break;
                    case EntityAIState.Confused:
                        if (Util.LHQToFace(entity.IdealRotation) == DamagedDirection) {
                            Target = EM.FindEntityInLine(DamagedDirection, entity);
                            if (Target != null) {
                                IsTargetSeen = true;
                                memoryTime = 0;
                                state = EntityAIState.Chase;
                                emotion = EntityEmotion.Surprised;
                            } else {
                                state = EntityAIState.Wander;
                                emotion = EntityEmotion.Confused;
                            }
                        }
                        break;



                    }

                    switch (state) {
                    case EntityAIState.Attack:
                        EW.TryAttack(Target, EntityClock, 1, 0, 0);//Attempts melee attack
                        IsTargetSeen = true;
                        memoryTime = 0;
                        break;
                    case EntityAIState.Chase:

                        if (ES.IsVisible(Target)) {
                            IsTargetSeen = true;
                            memoryTime = 0;
                            LastTargetPositionSeen = Target.IdealPosition;
                            move.MoveToward(Target.IdealPosition, EntityClock);
                        } else {
                            move.MoveToward(LastTargetPositionSeen, EntityClock);
                        }
                        break;
                    case EntityAIState.Wander:
                        IsTargetSeen = false;
                        RandomWalk();
                        break;
                    case EntityAIState.Confused:
                        move.Face(DamagedDirection, EntityClock);
                        break;

                    }
                    IsRecentlyDamaged = false;
                    Act();
                    void RandomWalk() {
                        if (UnityEngine.Random.Range(0f, 1f) < 0.5f) { emotion = EntityEmotion.None; move.MoveToward(Util.LHQToLHUnitVector(entity.IdealRotation), EntityClock); } else {
                            emotion = EntityEmotion.None; move.Face(Util.LHQToFace(TurnRandom()), EntityClock);
                        }

                    }
                }
            }
           );
        }
        class MeleeAITask : IClockTask {
            public int Priority { get; set; }
            public MeleeAI AI;
            public float StartClock { get; set; }
            public Action Act;
            public IEnumerator Run() {
                Act();
                return null;
            }


        }
        Quaternion TurnRandom() {
            var i = UnityEngine.Random.Range(0, 4);
            switch (i) {
            case 0: return Util.TurnUp;
            case 1: return Util.TurnDown;
            case 2: return Util.TurnLeft;
            case 3: return Util.TurnRight;
            default:
                return Quaternion.identity;
            }
        }
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}