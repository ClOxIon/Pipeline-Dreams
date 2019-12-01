using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : Item
{
    public float MeleeDamage { get; protected set; }

    public float RangeDamage { get; protected set; }

    public float FieldDamage { get; protected set; }
    // Start is called before the first frame update
    public override void Activate(ItemData data) {
        base.Activate(data);
        MeleeDamage = data.value1;
        RangeDamage = data.value1;
        FieldDamage = data.value1;
    }
}
