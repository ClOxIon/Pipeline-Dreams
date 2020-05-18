using System;
using System.Collections;
using UnityEngine;

namespace PipelineDreams.Entity
{
    
    public class MeleeAI : AI {

        /// <summary>
        /// Does the entity remember the position of its target?
        /// </summary>
        bool IsTargetSeen = false;
        bool IsRecentlyDamaged = false;
        Vector3Int LastTargetPositionSeen;
        int DamagedDirection;
        Entity Target;
        int memoryTime;

        //Vector3Int Target;
        protected new void Awake() {
            base.Awake();
            GetComponent<Health>().OnDamaged += (x, e) => { if (x > 0) IsRecentlyDamaged = true; DamagedDirection = Util.LHUnitVectorToFace(Util.Normalize(e.IdealPosition - entity.IdealPosition)); };
        }

        protected override void Act() {
            
            CM.AddSequentialTask(new MeleeAITask() {
                AI = this, StartClock = EntityClock, Priority = TaskPriority.ENEMY, Act = () => {
                    if (!entity.IsActive)
                        return;
                    memoryTime++;

                    if (memoryTime > 3) {
                        IsTargetSeen = false;
                        Target = null;

                    }
                    //Debug.Log(state.ToString());
                    Emotion = EntityEmotion.None;
                    switch (State) {
                    case AIState.Attack:
                        if (Target == null || !Target.enabled) {
                            Target = null;
                            State = AIState.Wander;
                            Emotion = EntityEmotion.Confused;
                            break;
                        }
                        var dv = Target.IdealPosition - entity.IdealPosition;
                            if (dv.magnitude <= 1 && Util.LHQToFace(entity.IdealRotation) == Util.LHUnitVectorToFace(dv))
                            if (IsTargetSeen) {
                                State = AIState.Chase;
                            } else {
                                State = AIState.Wander;
                                Emotion = EntityEmotion.Confused;
                                Target = null;
                            }
                        break;
                    case AIState.Chase:
                        if (Target == null || !Target.enabled) {
                            Target = null;
                            State = AIState.Wander;
                            break;
                        }
                        dv = Target.IdealPosition - entity.IdealPosition;
                            if (dv.magnitude <= 1 && Util.LHQToFace(entity.IdealRotation) == Util.LHUnitVectorToFace(dv))
                            State = AIState.Attack;
                        if (entity.IdealPosition == LastTargetPositionSeen && !ES.IsVisible(Target)) {
                            State = AIState.Wander;
                            Emotion = EntityEmotion.Confused;
                            Target = null;
                        }
                        break;
                    case AIState.Wander:
                        if (IsRecentlyDamaged) {
                            //Target = EM.FindEntityInLine(DamagedDirection, entity, 6);

                            State = AIState.Confused;
                            Emotion = EntityEmotion.Angry;
                            break;
                        }
                            var players = ES.VisibleEntitiesOfType(EntityType.PLAYER);
                        if (players.Length!=0) {
                            IsTargetSeen = true;
                            memoryTime = 0;
                            Target = players[0];
                            State = AIState.Chase;
                            Emotion = EntityEmotion.Surprised;
                            break;
                        }
                        break;
                    case AIState.Confused:
                        if (Util.LHQToFace(entity.IdealRotation) == DamagedDirection) {
                            Target = EM.FindLineOfSightEntityOnAxis(DamagedDirection, entity);
                            if (Target != null) {
                                IsTargetSeen = true;
                                memoryTime = 0;
                                State = AIState.Chase;
                                Emotion = EntityEmotion.Surprised;
                            } else {
                                State = AIState.Wander;
                                Emotion = EntityEmotion.Confused;
                            }
                        }
                        break;



                    }

                    switch (State) {
                    case AIState.Attack:
                        EA.AbilityContainer.UseInstructionByName("MeleeAttack_Mob");
                        IsTargetSeen = true;
                        memoryTime = 0;
                        break;
                    case AIState.Chase:

                        if (ES.IsVisible(Target)) {
                            IsTargetSeen = true;
                            memoryTime = 0;
                            LastTargetPositionSeen = Target.IdealPosition;
                            move.MoveToward(Target.IdealPosition, EntityClock);//Note that most mobs will only go forward when MoveToward is called.
                        } else {
                            move.MoveToward(LastTargetPositionSeen, EntityClock);
                        }
                        break;
                    case AIState.Wander:
                        IsTargetSeen = false;
                        RandomWalk();
                        break;
                    case AIState.Confused:
                        move.Face(DamagedDirection, EntityClock);
                        break;

                    }
                    IsRecentlyDamaged = false;
                    Act();
                    void RandomWalk() {
                        if (UnityEngine.Random.Range(0f, 1f) < 0.5f) { Emotion = EntityEmotion.None; move.MoveToward(Util.LHQToLHUnitVector(entity.IdealRotation), EntityClock); } else {
                            Emotion = EntityEmotion.None; move.Face(Util.LHQToFace(entity.IdealRotation * TurnRandom()), EntityClock);
                        }

                    }
                }
            }
           );
        }
        class MeleeAITask : IClockTask {
            public TaskPriority Priority { get; set; }
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
        
    }
}