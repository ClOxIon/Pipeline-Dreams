using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    public class EntityMove : MonoBehaviour {
        public float SpeedModifier = 1;
        Entity entity;
        TaskManager CM;
        EntityDataContainer EM;
        MapDataContainer mManager;
        List<Func<Vector3Int, Vector3Int, IEnumerator>> OnMove = new List<Func<Vector3Int, Vector3Int, IEnumerator>>();
        public void SubscribeOnMove(Func<Vector3Int, Vector3Int, IEnumerator> x) {
            OnMove.Add(x);
        }
        public void UnsubscribeOnMove(Func<Vector3Int, Vector3Int, IEnumerator> x) {
            OnMove.Remove(x);
        }
        List<Func<Quaternion, Quaternion, IEnumerator>> OnRotate = new List<Func<Quaternion, Quaternion, IEnumerator>>();
        public void SubscribeOnRotate(Func<Quaternion, Quaternion, IEnumerator> x) {
            OnRotate.Add(x);
        }
        public void UnsubscribeOnRotate(Func<Quaternion, Quaternion, IEnumerator> x) {
            OnRotate.Remove(x);
        }

        private void Awake() {
            entity = GetComponent<Entity>();
            GetComponent<Entity>().OnInit += EntityMove_OnInit;
        }

        private void EntityMove_OnInit(TaskManager arg1, MapDataContainer arg2, EntityDataContainer arg3)
        {
            CM = arg1;
            mManager = arg2;
            EM = arg3;
        }

        public bool CanMove(Vector3Int UVector) {
            if (UVector != Util.LHQToLHUnitVector(entity.IdealRotation)) return false;
            if (!Util.CompareTiles(mManager.GetTileRelative(Vector3Int.zero, Util.LHQToFace(entity.IdealRotation), entity), Tile.hole)) return false;
            if (EM.FindEntityInRelativePosition(UVector, entity) != null) return false;
            return true;
        }
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <param name="startClock"></param>

        public virtual void Face(int f, float startClock) {
            Priority _p = Priority.PLAYER;
            switch (entity.Type) {
                case EntityType.ENEMY: _p = Priority.ENEMY; break;

                case EntityType.NPC: _p = Priority.NPC; break;

                case EntityType.PLAYER: _p = Priority.PLAYER; break;
            }
            CM.AddSequentialTask(new RotateTask() { Entity = entity, deltaQ = Util.RotateToFace(f, entity.IdealRotation), StartClock = startClock, Priority = _p});
            GetComponent<EntityAI>().EntityClock += entity.Data.FindParameterFloat("RotationTime") * SpeedModifier;

        }
        public virtual void MoveToward(Vector3Int v, float startClock) {
            Priority _p = Priority.PLAYER;
            switch (entity.Type)
            {
                case EntityType.ENEMY: _p = Priority.ENEMY; break;

                case EntityType.NPC: _p = Priority.NPC; break;

                case EntityType.PLAYER: _p = Priority.PLAYER; break;
            }
            CM.AddSequentialTask(new MoveTask() { Entity = entity, Face = Util.LHQToFace(entity.IdealRotation), StartClock = startClock, Priority = _p });
            GetComponent<EntityAI>().EntityClock += entity.Data.FindParameterFloat("TranslationTime") * SpeedModifier;
        }
        public class MoveTask : IClockTask {
            public Priority Priority { get; set; }
            public Entity Entity;
            public int Face;
            public float StartClock { get; set; }

            public IEnumerator Run() {
                var em = Entity.GetComponent<EntityMove>();
                var PositionBefore = Entity.IdealPosition;
                if (!em.CanMove(Util.FaceToLHVector(Face))) yield break;
                Entity.IdealPosition += Util.FaceToLHVector(Face);
                foreach (var x in em.OnMove) {
                    var r = x?.Invoke(PositionBefore, Entity.IdealPosition);
                    if (r != null)
                        yield return r;
                }

            }


        }

        public class RotateTask : IClockTask {
            public Priority Priority { get; set; }
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