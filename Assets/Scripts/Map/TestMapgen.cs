using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Map
{
    public class TestMapgen : MonoBehaviour
    {
        MapFeatData mapFeatCode;
        [SerializeField] Generator mapGenerator;
        // Start is called before the first frame update
        void Start() {
            mapFeatCode = mapGenerator.GenerateMap(0, 1);
            Debug.Log("MapFeatCode Generated: "+mapFeatCode);
            Debug.Log("Paths Generated: " + mapFeatCode.Paths.Count);
            foreach (var x in mapFeatCode.Paths)
                Debug.Log("Length: " + x.Cells.Count);
        }

        // Update is called once per frame
        void Update() {

        }
        
    }
}