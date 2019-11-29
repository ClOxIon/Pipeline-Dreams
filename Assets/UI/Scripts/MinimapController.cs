using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    const float scale = 0.2f;
    const int renderDistance = 5;
    [SerializeField] GameObject blockPrefab;
    [SerializeField] GameObject pipePrefab;
    [SerializeField] GameObject playerPrefab;
    GameObject minimapPlayerMarker;
    MapManager mManager;
    EntityManager EM;
    ClockManager CM;
    Entity player;
    Quaternion offset = Quaternion.Euler(0,0,0);
    List<GameObject> RenderedObjects = new List<GameObject>();
    private void Awake() {
        mManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MapManager>();
        EM = mManager.GetComponent<EntityManager>();
        EM.OnPlayerInit += Render;
        CM = mManager.GetComponent<ClockManager>();
        CM.OnTaskEnd += Render;
        minimapPlayerMarker = Instantiate(playerPrefab, gameObject.transform,false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void RotatePlayer(Quaternion q) {
        minimapPlayerMarker.transform.localRotation = offset*q;
    }
    private void Render() {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        foreach (var g in RenderedObjects)
            Destroy(g);
        RenderedObjects.Clear();
        for (int i = -renderDistance; i <= renderDistance; i++)
            for (int j = -renderDistance; j <= renderDistance; j++)
                if (mManager.GetBlockRelative(new Vector3Int(i, 0, j),player) == Block.pipe) {
                    var obj = Instantiate(blockPrefab, transform, false);
                    RenderedObjects.Add(obj);
                    obj.transform.localPosition = new Vector3(i * scale, 0, j*scale);
                    
                }
        RotatePlayer(player.IdealRotation);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
