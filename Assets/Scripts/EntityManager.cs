using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public event Action OnPlayerInit;
    public Entity Player { get; private set; }
    public event Action<Entity> OnNewEntitySpawn;
    public event Action<Entity> OnEntityDeath;
    [SerializeField] EntityDataset EDataContainer;
    [SerializeField] public BuffDataset BDataContainer;
    [SerializeField]List<Entity> EntitiesInScene = new List<Entity>();
    // Start is called before the first frame update
    MapManager mManager;
    float spawntimer;
    void Start()
    {
       
       
    }
    private void Awake() {
        mManager = GetComponent<MapManager>();
        GetComponent<ClockManager>().OnClockModified += (f)=> { while (f - spawntimer >= 100) { SpawnRandomEnemy(); spawntimer += 100; } };
        GetComponent<MapManager>().OnMapCreateComplete += ()=> { InitPlayer(); InitEntities(); };
        GetComponent<MapManager>().OnMapLoadComplete += () => { LoadPlayer(); LoadEntities(); };
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        EntitiesInScene.Add(Player);
    }

    private void LoadPlayer() {
       
    }
    void InitEntities() {


    }
    void LoadEntities() {


    }

    EntityData GetEntityDataFromName(string name) {
        return EDataContainer.Dataset.Find((x)=> { return x.Name == name; });

    }
    void InitPlayer() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>().Initialize(GetComponent<MapManager>().GetPlayerSpawnPoint(), Util.FaceToLHQ(UnityEngine.Random.Range(0, 5)),GetEntityDataFromName("Player"));
        OnPlayerInit?.Invoke();
    }
    void SpawnRandomEnemy() {
        SpawnEnemy(EDataContainer.Dataset[UnityEngine.Random.Range(0, EDataContainer.Dataset.Count - 1)].Name, mManager.GetRandomAccessiblePoint(), Util.TurnDown);
    }
    void SpawnEnemy(string name, Vector3Int Position, Quaternion Rotation) {
       
        try {

            
            if (FindEntityInPosition(Position) != null) return;
            var dt = GetEntityDataFromName(name);
            var mob = Instantiate(dt.Prefab);
            mob.Initialize(Position, Rotation,dt);
            EntitiesInScene.Add(mob);
            OnNewEntitySpawn?.Invoke(mob);
            var d = mob.GetComponent<EntityDeath>();
            if (d != null) d.OnEntityDeath += (e) => { EntitiesInScene.Remove(e); OnEntityDeath?.Invoke(e); };
        }
        catch(ArgumentOutOfRangeException e) {

        }
    }
    public Entity FindEntityInPosition(Vector3Int v) {

        foreach (var e in EntitiesInScene)
            if (e.IdealPosition == v) return e;
        return null;
    }
    public Entity FindEntityInPosition(int x, int y, int z) {
        return FindEntityInPosition(new Vector3Int(x, y, z));
    }
    public Entity FindEntityInRelativePosition(Vector3Int v, Entity origin) {

        foreach (var e in EntitiesInScene)
            if (e.IdealPosition == v+origin.IdealPosition) return e;
        return null;
    }
    public Entity FindEntityInRelativePosition(int x, int y, int z, Entity origin) {

        return FindEntityInRelativePosition(new Vector3Int(x, y, z), origin);
    }
    public Entity FindEntityInLine(int f, Entity origin, int Sightscale = SceneObjectManager.sightscale) {
        Entity e;
        for (int i = 1; i <= Sightscale; i++) {
            if (mManager.GetTileRelative(Vector3Int.zero +Util.FaceToLHVector(f) * (i - 1), f, origin) == Tile.wall)
                return null;
            e = FindEntityInRelativePosition(Util.FaceToLHVector(f) * i, origin);
            if (e != null)
                return e;
        }
        return null;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
