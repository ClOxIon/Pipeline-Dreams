using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatusCanvas : MonoBehaviour
{
    [SerializeField] EntityStatusBar ESBPrefab;
    List<EntityStatusBar> ESBList = new List<EntityStatusBar>();
    EntityManager EM;
    PlayerController PC;
    ClockManager CM;
    MapManager mManager;
    // Start is called before the first frame update
    private void Awake() {
        EM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EntityManager>();
        PC = EM.GetComponent<PlayerController>();
        CM = EM.GetComponent<ClockManager>();
        mManager = EM.GetComponent<MapManager>();
        EM.OnNewEntitySpawn += (e) => {
            var obj = Instantiate(ESBPrefab,transform,true);
            ESBList.Add(obj);
            obj.Init(e);
            obj.Show(false);


        };
        EM.OnEntityDeath += (e) => {
            var obj = ESBList.Find((x) => { return x.entity == e; });
            ESBList.Remove(obj);
            Destroy(obj.gameObject);


        };
        EM.OnPlayerReferenceSet += () => {
            EM.Player.GetComponent<EntityDeath>().OnEntityDeath += (e) => {
                foreach (var obj in ESBList)
                    obj.enabled = false;
                Debug.Log("hide!");
            };
        };

        
        CM.OnTaskEnd += ESBVisibilityRefresh;
        
    }

    private void ESBVisibilityRefresh() {
        foreach (var obj in ESBList) {
            
            obj.Show(mManager.IsLineOfSight(obj.entity.IdealPosition, EM.Player.IdealPosition));//line of sight
        }
    }


}
