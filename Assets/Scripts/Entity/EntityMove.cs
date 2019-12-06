using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EntityMove : MonoBehaviour
{
    public float SpeedModifier = 1;
    Entity entity;
    ClockManager CM;
    PlayerMove PC;
    MapManager mManager;
    EntityManager EM;
    private void Awake() {
        entity = GetComponent<Entity>();
        CM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ClockManager>();
        mManager = CM.GetComponent<MapManager>();
        PC = CM.GetComponent<PlayerMove>();
        EM = CM.GetComponent<EntityManager>();
    }
    public bool CanMove(Vector3Int UVector) {
        if (UVector != Util.LHQToLHUnitVector(entity.IdealRotation)) return false;
        if (!Util.CompareTiles(mManager.GetTileRelative(Vector3Int.zero, Util.LHQToFace(entity.IdealRotation), entity), Tile.hole)) return false;
        if (EM.FindEntityInRelativePosition(UVector, entity) != null)return false;
        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="q">Absolute Quaternion to rotate the object</param>
    /// <returns></returns>
    public bool CanRotate(Quaternion q) {
        var q0 = Quaternion.Inverse(entity.IdealRotation) * q;
        if (q0==Util.TurnDown|| q0 == Util.TurnUp || q0 == Util.TurnLeft || q0 == Util.TurnRight )
        return true;
        return false;
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="f"></param>
    /// <param name="startClock"></param>

    public virtual void Face(int f, float startClock) {
        var speed = 2f;
        if (entity.Type == EntityType.ENEMY&& !mManager.IsLineOfSight(entity.IdealPosition, EM.Player.IdealPosition)) speed = 0f;
        CM.AddSequentialTask(new RotateTask() { Entity = entity, q = Util.RotateToFace(f, entity.IdealRotation), StartClock = startClock, Priority = (int)entity.Type, Speed = speed, HasIteration = speed!=0});
        GetComponent<EntityAI>().EntityClock += 50* SpeedModifier;

    }
    public virtual void MoveToward(Vector3Int v, float startClock) {
        var speed = 2f;
        if (entity.Type == EntityType.ENEMY && !mManager.IsLineVisible(EM.Player.IdealPosition, entity.IdealPosition, entity.IdealPosition + Util.LHQToLHUnitVector(entity.IdealRotation))) speed = 0f;
        CM.AddSequentialTask(new MoveTask() { Entity = entity, Face = Util.LHQToFace(entity.IdealRotation), StartClock =  startClock, Priority = (int)entity.Type, Speed = speed, HasIteration = speed != 0 });
        GetComponent<EntityAI>().EntityClock += 100* SpeedModifier;
    }
    public class MoveTask : IClockTask {
        public int Priority { get; set; }
        public Entity Entity;
        public int Face;
        public float Speed = 2;
        public bool HasIteration { get; set; } = true;
        public float StartClock { get; set; }

        public IEnumerator Run() {
            var mManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MapManager>();
            if (!Entity.GetComponent<EntityMove>().CanMove(Util.LHQToLHUnitVector(Entity.IdealRotation))) yield break;
            Entity.IdealPosition += Util.FaceToLHVector(Face);
            var v0 = Entity.transform.localPosition;
            var v1 = (Vector3)Entity.IdealPosition * SceneObjectManager.worldscale;
            if ((v0 - v1).magnitude<1) yield break;
            float ratio = 0;
            while (ratio < 1 && Speed != 0) {
                Entity.GetComponent<EntityAnimator>()?.InvokeAnimation("Walk", true);
                Entity.transform.localPosition = Vector3.Lerp(v0, v1, ratio);
                ratio += Speed * Time.deltaTime;
                yield return null;

            }
            Entity.GetComponent<EntityAnimator>()?.InvokeAnimation("Walk", false);
            Entity.transform.localPosition = v1;

        }

        
    }

    public class RotateTask : IClockTask {
        public int Priority { get; set; }
        public Entity Entity;
        public Quaternion q;
        public float Speed = 2;
        public bool HasIteration { get; set; } = true;
        public float StartClock { get; set; }

        public IEnumerator Run() {
            if (!Entity.GetComponent<EntityMove>().CanRotate(Entity.IdealRotation*q)) yield break;
            Entity.IdealRotation = Entity.IdealRotation * q;
            var q0 = Entity.transform.localRotation;
            var q1 = Entity.IdealRotation;
            if (q0==q1) yield break;
            float ratio = 0;
            while (ratio < 1&&Speed!=0) {
                Entity.GetComponent<EntityAnimator>()?.InvokeAnimation("Rotate", true);
                Entity.transform.localRotation = Quaternion.Slerp(q0, q1, ratio);

                ratio += Speed * Time.deltaTime;
                yield return null;
            }
            Entity.GetComponent<EntityAnimator>()?.InvokeAnimation("Rotate", false);
            Entity.transform.localRotation = q1;

        }

        
    }
    
}
