using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightFlashScript : MonoBehaviour
{
    [SerializeField] float time;
    float since = 0;
    List<GameObject> lights;
    // Start is called before the first frame update
    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("Light").ToList().FindAll((x) => x.transform.parent == transform);
    }

    // Update is called once per frame
    void Update()
    {
        since += Time.deltaTime;
        if (since > time) {
            since -= time;
            foreach (var x in lights)
                x.SetActive(Random.value > 0.5);
        }
    }
}
