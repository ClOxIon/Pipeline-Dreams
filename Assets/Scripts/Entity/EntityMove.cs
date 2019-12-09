using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    public class EntityMove : MonoBehaviour {
        public float SpeedModifier = 1;
        Entity entity;
        [SerializeField] TaskManager CM;
        [SerializeField] EntityDataContainer EM;
        [SerializeField] MapDataContainer mManager;
        List<Func<Vector3Int, Vector3Int, IEnumerator>> OnMove = new List<Func<Vector3Int, Vector3Int, IEnumerator>>();
        public void AddMoveTask(Func<Vector3Int, Vector3Int, IEnumerator> x) {
            OnMove.Add(x);
        }
        public void DelMoveTask(Func<Vector3Int, Vector3Int, IEnumerator> x) {
            OnMove.Remove(x);
        }
        List<Func<Quaternion, Quaternion, IEnumerator>> OnRotate = new List<Func<Quaternion, Quaternion, IEnumerator>>();
        public void AddRotateTask(Func<Quaternion, Quaternion, IEnumerator> x) {
            OnRotate.Add(x);
        }
        public void DelRotateTask(Func<Quaternion, Quaternion, IEnumerator> x) {
            OnRotate.Remove(x);
        }

        private void Awake() {
            entity = GetComponent<Entity>();

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
            CM.AddSequentialTask(new RotateTask() { Entity = entity, deltaQ = Util.RotateToFace(f, entity.IdealRotation), StartClock = startClock, Priority = (int)entity.Type });
            GetComponent<EntityAI>().EntityClock += 50 * SpeedModifier;

        }
        public virtual void MoveToward(Vector3Int v, float startClock) {
            CM.AddSequentialTask(new MoveTask() { Entity = entity, Face = Util.LHQToFace(entity.IdealRotation), StartClock = startClock, Priority = (int)entity.Type });
            GetComponent<EntityAI>().EntityClock += 100 * SpeedModifier;
        }
        public class MoveTask : IClockTask {
            public int Priority { get; set; }
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
            public int Priority { get; set; }
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