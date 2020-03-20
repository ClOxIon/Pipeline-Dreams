using System;
using UnityEngine;

namespace PipelineDreams.Entity {
    /// <summary>
    /// This base class is only used for the player entity.
    /// </summary>
    public class WeaponHolder : MonoBehaviour {
        protected Entity entity;
        protected TaskManager CM;
        protected Item.Weapon.Weapon weapon;
        public event Action<Item.Weapon.Weapon> OnRefreshWeapon;
        /// <summary>
        /// Subscribe to this event to modify damage packet.
        /// </summary>
        public event Action<DamagePacket> OnDamagePacketDepart;
        protected virtual void Awake() {
            entity = GetComponent<Entity>();
            entity.OnInit += Entity_OnInit;
        }

        private void Entity_OnInit(TaskManager arg1, Container arg3)
        {
            CM = arg1;
        }

        public void InvokeRefresh() {
            OnRefreshWeapon?.Invoke(weapon);
        }
        /// <summary>
        /// Changes current weapon to new weapon. Returns unequiped weapon.
        /// </summary>
        /// <param name="newWeapon"></param>
        /// <returns>Unequiped weapon</returns>
        public Item.Weapon.Weapon SetWeapon(Item.Weapon.Weapon newWeapon)
        {
            Item.Weapon.Weapon tmp = weapon;
            if (weapon != null)
                weapon.Unequip();      
            weapon = newWeapon;
            weapon.Equip();
            return tmp;
        }
        /// <summary>
        /// Attacks the target specified.
        /// </summary>
        /// <param name="e">Target entity</param>
        /// <param name="startClock">start clock of the action</param>
        /// <param name="meleeCoef">melee damage coefficient</param>
        /// <param name="rangeCoef">range damage coefficient</param>
        /// <param name="fieldCoef">field damage coefficient</param>
        public virtual void PerformAttack(Entity e, float startClock, float meleeCoef, float rangeCoef, float fieldCoef, float accuracy) {
            if (e == null|| e.GetComponent<Health>()==null) {//Failed to find a target
                return;
            }
            if (weapon != null)
            {
                var damage = new MutableValue.FunctionChain();
                damage.AddFunction(new MutableValue.Constant() { Value = weapon.MeleeDamage * meleeCoef + weapon.RangeDamage * rangeCoef + weapon.FieldDamage * fieldCoef });
                damage.EvalAtNextGet = true;
                var acc = new MutableValue.FunctionChain();
                acc.AddFunction(new MutableValue.Constant() { Value = accuracy });
                acc.EvalAtNextGet = true;
                var dp = new DamagePacket() { damage = damage, subject = entity, accuracy = acc };
                OnDamagePacketDepart?.Invoke(dp);
                e.GetComponent<Health>().RecieveDamage(dp);
            }
            else
                Debug.LogWarning("Attack performed without a weapon: " + entity.Data.Name);

        }
        public Item.Data WeaponData => weapon?.Data as Item.Data;

    }
}