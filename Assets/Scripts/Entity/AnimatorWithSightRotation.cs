using System;
using System.Collections;
using UnityEngine;

namespace PipelineDreams.Entity {
    /// <summary>
    /// This module is used with SightWithRotation.
    /// </summary>
    public class AnimatorWithSightRotation : Animator {
        [SerializeField] float SightRSpeed = 2f;
        SightWithRotation sr;
        protected override void Awake() {
            base.Awake();
            sr = GetComponent<SightWithRotation>();
            if (sr != null)
                sr.SubscribeOnRotate(AnimateSightRotation);
            else
                Debug.LogWarning($"No Entity.SightRotation found on this entity {entity.Data.Name}!");

        }

        public IEnumerator AnimateSightRotation(Quaternion q0, Quaternion q1) {
            
            if (IsSeenByPlayer())
            {
                InvokeAnimation("SightRotate", true);//I do not think this animation would be ever implemented, but still.
                var ratio = 0f;
                while (ratio < 1 && SightRSpeed != 0)
                {

                    sr.SightTransform.localRotation = Quaternion.Slerp(q0, q1, ratio);

                    ratio += SightRSpeed * Time.deltaTime;
                    yield return null;
                }
                InvokeAnimation("Rotate", false);
            }
            sr.SightTransform.localRotation = q1;
        }
        
    }
}