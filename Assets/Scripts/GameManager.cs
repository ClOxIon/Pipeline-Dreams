using System;
using System.IO;
using UnityEngine;

namespace PipelineDreams {
    public class GameManager : MonoBehaviour {
        #region singleton
        private static GameManager _instance;
        public static GameManager Instance {
            get {
                if (!_instance) {
                    _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                    if (!_instance) {
                        GameObject container = new GameObject {
                            name = "GameManager"
                        };
                        container.tag = "GameManager";
                        _instance = container.AddComponent(typeof(GameManager)) as GameManager;
                        DontDestroyOnLoad(container);
                    }
                }

                return _instance;
            }
        }


        #endregion


        public string AppdataPath { get; private set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        void InitDirectory() {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(appDataPath, @"Pipeline Dreams\");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            AppdataPath = path;

        }
        // Start is called before the first frame update
        void Awake() {
            InitDirectory();
        }

    }
}