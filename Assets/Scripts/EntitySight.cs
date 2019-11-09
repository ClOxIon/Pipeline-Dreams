using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySight : MonoBehaviour
{
    Entity entity;
    MapManager mManager;
    public virtual bool IsVisible(Entity e) {
        var v = e.IdealPosition - entity.IdealPosition;
        return mManager.IsLineOfSight(entity.IdealPosition, e.IdealPosition)&&Util.LHQToLHUnitVector(entity.IdealRotation)==Util.Normalize(v);
    }

    private void Awake() {
        entity = GetComponent<Entity>();
        mManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MapManager>();
    }
}
