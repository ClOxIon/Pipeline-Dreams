using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "ItData", menuName = "ScriptableObjects/ItemData")]
    public class ItemDataset : ScriptableObject, IPDDataSet
    {
        public List<PDData> DataSet { get {
                var d = new List<PDData>();
                    foreach (var x in dataSet)
                    d.Add(x);
                return d;
        } }
        
        [SerializeField] private List<ItemData> dataSet;
    }
    [System.Serializable]
    public class ItemData : PDData {
        [Tooltip("Check if the item actions of this item is not inherited.")] [SerializeField] private bool doNotInherit;
        [SerializeField] private List<string> itemActions;
        [SerializeField] private string defaultAction;

        /// <summary>
        /// This functions as an item action parser, from the name of an item.
        /// </summary>
        public string[] ItemActions 
        { 
            get{
                if (doNotInherit)
                    return itemActions.ToArray();
                else
                {
                    if(Name.StartsWith("ItemWeapon"))
                    return itemActions.Concat(BaseItemWeaponActions).ToArray();

                    return itemActions.Concat(BaseItemActions).ToArray();
                }
            } 
        }
        
        
        
        public string DefaultAction
        {
            get
            {
                if (doNotInherit)
                    return defaultAction;
                else
                {
                    if (Name.StartsWith("ItemWeapon"))
                        return BaseItemWeaponDefaultAction;

                    return BaseItemDefaultAction;
                }
            }
        }


        static string[] BaseItemActions = { };
        static string BaseItemDefaultAction = "";
        static string[] BaseItemWeaponActions = { };
        static string BaseItemWeaponDefaultAction = "";
    }
    
}