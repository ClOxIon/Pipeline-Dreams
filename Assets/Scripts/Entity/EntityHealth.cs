using System;
using UnityEngine;

namespace PipelineDreams {
    public class EntityHealth : MonoBehaviour {
        int MaxHP;
        int CurrentHP;
        public event Action<float> OnHpModified;
        public event Action<DamagePacket> OnDamagePacketEvaluation;
        public event Action<int, int, Entity> OnDamagedAmount;
        public event Action OnZeroHP;
        Entity entity;
        // Start is called before the first frame update
        private void Awake() {

            entity = GetComponent<Entity>();
            entity.OnInit += (tm, mc, ec) => { MaxHP = entity.Data.MaxHP; CurrentHP = MaxHP; OnHpModified?.Invoke((float)CurrentHP / MaxHP); };

        }
        void Start() {
            OnHpModified?.Invoke((float)CurrentHP / MaxHP);

        }
        public virtual void RecieveDamage(DamagePacket dp) {
            OnDamagePacketEvaluation?.Invoke(dp);
            var _damage = UnityEngine.Random.Range(0, 1) < dp.accuracy.Value ? dp.damage.Value : 0;
            CurrentHP -= (int)(dp.damage.Value);

            OnDamagedAmount?.Invoke((int)(dp.damage.Value), MaxHP, dp.subject);
            OnHpModified?.Invoke((float)CurrentHP / MaxHP);
            if (CurrentHP <= 0) {
                CurrentHP = 0;
                OnZeroHP?.Invoke();
            }
        }


    }
}