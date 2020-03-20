using System;
using System.Collections;
using UnityEngine;

namespace PipelineDreams.Entity {
    public class Animator : MonoBehaviour {
        UnityEngine.Animator an;
        public event Action<string, bool> OnAnimate;
        public Action OnDeathClipExit;
        [SerializeField] float RSpeed = 2f;
        [SerializeField] float TSpeed = 2f;
        Entity entity;
        Container EC;
        private void Awake() {
            entity = GetComponent<Entity>();
            entity.OnInit += Entity_OnInit;
            an = GetComponent<UnityEngine.Animator>();
            var x = GetComponent<Move>();
            if (x != null)
            {
                x.SubscribeOnMove(AnimateTranslation);
                x.SubscribeOnRotate(AnimateRotation);
            }

        }

        private void Entity_OnInit(TaskManager tm, Container ec) {
            EC = ec;
            transform.localPosition = GraphicalConstants.WORLDSCALE * (Vector3)entity.IdealPosition;
            transform.localRotation = entity.IdealRotation;
        }
        /// <summary>
        /// Animates only when the entity is visible by a player.
        /// </summary>
        /// <returns></returns>
        bool IsSeenByPlayer() {
            if(entity.Data.Type==EntityType.PLAYER)
                return true;//Player can 'see' itself
            bool flag = false;
            foreach (var x in EC.FindEntitiesOfType(EntityType.PLAYER))
                flag |= x.GetComponent<Sight>().IsVisible(entity);
            return flag;
        }

        public IEnumerator AnimateRotation(Quaternion q0, Quaternion q1) {
            
            if (IsSeenByPlayer())
            {
                InvokeAnimation("Rotate", true);
                var ratio = 0f;
                while (ratio < 1 && RSpeed != 0)
                {

                    transform.localRotation = Quaternion.Slerp(q0, q1, ratio);

                    ratio += RSpeed * Time.deltaTime;
                    yield return null;
                }
                InvokeAnimation("Rotate", false);
            }
            transform.localRotation = q1;
        }
        public IEnumerator AnimateTranslation(Vector3Int x0, Vector3Int x1) {
            
            if (IsSeenByPlayer())
            {
                InvokeAnimation("Move", true);
                var ratio = 0f;
                while (ratio < 1 && TSpeed != 0)
                {

                    transform.localPosition = GraphicalConstants.WORLDSCALE * Vector3.Lerp(x0, x1, ratio);

                    ratio += TSpeed * Time.deltaTime;
                    yield return null;
                }
                InvokeAnimation("Move", false);

            }
            transform.localPosition = GraphicalConstants.WORLDSCALE * (Vector3)x1;
        }
        public void InvokeAnimation(string name, bool b) {
            OnAnimate?.Invoke(name, b);
            an.SetBool(name, b);
        }
    }
}