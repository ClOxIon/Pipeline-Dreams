using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PipelineDreams {
    public class EntityInitialWeapon : MonoBehaviour
    {
        [SerializeField] string InitialWeaponName;
        [SerializeField] ItemDataset DataContainer;
        // Start is called before the first frame update
        void Awake()
        {
            var entity = GetComponent<Entity>();
            entity.OnInit += (tm, ec) =>
          {
              Item AddedItem = null;
              ItemData AddedItemData;
              AddedItemData = DataContainer.DataSet.Find((x) => { return x.Name == InitialWeaponName; }) as ItemData;
              if (AddedItemData == null)
              {
                  Debug.LogError("ItemCollection.Additem(): Cannot find item named " + InitialWeaponName);
                  return;
              }

              if (Type.GetType(typeof(Item).Namespace + "." + "Item" + InitialWeaponName) != null)
              {
                  AddedItem = (Item)Activator.CreateInstance(Type.GetType(typeof(Item).Namespace + "." + "Item" + InitialWeaponName));
                  AddedItem.Init(AddedItemData);
                  AddedItem.Obtain(entity, tm);
              }
              else
              {
                  Debug.LogError("ItemCollection.Additem(): Cannot find class named " + typeof(Item).Namespace + "." + "Item" + InitialWeaponName);

              }
              
              GetComponent<EntityWeapon>().SetWeapon((ItemWeapon)AddedItem);

            };
        }

        
    }
}