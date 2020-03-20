using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PipelineDreams.Entity {
    public class InitialWeapon : MonoBehaviour
    {
        [SerializeField] string InitialWeaponName;
        [SerializeField] Item.Dataset DataContainer;
        // Start is called before the first frame update
        void Awake()
        {
            var entity = GetComponent<Entity>();
            entity.OnInit += (tm, ec) =>
          {
              Item.Weapon.Weapon AddedItem = null;
              Item.Data AddedItemData;
              AddedItemData = DataContainer.DataSet.Find((x) => { return x.Name == "Weapon." + InitialWeaponName; }) as Item.Data;
              if (AddedItemData == null)
              {
                  Debug.LogError("ItemCollection.Additem(): Cannot find item named " + "Weapon." + InitialWeaponName);
                  return;
              }

              if (Type.GetType(typeof(Item.Weapon.Weapon).Namespace + "." + InitialWeaponName) != null)
              {
                  AddedItem = (Item.Weapon.Weapon)Activator.CreateInstance(Type.GetType(typeof(Item.Weapon.Weapon).Namespace + "." + InitialWeaponName));
                  AddedItem.Init(AddedItemData);
                  AddedItem.Obtain(entity, tm);
              }
              else
              {
                  Debug.LogError("ItemCollection.Additem(): Cannot find class named " + typeof(Item.Weapon.Weapon).Namespace + "." + InitialWeaponName);

              }
              
              GetComponent<WeaponHolder>().SetWeapon(AddedItem);

            };
        }

        
    }
}