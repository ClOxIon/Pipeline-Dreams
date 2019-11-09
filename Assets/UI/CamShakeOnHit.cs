using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShakeOnHit : MonoBehaviour
{
    private void Awake() {
        var EM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EntityManager>();
        EM.OnPlayerInit+=()=>EM.Player.GetComponent<EntityHealth>().OnDamagedAmount += CamShakeOnHit_OnDamaged;
    }

    private void CamShakeOnHit_OnDamaged(int obj, int max,  Entity e) {
        var r = (float)obj / max;
        EZCameraShake.CameraShaker.Instance.ShakeOnce(r*10, 1f, 0.2f, 2*r);
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
