using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : Item
{
    public float MeleeDamage { get; protected set; }

    public float RangeDamage { get; protected set; }

    public float FieldDamage { get; protected set; }
    // Start is called before the first frame update
    public override void Obtain(ItemData data) {
        base.Obtain(data);
        MeleeDamage = data.value1;
        RangeDamage = data.value1;
        FieldDamage = data.value1;
    }
    /// <summary>
    /// Called when the weapon is equipped.
    /// </summary>
    public virtual void Equip() {
        
    }
    /// <summary>
    /// Called when the weapon is unequipped.
    /// </summary>
    public virtual void Unequip() {

    }
}
