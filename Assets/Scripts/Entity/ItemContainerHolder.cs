using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PipelineDreams.Entity {
    public class ItemContainerHolder : MonoBehaviour
    {
        [Tooltip("every entity, normally activated, will hold a unique copy of this Item Container.")] public Item.ContainerPlayer ItemContainer;

        [Tooltip("Share the assigned container, rather than copying it.")] public bool KeepPrefab;
        private void Awake()
        {
            GetComponent<Entity>().OnInit += (tm, ec) =>
            {
                if (!KeepPrefab)
                    ItemContainer = Instantiate(ItemContainer);
                ItemContainer.Init(tm, GetComponent<Entity>());
            };
        }
    }
}