using System;
using UnityEngine;

namespace PipelineDreams {
    public class EntityHealth : MonoBehaviour {
        MutableValue.FunctionChain MaxHP = new MutableValue.FunctionChain();
        float CurrentHP;
        public event Action<float> OnHpModified;
        public event Action<DamagePacket> OnDamagePacketEvaluation;
        public event Action<float, float, Entity> OnDamagedAmount;
        public event Action OnZeroHP;
        Entity entity;
        // Start is called before the first frame update
        private void Awake() {

            entity = GetComponent<Entity>();
            entity.OnInit += (tm, ec) => 
            {
                MaxHP.OnValueRequested += () => { MaxHP.AddFunction(new MutableValue.Constant(entity.Data.MaxHP)); };
                CurrentHP = MaxHP.Value; 
                OnHpModified?.Invoke(CurrentHP / MaxHP.Value); 
            };

        }
        public virtual void RecieveDamage(DamagePacket dp) {
            OnDamagePacketEvaluation?.Invoke(dp);
            var _damage = UnityEngine.Random.Range(0, 1) < dp.accuracy.Value ? dp.damage.Value : 0;
            CurrentHP -= (int)(dp.damage.Value);

            OnDamagedAmount?.Invoke(dp.damage.Value, MaxHP.Value, dp.subject);
            OnHpModified?.Invoke(CurrentHP / MaxHP.Value);
            if (CurrentHP <= 0) {
                CurrentHP = 0;
                OnZeroHP?.Invoke();
            }
        }


    }
}