using System.Linq;
using UnityEngine;

namespace PipelineDreams {
    public class EntitySoul : MonoBehaviour {
        Entity entity;
        EntitySoulCollector PI;
        [SerializeField] EntityDataContainer entityDataContainer;
        [SerializeField] int number;
        readonly float chance = 0.5f;
        private void Awake() {
            
            entity = GetComponent<Entity>();
            entity.OnEntityDeath += EntitySoul_OnEntityDeath;
        }

        private void EntitySoul_OnEntityDeath(Entity obj) {

            PI = entityDataContainer.FindEntitiesOfType(EntityType.PLAYER).FirstOrDefault()?.GetComponent<EntitySoulCollector>();
            if (PI == null)
            {
                Debug.LogWarning("EntitySoulCollector is not Assigned!");
                return;
            }
            for(int i = 0;i<number;i++)
                PI.AddSoul(UnityEngine.Random.value < chance ? 1 : 0);//Binomial Distribution
        }

       
    }
}