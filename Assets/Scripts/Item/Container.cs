using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Item
{

    [CreateAssetMenu(fileName = "ItemContainer", menuName = "ScriptableObjects/Manager/ItemContainer")]
    public class Container : PDObjectContainer<Item> {
        int MaximumItemCount = 10;
    }
}