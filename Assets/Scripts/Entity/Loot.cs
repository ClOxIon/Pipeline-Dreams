﻿using System.Linq;
using UnityEngine;

namespace PipelineDreams.Entity {
    public class Loot : MonoBehaviour {
        Entity entity;
        Item.ContainerPlayer PI;
        [SerializeField] Container entityDataContainer;
        private void Awake() {
            
            entity = GetComponent<Entity>();
            entity.OnEntityDeath += EntityLoot_OnEntityDeath;
        }

        private void EntityLoot_OnEntityDeath(Entity obj) {

            PI = entityDataContainer.FindEntitiesOfType(EntityType.PLAYER).FirstOrDefault()?.GetComponent<ItemContainerHolder>().ItemContainer;
            if (PI == null)
            {
                Debug.LogWarning("PlayerItem is not Assigned!");
                return;
            }
            if (Random.Range(0, 1) < entity.Data.FindParameterFloat("LootChance"))
                PI.AddItem(entity.Data.FindParameterString("Loot"));
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}