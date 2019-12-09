using System;
using System.Collections;
using UnityEngine;

namespace PipelineDreams {
    public class EntityAnimator : MonoBehaviour {
        Animator an;
        public event Action<string, bool> OnAnimate;
        public Action OnDeathClipExit;
        [SerializeField] float RSpeed = 2f;
        [SerializeField] float TSpeed = 2f;
        Entity entity;
        [SerializeField] Entity Player;

        [SerializeField] MapDataContainer mManager;
        private void Awake() {
            entity = GetComponent<Entity>();
            entity.OnInit += Entity_OnInit;
            an = GetComponent<Animator>();
            var x = GetComponent<EntityMove>();
            x?.AddMoveTask(AnimateTranslation);
            x?.AddRotateTask(AnimateRotation);

        }

        private void Entity_OnInit() {
            transform.localPosition = GraphicalConstants.WORLDSCALE * (Vector3)entity.IdealPosition;
            transform.localRotation = entity.IdealRotation;
        }


        public IEnumerator AnimateRotation(Quaternion q0, Quaternion q1) {
            if (entity.Type == EntityType.ENEMY && !mManager.IsLineVisible(Player.IdealPosition, entity.IdealPosition, entity.IdealPosition + Util.LHQToLHUnitVector(entity.IdealRotation)))
                yield break;
            InvokeAnimation("Rotate", true);
            var ratio = 0f;
            while (ratio < 1 && RSpeed != 0) {

                transform.localRotation = Quaternion.Slerp(q0, q1, ratio);

                ratio += RSpeed * Time.deltaTime;
                yield return null;
            }
            InvokeAnimation("Rotate", false);
            transform.localRotation = q1;
        }
        public IEnumerator AnimateTranslation(Vector3Int x0, Vector3Int x1) {

            if (entity.Type == EntityType.ENEMY && !mManager.IsLineOfSight(entity.IdealPosition, Player.IdealPosition))
                yield break;
            InvokeAnimation("Move", true);
            var ratio = 0f;
            while (ratio < 1 && TSpeed != 0) {

                transform.localPosition = GraphicalConstants.WORLDSCALE * Vector3.Lerp(x0, x1, ratio);

                ratio += TSpeed * Time.deltaTime;
                yield return null;
            }
            InvokeAnimation("Move", false);
            transform.localPosition = GraphicalConstants.WORLDSCALE * (Vector3)x1;
        }
        public void InvokeAnimation(string name, bool b) {
            OnAnimate?.Invoke(name, b);
            an.SetBool(name, b);
        }
    }
}