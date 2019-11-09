using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System;

public class GameManager : MonoBehaviour
{
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
    public event Action OnGameSave;
    public event Action OnNewGameStart;
    public event Action OnGameLoad;
    public void StartNewGame() {
        var work = SceneManager.LoadSceneAsync("Prime Scene");
        work.allowSceneActivation = true;
        work.completed += (x)=> { OnNewGameStart?.Invoke();
            var mManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MapManager>();
            mManager.CreateNewMap();

        };
        
       
    }

    public void LoadGame() {
        SceneManager.LoadScene("Prime Scene with Single View");
        try {
            OnGameLoad?.Invoke();
        }
        catch (Exception e) {
            Debug.LogError("Load Fail!");
        }
    }
    void InitDirectory() {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var path = Path.Combine(appDataPath, @"Pipeline Dreams\");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        AppdataPath = path;

    }
    // Start is called before the first frame update
    void Awake()
    {
        InitDirectory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsSaveFileExist() {

            return File.Exists(Path.Combine(AppdataPath, "Map.json"));
    }
    void SaveGameStatus() {

        OnGameSave?.Invoke();
        var obj = GameObject.FindGameObjectWithTag("SceneManager");
        if (obj == null) return;//The game is not currently running, so nothing to save.
        var mapdata = obj.GetComponent<MapManager>().SerializeMapData();
        using (StreamWriter SW = new StreamWriter(Path.Combine(AppdataPath, "Map.json"))) {
            SW.Write(mapdata);

        }

    }
    private void OnApplicationQuit() {
        SaveGameStatus();
    }
}
