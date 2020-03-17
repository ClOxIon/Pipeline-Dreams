using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{

    public class EntityHealth : MonoBehaviour
    {

        Entity entity;
        /// <summary>
        /// Called when damaged by another entity. Amount, and the attacker.
        /// </summary>
        public event Action<float, Entity> OnDamaged;
        public event Action OnZeroHP;
        /// <summary>
        /// Called whenever a damagePacket arrives. This need not result in actual damage.
        /// </summary>
        public event Action<DamagePacket> OnDamagePacketArrive;
        // Start is called before the first frame update
        private void Awake() {

            entity = GetComponent<Entity>();
            entity.OnInit += (tm, ec) => {
                entity.Parameters.Add("HP", entity.Data.MaxHP);
                var f = new MutableValue.FunctionChain();
                f.OnEvalRequest += () => f.AddFunction(new MutableValue.Constant() { Value = entity.Data.MaxHP });
                f.EvalAtNextGet = true;
                entity.Stats.Add("MaxHP", f);

            };
            entity.OnParamChange += Entity_OnParamChange;

            OnZeroHP += entity.Death;
        }

        private void Entity_OnParamChange(string name, float val) {
            if (name != "HP") return;
            if (val == 0) OnZeroHP?.Invoke();
        }

        public virtual void RecieveDamage(DamagePacket dp) {
            OnDamagePacketArrive?.Invoke(dp);
            var _damage = UnityEngine.Random.Range(0, 1) < dp.accuracy.Value ? dp.damage.Value : 0;
            entity.Parameters["HP"] -= _damage;


            OnDamaged?.Invoke(dp.damage.Value, dp.subject);

        }
    }
}