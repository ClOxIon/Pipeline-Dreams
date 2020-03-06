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
            
                  if (Type.GetType("Item" + name) != null)
                      AddedItem = (Item)Activator.CreateInstance(Type.GetType("Item" + name), entity, tm, AddedItemData);
              
              GetComponent<EntityWeapon>().SetWeapon((ItemWeapon)AddedItem);

            };
        }

        
    }
}