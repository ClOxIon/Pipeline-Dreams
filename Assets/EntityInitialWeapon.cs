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
        void Start()
        {
            var entity = GetComponent<Entity>();
            entity.OnInit += (tm, mc, ec) =>
          {
              Item AddedItem;
              ItemData AddedItemData;
              AddedItemData = DataContainer.Dataset.Find((x) => { return x.Name == InitialWeaponName; });
              if (AddedItemData == null)
              {
                  Debug.LogError("ItemCollection.Additem(): Cannot find item named " + InitialWeaponName);
                  return;
              }
              AddedItem = new Item(entity, tm, AddedItemData);//If the item class for the specific item is not defined, we will just use the base class.
            if (typeof(Item).Namespace != null)
              {
                  if (Type.GetType(typeof(Item).Namespace + ".Item" + name) != null)
                      AddedItem = (Item)Activator.CreateInstance(Type.GetType(typeof(Item).Namespace + ".Item" + name), entity, tm, AddedItemData);
              }
              else
              {
                  if (Type.GetType("Item" + name) != null)
                      AddedItem = (Item)Activator.CreateInstance(Type.GetType("Item" + name), entity, tm, AddedItemData);
              }

              GetComponent<EntityWeapon>().SetWeapon((ItemWeapon)AddedItem);

            };
        }

        
    }
}