using System.Collections;
using System.Linq;
using UnityEngine;
using System;
using System.Collections.Generic;
using PipelineDreams.MutableValue;
namespace PipelineDreams.Entity {
    /// <summary>
    /// This sight module can rotate independently from the holder entity.
    /// </summary>
    public class SightWithRotation : Sight {

        /// <summary>
        /// The gameobject with the camera corresponding to this module attached. 
        /// </summary>
        [SerializeField] public Transform SightTransform;
        /// <summary>
        /// mutable Rotation time.
        /// </summary>
        public FunctionChain RTimeModifier = new FunctionChain();
        [SerializeField] float rTime;
        public Quaternion IdealRotation { get; private set; }
        protected override void Entity_OnInit(TaskManager arg1, Container arg3)
        {
            CM = arg1;
            ec = arg3;
            IdealRotation = entity.IdealRotation;
            SightTransform.rotation = entity.IdealRotation;
        }
        public void SubscribeOnRotate(Func<Quaternion, Quaternion, IEnumerator> x)
        {
            OnRotate.Add(x);
        }
        public void UnsubscribeOnRotate(Func<Quaternion, Quaternion, IEnumerator> x)
        {
            OnRotate.Remove(x);
        }
        /// <summary>
        /// Orientation before, Orientation after, Coroutine
        /// </summary>
        List<Func<Quaternion, Quaternion, IEnumerator>> OnRotate = new List<Func<Quaternion, Quaternion, IEnumerator>>();
        public virtual void Face(int f, float startClock)
        {
            TaskPriority _p = TaskPriority.PLAYER;
            switch (entity.Data.Type)
            {
                case EntityType.ENEMY: _p = TaskPriority.ENEMY; break;

                case EntityType.NPC: _p = TaskPriority.NPC; break;

                case EntityType.PLAYER: _p = TaskPriority.PLAYER; break;
            }
            CM.AddSequentialTask(new SightRotateTask() { Entity = entity, deltaQ = Util.RotateToFace(f, IdealRotation), StartClock = startClock, Priority = _p });

            GetComponent<AI>().EntityClock += rTime;

        }
        /// <summary>
        /// TODO: Consider the rotation of the sight module
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override bool IsVisible(Entity e) {
            return ec.FindLineOfSightEntityOnAxis(Util.LHQToFace(IdealRotation), entity) == e;
        }
        class SightRotateTask : IClockTask
        {
            public TaskPriority Priority { get; set; }
            public Entity Entity;
            public Quaternion deltaQ;
            public float StartClock { get; set; }

            public IEnumerator Run()
            {
                if (deltaQ == Quaternion.identity) yield break;

                var sr = Entity.GetComponent<SightWithRotation>();
                var RotationBefore = sr.IdealRotation;
                sr.IdealRotation = sr.IdealRotation * deltaQ;//Do not use *= here!
                foreach (var x in sr.OnRotate)
                {
                    var r = x?.Invoke(RotationBefore, sr.IdealRotation);
                    if (r != null)
                        yield return r;
                }
            }


        }
        private void Update()
        {
            SightTransform.position = transform.position;
        }
    }
}