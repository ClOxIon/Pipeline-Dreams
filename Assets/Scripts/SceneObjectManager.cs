using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneObjectManager : MonoBehaviour
{
    public const float worldscale = 10;
    public const int sightscale = 12;
    public const int despawnscale = 9;
    Entity Player;
    bool[,,] GridVisibility;
    List<SceneObject> SceneObjects;
    MapManager mManager;
    PlayerController cMovement;
    EntityManager EM;
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("I have awoken");
        GridVisibility = new bool[2 * sightscale + 1, 2 * sightscale + 1, 2 * sightscale + 1];
        if (GameObject.FindGameObjectsWithTag("Player").Length != 0) Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        else Debug.Log("Player object not found");
        mManager = GetComponent<MapManager>();
        cMovement = GetComponent<PlayerController>();
        EM = GetComponent<EntityManager>();
        if (EM != null) EM.OnPlayerInit += CalculateVisibility;
        else Debug.Log("Event manager not found");
        if (cMovement != null) cMovement.OnPlayerTranslation += (v) => { CalculateVisibility(); };
        else Debug.Log("Player controller not found");
        if (mManager != null)
        {
            mManager.OnMapCreateComplete += InitializeWorldObjects;
            mManager.OnMapLoadComplete += LoadWorldObjects;
        }
        else Debug.Log("Map manager not found");
    }

    void SetGridVisibilityRelative(Vector3Int v, bool b)
    {

        v += Vector3Int.one * sightscale;
        GridVisibility[v.x, v.y, v.z] = b;
    }
    public bool GetGridVisibilityRelative(Vector3Int v)
    {
        v += Vector3Int.one * sightscale;
        return GridVisibility[v.x, v.y, v.z];
    }
    /// <summary>
    /// Mark each point of the grid Visible/Invisible. (Does not render invisible passages)
    /// </summary>
    void CalculateVisibility()
    {
        foreach (var o in SceneObjects)
        {
            var v = o.Position - Player.IdealPosition;
            o.gameObject.SetActive(true);
            //o.gameObject.SetActive(v.x * v.y == 0 && v.z * v.y == 0 && v.x * v.z == 0);


        }

        /*
        GridVisibility.Initialize();
        GridVisibility[sightscale, sightscale, sightscale] = true;
        for (int f = 0; f < 6; f++) {
            var direction = Util.FaceToLHVector(f);
            for (int d = 0; d < sightscale; d++) {
                if (mManager.GetTileRelative(direction * d, 0,Player) == Tile.wall) break;
                SetGridVisibilityRelative(direction * (d + 1), true);
                for (int fp = 0; fp < 6; fp++) {
                    if (fp >> 1 == f >> 1) continue;
                    var directionp = Util.FaceToLHVector(fp);
                    for (int dp = 0; dp < sightscale; dp++) {
                        if (mManager.GetTileRelative(direction * (d + 1) + directionp * dp, 0,Player) == Tile.wall) break;
                        SetGridVisibilityRelative(direction * (d + 1) + directionp * (dp + 1), true);
                    }
                }
            }
        }
        */
    }

    /// <summary>
    /// Translation is unnecessary when we move the cameraRoot.
    /// However, Instantiation is still crucial.
    /// To instantiate, read object data from MapManager. 
    /// </summary>
    /// <param name="deltaOrigin"></param>
    void TranslateAndInstantiateWorldObjects(Vector3Int deltaOrigin)
    {

        for (int i = SceneObjects.Count - 1; i >= 0; i--)
        {
            /*
            if (Squaremetric(SceneObjects[i].Position, mManager.CurrentPosition) > despawnscale) {
                   
                Destroy(SceneObjects[i].gameObject);
                SceneObjects.RemoveAt(i);
            }
            */
        }


    }
    int Squaremetric(Vector3Int a, Vector3Int b)
    {
        return Mathf.Max(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y), Mathf.Abs(a.z - b.z));
    }
    public void InitializeWorldObjects()
    {
        Debug.Log("Initializing world objects");
        SceneObjects = new List<SceneObject>();
        for (int i = 0; i < mManager.GetMapScale(0); i++)
            for (int j = 0; j < mManager.GetMapScale(1); j++)
                for (int k = 0; k < mManager.GetMapScale(2); k++)
                {

                    for (int f = 0; f < 6; f++)
                    {
                        if (mManager.GetTile(i, j, k, f) == Tile.nothing)
                            continue;
                        var obj = mManager.Dataset.Dataset.Find((x) => x.Type == mManager.GetTile(i, j, k, f));
                        if (obj.Prefab != null)
                        {
                            var o = Instantiate(obj.Prefab, new Vector3(i, j, k) * worldscale, Util.FaceToLHQ(f));
                            o.Position = new Vector3Int(i, j, k);
                            SceneObjects.Add(o);
                        }
                    }

                }
    }
    void LoadWorldObjects()
    {

    }
    bool IsOutofRange(bool[,,] m, int i, int j, int k)
    {
        try
        {
            var b = m[i, j, k];
        }
        catch (IndexOutOfRangeException e)
        {

            return true;
        }

        return false;

    }

    void Start()
    {


    }
    void RecalculateGridVisibility()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
