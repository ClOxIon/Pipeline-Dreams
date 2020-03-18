using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    using MutableValue;
    public class EntityMove : MonoBehaviour {
        /// <summary>
        /// mutable Rotation time.
        /// </summary>
        public FunctionChain RTimeModifier = new FunctionChain();
        /// <summary>
        /// mutable Translation time.
        /// </summary>
        public FunctionChain TTimeModifier = new FunctionChain();
        Entity entity;
        TaskManager CM;
        EntityDataContainer EM;
        /// <summary>
        /// Position before, Position after, Coroutine
        /// </summary>
        List<Func<Vector3Int, Vector3Int, IEnumerator>> OnMove = new List<Func<Vector3Int, Vector3Int, IEnumerator>>();
        /// <summary>
        /// Orientation before, Orientation after, Coroutine
        /// </summary>
        List<Func<Quaternion, Quaternion, IEnumerator>> OnRotate = new List<Func<Quaternion, Quaternion, IEnumerator>>();

        public void SubscribeOnMove(Func<Vector3Int, Vector3Int, IEnumerator> x) {
            OnMove.Add(x);
        }
        public void UnsubscribeOnMove(Func<Vector3Int, Vector3Int, IEnumerator> x) {
            OnMove.Remove(x);
        }
        public void SubscribeOnRotate(Func<Quaternion, Quaternion, IEnumerator> x) {
            OnRotate.Add(x);
        }
        public void UnsubscribeOnRotate(Func<Quaternion, Quaternion, IEnumerator> x) {
            OnRotate.Remove(x);
        }

        private void Awake() {
            entity = GetComponent<Entity>();
            GetComponent<Entity>().OnInit += EntityMove_OnInit;
            RTimeModifier.OnEvalRequest += () => { RTimeModifier.AddFunction(new Constant() { Value = entity.Data.FindParameterFloat("RotationTime") }); };//Base value of the RTimeModifier.
            RTimeModifier.EvalAtNextGet = true;
            TTimeModifier.OnEvalRequest += () => { TTimeModifier.AddFunction(new Constant() { Value = entity.Data.FindParameterFloat("TranslationTime") }); };//Base value of the TTimeModifier.
            TTimeModifier.EvalAtNextGet = true;
        }

        private void EntityMove_OnInit(TaskManager arg1, EntityDataContainer arg3)
        {
            CM = arg1;
            EM = arg3;
        }

        /// <summary>
        /// Can move to that position next turn?
        /// </summary>
        /// <param name="WVector">World Position</param>
        /// <returns></returns>
        public bool CanMove(Vector3Int WVector) {
            if (!CanStay(WVector)) return false;
            //If there are tiles in the same cell as the player, blocking the way.
            if (EM.FindEntities((x) => x.IdealPosition == entity.IdealPosition &&Util.LHQToFace(x.IdealRotation) == Util.LHUnitVectorToFace(WVector - entity.IdealPosition) && x.Data.Type == EntityType.TILE&&x.Data.HasParameter("BlockEntity")).Count!=0) return false;
            //If there are tiles in the cell the player is trying to go, blocking the way.
            if (EM.FindEntities((x) => x.IdealPosition == WVector &&
            Util.LHQToFace(x.IdealRotation) == Util.LHUnitVectorToFace(entity.IdealPosition - WVector) &&
            x.Data.Type == EntityType.TILE &&
            x.Data.HasParameter("BlockEntity")).Count != 0) return false;
            return true;
        }
        /// <summary>
        /// Can rotate to that orientation next turn?
        /// </summary>
        /// <param name="q">Absolute Quaternion to rotate the object</param>
        /// <returns></returns>
        public bool CanRotate(Quaternion q) {
            var q0 = Quaternion.Inverse(entity.IdealRotation) * q;
            if (q0 == Util.TurnDown || q0 == Util.TurnUp || q0 == Util.TurnLeft || q0 == Util.TurnRight)
                return true;
            return false;
        }
        /// <summary>
        /// Can end turn in that position?
        /// </summary>
        /// <param name="WVector"></param>
        /// <returns></returns>
        public bool CanStay(Vector3Int WVector) {
            if(entity.Data.OccupySpace)
            foreach (var x in EM.FindEntities((x) =>  x.IdealPosition == WVector ) ){
                    if (x.Data.OccupySpace) return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <param name="startClock"></param>

        public virtual void Face(int f, float startClock) {
            TaskPriority _p = TaskPriority.PLAYER;
            switch (entity.Data.Type) {
                case EntityType.ENEMY: _p = TaskPriority.ENEMY; break;

                case EntityType.NPC: _p = TaskPriority.NPC; break;

                case EntityType.PLAYER: _p = TaskPriority.PLAYER; break;
            }
            CM.AddSequentialTask(new RotateTask() { Entity = entity, deltaQ = Util.RotateToFace(f, entity.IdealRotation), StartClock = startClock, Priority = _p});
            
            GetComponent<EntityAI>().EntityClock += RTimeModifier.Value;

        }
        public virtual void MoveToward(Vector3Int v, float startClock) {
            TaskPriority _p = TaskPriority.PLAYER;
            switch (entity.Data.Type)
            {
                case EntityType.ENEMY: _p = TaskPriority.ENEMY; break;

                case EntityType.NPC: _p = TaskPriority.NPC; break;

                case EntityType.PLAYER: _p = TaskPriority.PLAYER; break;
            }
            CM.AddSequentialTask(new MoveTask() { Entity = entity, Face = Util.LHQToFace(entity.IdealRotation), StartClock = startClock, Priority = _p });
            
            GetComponent<EntityAI>().EntityClock += TTimeModifier.Value;
        }
        /// <summary>
        /// Instant warp to the destination.
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="startClock"></param>
        public virtual void MoveWarp(Vector3Int dest, float startClock)
        {
            TaskPriority _p = TaskPriority.PLAYER;
            switch (entity.Data.Type)
            {
                case EntityType.ENEMY: _p = TaskPriority.ENEMY; break;

                case EntityType.NPC: _p = TaskPriority.NPC; break;

                case EntityType.PLAYER: _p = TaskPriority.PLAYER; break;
            }
            CM.AddSequentialTask(new WarpTask() { Entity = entity, Dest = dest, StartClock = startClock, Priority = _p });
        }
        class MoveTask : IClockTask {
            public TaskPriority Priority { get; set; }
            public Entity Entity;
            public int Face;
            public float StartClock { get; set; }

            public IEnumerator Run() {
                var em = Entity.GetComponent<EntityMove>();
                var PositionBefore = Entity.IdealPosition;
                if (!em.CanMove(Entity.IdealPosition+Util.FaceToLHVector(Face))) yield break;
                Entity.IdealPosition += Util.FaceToLHVector(Face);
                foreach (var x in em.OnMove) {
                    var r = x?.Invoke(PositionBefore, Entity.IdealPosition);
                    if (r != null)
                        yield return r;
                }

            }


        }
        class WarpTask : IClockTask
        {
            public TaskPriority Priority { get; set; }
            public Entity Entity;
            public Vector3Int Dest;
            public float StartClock { get; set; }

            public IEnumerator Run()
            {
                var em = Entity.GetComponent<EntityMove>();
                var PositionBefore = Entity.IdealPosition;
                //Canmove check doesn't apply here.
                Entity.IdealPosition = Dest;
                foreach (var x in em.OnMove)
                {
                    var r = x?.Invoke(PositionBefore, Entity.IdealPosition);
                    if (r != null)
                        yield return r;
                }

            }


        }

        class RotateTask : IClockTask {
            public TaskPriority Priority { get; set; }
            public Entity Entity;
            public Quaternion deltaQ;
            public float StartClock { get; set; }

            public IEnumerator Run() {
                if (deltaQ == Quaternion.identity) yield break;

                var em = Entity.GetComponent<EntityMove>();
                if (!em.CanRotate(Entity.IdealRotation * deltaQ)) yield break;
                var RotationBefore = Entity.IdealRotation;
                Entity.IdealRotation = Entity.IdealRotation * deltaQ;
                foreach (var x in em.OnRotate) {
                    var r = x?.Invoke(RotationBefore, Entity.IdealRotation);
                    if (r != null)
                        yield return r;
                }
            }


        }

    }
}