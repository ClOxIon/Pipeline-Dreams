using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace PipelineDreams {

    [CreateAssetMenu(fileName = "Executer", menuName = "ScriptableObjects/Manager/Executer")]
    public class Executer : ScriptableObject
    {
        [SerializeField] EntityDataContainer EDC;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ChangeEntityParam(string name, string param, string value) {
            var list = EDC.FindEntities((x) => x.Data.Name == name);
            if (list.Count != 0)
                list[0].Parameters[param] = float.Parse(value);
        }
    }
}
