using System;
using UnityEngine;

namespace PipelineDreams {
    public class EntityHealth : MonoBehaviour {
        int MaxHP;
        int CurrentHP;
        public float damageRecieveCoef { get; set; } = 1f;
        public event Action<float> OnHpModified;

        public event Action<int, int, Entity> OnDamagedAmount;
        public event Action OnZeroHP;
        Entity entity;
        // Start is called before the first frame update
        private void Awake() {

            entity = GetComponent<Entity>();
            entity.OnInit += () => { MaxHP = entity.Data.MaxHP; CurrentHP = MaxHP; OnHpModified?.Invoke((float)CurrentHP / MaxHP); };

        }
        void Start() {
            OnHpModified?.Invoke((float)CurrentHP / MaxHP);

        }
        public virtual void RecieveDamage(int damage, Entity subject) {
            CurrentHP -= (int)(damageRecieveCoef * damage);

            OnDamagedAmount?.Invoke((int)(damageRecieveCoef * damage), MaxHP, subject);
            OnHpModified?.Invoke((float)CurrentHP / MaxHP);
            if (CurrentHP <= 0) {
                CurrentHP = 0;
                OnZeroHP?.Invoke();
            }
        }


    }
}