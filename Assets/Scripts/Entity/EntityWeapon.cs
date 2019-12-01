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
    private void Awake() {
        entity = GetComponent<Entity>();
        CM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ClockManager>();
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
        if(weapon!=null)
        e.GetComponent<EntityHealth>()?.RecieveDamage((int)(weapon.MeleeDamage * meleeCoef+ weapon.RangeDamage * rangeCoef+ weapon.FieldDamage * fieldCoef), entity);
        
    }
    
}
