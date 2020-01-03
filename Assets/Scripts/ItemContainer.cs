using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{

    [CreateAssetMenu(fileName = "ItemContainer", menuName = "ScriptableObjects/Manager/ItemContainer")]
    public class ItemContainer : PDObjectContainer<Item> {
        int MaximumItemCount = 10;
    }
}