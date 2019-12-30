using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [System.Serializable]
    public class Data {
        public string Name;
        public string NameInGame;
        [TextArea(5, 10)]
        public string Description;
        public Sprite Icon;
        [SerializeField] List<Parameter> Parameters;
        public bool HasParameter(string Name) {
            foreach (var x in Parameters)
                if (x.Name == Name)
                    return true;
            return false;
        }
        public float FindParameterFloat(string Name) {
            foreach (var x in Parameters)
                if (x.Name == Name)
                    return float.Parse(x.Value);
            Debug.LogWarning("No Float Parameter Found: " + GetType());
            return 0;
        }
        public int FindParameterInt(string Name) {
            foreach (var x in Parameters)
                if (x.Name == Name)
                    return int.Parse(x.Value);
            Debug.LogWarning("No Integer Parameter Found: " + GetType());
            return 0;
        }
        public string FindParameterString(string Name) {
            foreach (var x in Parameters)
                if (x.Name == Name)
                    return x.Value;
            Debug.LogWarning("No String Parameter Found: " + GetType());
            return null;
        }
    }
}