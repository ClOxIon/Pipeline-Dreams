using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCollectionUI : MonoBehaviour
{
    List<BuffUI> BuffUIs;
    EntityBuff PI;
    EntityManager EM;
    [SerializeField] BuffUI BuffUIPrefab;
    private void Awake() {
        EM = (EntityManager)FindObjectOfType(typeof(EntityManager));
        
        BuffUIs = new List<BuffUI>(GetComponentsInChildren<BuffUI>());
        
        
    }
    private void Start() {
        PI = EM.Player.GetComponent<EntityBuff>();
        PI.OnRefreshBuffs += PI_OnRefreshUI;
    }


    private void PI_OnRefreshUI(Buff[] obj) {
        Debug.Log("BuffCollection Refreshed");
        if(BuffUIs.Count>obj.Length)
            for (int i = obj.Length; i < BuffUIs.Count; i++) {

                BuffUIs[i].Clear();
            }
        if (BuffUIs.Count < obj.Length)
            for (int i = BuffUIs.Count; i < obj.Length; i++) {

                BuffUIs.Add(Instantiate(BuffUIPrefab, transform));
            }

        for (int i = obj.Length - 1; i >= 0; i--) {
            BuffUIs[i].Refresh(obj[i]);
        }
    }
}
