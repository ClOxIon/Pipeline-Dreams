using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PipelineDreams.Map;
using PipelineDreams.Entity;
public class Minimap2DPlayerUI : MonoBehaviour
{
    [SerializeField] Entity Player;
    
    [SerializeField] Image PlayerPrefab;
    [SerializeField] Color color;
    Quaternion ProjectionMatrix;
    [SerializeField] float scale;
    [SerializeField] float zscale;
    [SerializeField] int targetZ = 0;
    
    private void Awake()
    {
        FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
        ProjectionMatrix = Quaternion.Inverse(Quaternion.Euler(-35.26f, -45, 0));
    }
    public void Init() {
       
        Redraw();
    }
    public void Redraw() {
        for (int i = 0; i < transform.childCount; i++)//Clear all signs.
            Destroy(transform.GetChild(i).gameObject);
        DrawPlayer(Player.IdealPosition);
    }
    Vector3 Projection(Vector3 vec) { var vec2 = new Vector3(-vec.x, vec.y * zscale, vec.z);  var vector = ProjectionMatrix * vec2 * scale; return new Vector3(vector.x, vector.y , 0);}
    void DrawPlayer(Vector3 vec)
    {
        if (Vector3Int.RoundToInt(vec).y != targetZ) return;
        var inst = Instantiate(PlayerPrefab, transform);
        inst.GetComponent<RectTransform>().anchoredPosition = Projection(vec);
        inst.color = color;
        float angle = 0;
        switch (PipelineDreams.Util.LHQToFace(Player.IdealRotation))
        {
            case 0:
                angle = 120;
                break;
            case 1:
                angle = -60;
                break;
            case 2:
                angle = 0;
                break;
            case 3:
                angle = 180;
                break;
            case 4:
                angle = -120;
                break;
            case 5:
                angle = 60;
                break;
        }
        inst.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, angle);
    }
    
    void Start()
    {
        Redraw();   
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnZIncrease() {
        targetZ++;
        Redraw();
    }
    void OnZDecrease() {
        targetZ--;
        Redraw();
    }
}
