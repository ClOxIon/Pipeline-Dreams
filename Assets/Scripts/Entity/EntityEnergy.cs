using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{

    public class EntityEnergy : MonoBehaviour
    {

        Entity entity;
        /// <summary>
        /// Called when damaged by another entity. Amount, and the attacker.
        /// </summary>
        public event Action<float, Entity> OnDamaged;
        public event Action OnZeroEnergy;
        /// <summary>
        /// Called whenever a damagePacket arrives. This need not result in actual damage.
        /// </summary>
        public event Action<DamagePacket> OnDamagePacketArrive;
        // Start is called before the first frame update
        private void Awake() {

            entity = GetComponent<Entity>();
            entity.OnInit += (tm, ec) => {
                entity.Parameters.Add("Energy", entity.Data.MaxHP);
                var f = new MutableValue.FunctionChain();
                f.OnEvalRequest += () => f.AddFunction(new MutableValue.Constant() { Value = entity.Data.FindParameterFloat("MaxEnergy") });
                f.EvalAtNextGet = true;
                entity.Stats.Add("MaxEnergy", f);

            };
            entity.OnParamChange += Entity_OnParamChange;

        }

        private void Entity_OnParamChange(string name, float val) {
            if (name != "Energy") return;
            if (val == 0) OnZeroEnergy?.Invoke();
        }

        public virtual void RecieveDamage(DamagePacket dp) {
            OnDamagePacketArrive?.Invoke(dp);
            var _damage = UnityEngine.Random.Range(0, 1) < dp.accuracy.Value ? dp.damage.Value : 0;
            entity.Parameters["Energy"] -= _damage;


            OnDamaged?.Invoke(dp.damage.Value, dp.subject);

        }
    }
}