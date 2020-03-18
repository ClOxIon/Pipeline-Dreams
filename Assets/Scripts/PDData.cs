using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PipelineDreams {
    /// <summary>
    /// Base class for readonly data, saved as ScriptableObjects.
    /// </summary>
    [System.Serializable]
    public class PDData {
        [SerializeField] private string name;
        [SerializeField] private string nameInGame;
        [TextArea(5, 10)]
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] List<Parameter> parameters;

        public string Name { get => name; }
        public string NameInGame { get => nameInGame; set => nameInGame = value; }
        public string Description { get => description; set => description = value; }
        public Sprite Icon { get => icon; set => icon = value; }

        public bool HasParameter(string Name) {
            foreach (var x in parameters)
                if (x.Name == Name)
                    return true;
            return false;
        }
        public float FindParameterFloat(string Name) {
            foreach (var x in parameters)
                if (x.Name == Name)
                    return float.Parse(x.Value);
            Debug.LogWarning("No Float Parameter Found: " + Name +" At "+ GetType());
            return 0;
        }
        public int FindParameterInt(string Name) {
            foreach (var x in parameters)
                if (x.Name == Name)
                    return int.Parse(x.Value);
            Debug.LogWarning("No Integer Parameter Found: " + Name + " At " + GetType());
            return 0;
        }
        public string FindParameterString(string Name) {
            foreach (var x in parameters)
                if (x.Name == Name)
                    return x.Value;
            Debug.LogWarning("No String Parameter Found: " + Name + " At " + GetType());
            return null;
        }
        [System.Serializable]
        protected struct Parameter
        {
            public string Name;
            public string Value;
        }
    }
    public interface IPDDataSet {
        List<PDData> DataSet { get; }
    }
}