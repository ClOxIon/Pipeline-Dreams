using UnityEngine;

namespace PipelineDreams {
    public class EntitySight : MonoBehaviour {
        Entity entity;
        [SerializeField] MapDataContainer mManager;
        public virtual bool IsVisible(Entity e) {
            var v = e.IdealPosition - entity.IdealPosition;
            return mManager.IsLineOfSight(entity.IdealPosition, e.IdealPosition) && Util.LHQToLHUnitVector(entity.IdealRotation) == Util.Normalize(v);
        }

        private void Awake() {
            entity = GetComponent<Entity>();
        }
    }
}