using System.Linq;
using UnityEngine;

namespace PipelineDreams.Entity {
    public class Soul : MonoBehaviour {
        Entity entity;
        SoulCollector PI;
        [SerializeField] Container entityDataContainer;
        [SerializeField] int number;
        readonly float chance = 0.5f;
        private void Awake() {
            
            entity = GetComponent<Entity>();
            entity.OnEntityDeath += EntitySoul_OnEntityDeath;
        }

        private void EntitySoul_OnEntityDeath(Entity obj) {

            PI = entityDataContainer.FindEntitiesOfType(EntityType.PLAYER).FirstOrDefault()?.GetComponent<SoulCollector>();
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