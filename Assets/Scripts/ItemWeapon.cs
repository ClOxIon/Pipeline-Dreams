using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemWeapon : Item
{
    public override string[] ItemActions => base.ItemActions.Concat(new string[] {"Equip","UnEquip" }).ToArray();
    public int MeleeDamage { get; protected set; }

    public int RangeDamage { get; protected set; }

    public int FieldDamage { get; protected set; }
    // Start is called before the first frame update
    public override void Obtain(ItemData data) {
        base.Obtain(data);
        MeleeDamage = data.FindParameterInt("MeleeDamage");
        RangeDamage = data.FindParameterInt("RangeDamage");
        FieldDamage = data.FindParameterInt("FieldDamage");
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
