using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{

    [CreateAssetMenu(fileName = "ItemContainer", menuName = "ScriptableObjects/Manager/ItemContainerPlayer")]
    public class ItemContainerPlayer : PDObjectContainerLimitedCapacity<Item> {
        int MaximumItemCount = 10;

        public override void Init(TaskManager tm, Entity holder) {
            base.Init(tm, holder);
            for (int i = 0; i < MaximumItemCount; i++)
                objs.Add(null);
            ChangeCapacity(3);
            AddItem("WeaponRebar");
        }

        public void InvokeItemAction(int itemIndex, string actionName) {
            if (itemIndex < objs.Count && objs[itemIndex] != null)
                objs[itemIndex].InvokeItemAction(actionName);
        }
    }
}