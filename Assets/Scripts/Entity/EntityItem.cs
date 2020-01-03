using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PipelineDreams {
    public class EntityItem : MonoBehaviour
    {
        [Tooltip("every entity, normally activated, will hold a unique copy of this Item Container.")] public ItemContainerPlayer ItemContainer;

        [Tooltip("Share the assigned container, rather than copying it.")] public bool KeepPrefab;
        private void Awake()
        {
            GetComponent<Entity>().OnInit += (tm, mc, ec) =>
            {
                if (!KeepPrefab)
                    ItemContainer = Instantiate(ItemContainer);
                ItemContainer.Init(tm, GetComponent<Entity>());
            };
        }
    }
}