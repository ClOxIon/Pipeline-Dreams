using System;
using System.Collections;
using UnityEngine;

namespace PipelineDreams.Entity {
    public class Animator : MonoBehaviour {
        [SerializeField] UnityEngine.Animator an;

        /// <summary>
        /// animation clips or states could invoke this event during their playback.
        /// </summary>
        public Action<string> OnAnimEvent;
        [SerializeField] float RSpeed = 2f;
        [SerializeField] float TSpeed = 2f;
        protected Entity entity;
        protected Container EC;
        protected virtual void Awake() {
            entity = GetComponent<Entity>();
            entity.OnInit += Entity_OnInit;
            var x = GetComponent<Move>();
            if (x != null)
            {
                x.SubscribeOnMove(AnimateTranslation);
                x.SubscribeOnRotate(AnimateRotation);
            }
            else
                Debug.LogWarning($"No Entity.Move found on this entity {entity.Data.Name}!");
            

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
        protected bool IsSeenByPlayer() {
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
        /// <summary>
        /// To turn on and off animation clip. This does not lock the tackmanager.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="b"></param>
        public void InvokeAnimation(string name, bool b) {
            an.SetBool(name, b);
        }
        /// <summary>
        /// To trigger animation. To lock the taskmanager, wait for OnAnimEvent.
        /// </summary>
        /// <param name="name"></param>
        public void InvokeAnimation(string name)
        {
            an.SetTrigger(name);
           
        }
    }
}