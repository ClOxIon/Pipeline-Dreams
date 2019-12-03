using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This base class is only used for the player entity.
/// </summary>
public class EntityWeapon : MonoBehaviour
{
    protected Entity entity;
    protected ClockManager CM;
    protected ItemWeapon weapon;
    private ItemCollection IC;
    public event Action<ItemWeapon> OnRefreshWeapon;
    private void Awake() {
        entity = GetComponent<Entity>();
        CM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ClockManager>();
        IC = (ItemCollection)FindObjectOfType(typeof(ItemCollection));
        IC.OnItemCollectionInitialized += IC_OnItemCollectionInitialized;
    }

    private void IC_OnItemCollectionInitialized() {
        if (entity.Type == EntityType.PLAYER)
            SetWeapon(1);
    }

    void SetWeapon(int index) {
        if (weapon != null) {
            weapon.Unequip();
            IC.PushItem(weapon);
        }
        var i= IC.PopItem(index);

        weapon = (ItemWeapon)i;
        
        Debug.Log("Weaponname: " + weapon.ItData.Name);
        weapon.Equip();
        OnRefreshWeapon.Invoke(weapon);
    }
    /// <summary>
    /// For the player entity, this function is not used, since it is only used for AI state machines.
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public virtual bool CanAttack(Entity e) {
        return true;
    }
    /// <summary>
    /// Tries to attack a target
    /// </summary>
    /// <param name="e">Target entity</param>
    /// <param name="startClock">start clock of the action</param>
    /// <param name="meleeCoef">melee damage coefficient</param>
    /// <param name="rangeCoef">range damage coefficient</param>
    /// <param name="fieldCoef">field damage coefficient</param>
    public virtual void TryAttack(Entity e, float startClock, float meleeCoef, float rangeCoef, float fieldCoef) {
        if (e == null) {//Failed to find a target
            return; }
        if(weapon!=null)
        e.GetComponent<EntityHealth>()?.RecieveDamage((int)(weapon.MeleeDamage * meleeCoef+ weapon.RangeDamage * rangeCoef+ weapon.FieldDamage * fieldCoef), entity);
        
    }
    
}
