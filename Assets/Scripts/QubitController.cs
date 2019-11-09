using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QubitController : MonoBehaviour
{
    [SerializeField] GameObject Pointer;
    [SerializeField] GameObject ZProjectionQuad;
    [SerializeField] GameObject LatitudeIndicator;
    [SerializeField] GameObject LongitudeIndicator;
    // Start is called before the first frame update
    Quaternion IdealQ=Quaternion.identity;
    const float physicalRadius = 100;
    private void Awake() {
        //IdealQ = AlgebraUtil.LHQToRHQ(Pointer.transform.localRotation);
        //GameObject.FindGameObjectWithTag("SceneManager").GetComponent<PlayerController>().OnPlayerDeltaRotation += QubitController_OnPlayerRotation;

    }
    void Start()
    {
        //offset
        QubitController_OnPlayerRotation(Quaternion.Euler(0, 0, 90));
    }

    private void QubitController_OnPlayerRotation(Quaternion obj) {

        IdealQ = obj* IdealQ;
        //Debug.Log("QubitController_OnPlayerRotation : " + AlgebraUtil.LHQToRHQ(obj));
        float x = IdealQ.x;
        float y = IdealQ.y;
        float z = IdealQ.z;
        float w = IdealQ.w;
        var p = Mathf.Sqrt(1 - Mathf.Pow(1 - 2 * x * x - 2 * y * y, 2));
        float angle;
        
        if (p < 0.01)
            angle = 0;
        else {
            var X = (2 * x * z + 2 * y * w) / p;//=cos t
            var Y = (2 * y * z - 2 * x * w) / p;//=sin t
            
            angle = Mathf.Asin(Y);
            if (X < 0)
                angle = Mathf.PI - angle;
            }
        ZProjectionQuad.transform.localScale = new Vector3(p, 1, 1);
        ZProjectionQuad.transform.localRotation = Quaternion.Euler(0, 0, angle*180/Mathf.PI);

        /*
         * if (p>0.01)ZProjectionQuad.transform.rotation
        */
    }
    private void MeasureZ() {
        var state = IdealQ*new Vector3(0, 0, 1);
        var zero = Quaternion.Euler(0, 0, 90);
        var one = Quaternion.Euler(0, 0, -90);
        IdealQ = zero;


    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) 
            { IdealQ = Quaternion.Euler(-45, 0, 0) * IdealQ; QubitController_OnPlayerRotation(Quaternion.identity); } 
        else if (Input.GetKeyDown(KeyCode.W)) {
            IdealQ = Quaternion.Euler(180, 0, 0) * IdealQ; QubitController_OnPlayerRotation(Quaternion.identity);
        } else if (Input.GetKeyDown(KeyCode.E)) {
            IdealQ = Quaternion.Euler(45, 0, 0) * IdealQ; QubitController_OnPlayerRotation(Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.A)) { IdealQ = Quaternion.Euler(0, -45, 0) * IdealQ; QubitController_OnPlayerRotation(Quaternion.identity); } else if (Input.GetKeyDown(KeyCode.S)) {
            IdealQ = Quaternion.Euler(0, 180, 0) * IdealQ; QubitController_OnPlayerRotation(Quaternion.identity);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            IdealQ = Quaternion.Euler(0, 45, 0) * IdealQ; QubitController_OnPlayerRotation(Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Z)) { IdealQ = Quaternion.Euler(0, 0, -45) * IdealQ; QubitController_OnPlayerRotation(Quaternion.identity); } else if (Input.GetKeyDown(KeyCode.X)) {
            IdealQ = Quaternion.Euler(0, 0, 180) * IdealQ; QubitController_OnPlayerRotation(Quaternion.identity);
        } else if (Input.GetKeyDown(KeyCode.C)) {
            IdealQ = Quaternion.Euler(0, 0, 45) * IdealQ; QubitController_OnPlayerRotation(Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            MeasureZ();
        }

        Pointer.transform.localRotation = Quaternion.Slerp(Pointer.transform.localRotation, IdealQ, 0.2f);
        
        var vp = Pointer.transform.localRotation * new Vector3(0, 0, 1);//RH vector

        LatitudeIndicator.transform.localPosition = new Vector3(0, vp.z, 0);
        var r = Mathf.Sqrt(Mathf.Clamp(1 - vp.z * vp.z,0,1));
        if (r < 0.01) {
            LongitudeIndicator.GetComponent<Renderer>().enabled = false;
            LatitudeIndicator.GetComponent<Renderer>().enabled = false;
        } else {
            LongitudeIndicator.GetComponent<Renderer>().enabled = true;
            LatitudeIndicator.GetComponent<Renderer>().enabled = true;

        }
        LatitudeIndicator.transform.localScale = new Vector3(physicalRadius*r, physicalRadius * r, physicalRadius/2);//divided by 2 for beauty.
        var q = Pointer.transform.localRotation;
        
        LongitudeIndicator.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(q.y * q.z - q.x * q.w, q.x * q.z + q.y * q.w) * 180 / Mathf.PI);

    }
}
