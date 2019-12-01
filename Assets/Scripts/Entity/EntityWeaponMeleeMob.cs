using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWeaponMeleeMob : EntityWeapon
{
    public override bool CanAttack(Entity e) {
        var v = e.IdealPosition - entity.IdealPosition;
        //Debug.Log("CanAttack : " + v.x + "," + v.y + "," + v.z+","+Util.LHQToFace(entity.IdealRotation)+ "," + Util.LHUnitVectorToFace(v));
        return v.magnitude <= 1 && Util.LHQToFace(entity.IdealRotation) == Util.LHUnitVectorToFace(v);
    }
    public override void TryAttack(Entity e, float startClock, float meleeCoef, float rangeCoef, float fieldCoef) {
        CM.AddSequentialTask(new MeleeAttackTask() { Attacker = entity, Target = e, damage = entity.Data.Damage, StartClock = startClock, Priority = (int)entity.Type });
        GetComponent<EntityAI>().EntityClock += 100;
    }
    public class MeleeAttackTask : IClockTask {
        public int Priority { get; set; }
        public Entity Attacker;
        public Entity Target;
        public bool HasIteration { get; } = true;
        public float Speed = 1f;
        public int damage = 0;

        public float StartClock { get; set; }

        public IEnumerator Run() {

            try { Target.GetComponent<EntityHealth>().RecieveDamage(damage, Attacker); }
            catch (MissingReferenceException e) {
                yield break;
            }
            //var v0 = Entity.transform.localPosition;
            //var v1 = (Vector3)Entity.IdealPosition * SceneObjectManager.worldscale;
            float ratio = 0;
            Attacker.GetComponent<EntityAnimator>()?.InvokeAnimation("MeleeAttack", true);
            while (ratio < 1) {

                //Entity.transform.localPosition = Vector3.Lerp(v0, v1, ratio);
                ratio += Speed * Time.deltaTime;
                yield return null;

            }
            Attacker.GetComponent<EntityAnimator>()?.InvokeAnimation("MeleeAttack", false);


        }


    }
}
