using System;
using UnityEngine;

namespace PipelineDreams {
    /// <summary>
    /// This base class is only used for the player entity.
    /// </summary>
    public class EntityWeapon : MonoBehaviour {
        protected Entity entity;
        protected TaskManager CM;
        protected ItemWeapon weapon;
        public event Action<ItemWeapon> OnRefreshWeapon;
        protected virtual void Awake() {
            entity = GetComponent<Entity>();
            entity.OnInit += Entity_OnInit;
        }

        private void Entity_OnInit(TaskManager arg1, MapDataContainer arg2, EntityDataContainer arg3)
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
        public ItemWeapon SetWeapon(ItemWeapon newWeapon)
        {
            ItemWeapon tmp = weapon;
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
            if (e == null|| e.GetComponent<EntityHealth>()==null) {//Failed to find a target
                return;
            }
            if (weapon != null)
                e.GetComponent<EntityHealth>().RecieveDamage(new DamagePacket() { damage = (int)(weapon.MeleeDamage * meleeCoef + weapon.RangeDamage * rangeCoef + weapon.FieldDamage * fieldCoef), subject=entity, accuracy = accuracy});

        }
        public ItemData WeaponData => weapon?.ItData;

    }
}