using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLoot : MonoBehaviour
{
    Entity entity;
    PlayerItem PI;
    private void Awake() {
        GetComponent<EntityDeath>().OnEntityDeath += EntityLoot_OnEntityDeath;
        entity = GetComponent<Entity>();
        PI = (PlayerItem)FindObjectOfType(typeof(PlayerItem));
    }

    private void EntityLoot_OnEntityDeath(Entity obj) {
        if(Random.Range(0,1)<entity.Data.lootChance)
        PI.AddItem(entity.Data.loot);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
