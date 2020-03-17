using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    public class EntityStatusCanvas : MonoBehaviour {
        [SerializeField] EntityStatusBar ESBPrefab;
        List<EntityStatusBar> ESBList = new List<EntityStatusBar>();
        [SerializeField] EntityDataContainer EM;
        [SerializeField] TaskManager CM;
        [SerializeField] Entity Player;
        // Start is called before the first frame update
        private void Awake() {
            EM.OnNewEntitySpawn += (e) => {
                var obj = Instantiate(ESBPrefab, transform, true);
                ESBList.Add(obj);
                obj.Init(e);
                obj.Show(false);


            };
            EM.OnEntityDeath += (e) => {
                e.GetComponent<EntityAnimator>().OnDeathClipExit += () => {
                    var obj = ESBList.Find((x) => { return x.entity == e; });
                    ESBList.Remove(obj);
                    Destroy(obj.gameObject);
                };

            };
            ///Hides all ESB when the player is dead, since it will cause exceptions.
            Player.OnEntityDeath += (e) => {
                foreach (var obj in ESBList)
                    obj.enabled = false;
            };



            CM.OnTaskEnd += ESBVisibilityRefresh;

        }

        private void ESBVisibilityRefresh() {
            foreach (var obj in ESBList) {

                obj.Show(EM.IsLineOfSight(obj.entity.IdealPosition, Player.IdealPosition));//line of sight
            }
        }


    }
}