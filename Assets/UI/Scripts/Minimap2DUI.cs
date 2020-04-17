using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PipelineDreams.Map;
using PipelineDreams.Entity;
public class Minimap2DUI : MonoBehaviour
{
    [SerializeField] Generator generator;
    MapFeatData mapCode;
    [SerializeField] Container enData;
    [SerializeField] Image RoomPrefab;
    [SerializeField] Image PathPrefab;
    [SerializeField] Image NodePrefab;
    [SerializeField] Image ArrowPrefab;
    [SerializeField] Color color;
    Quaternion ProjectionMatrix;
    [SerializeField] float scale;
    [SerializeField] float zscale;
    [SerializeField] int targetZ = 0;
    [SerializeField] bool DrawUpDownArrows;
    [SerializeField] float UpDownArrowPosition;
    [SerializeField] float UpDownArrowAlpha;
    [SerializeField] VisitedNodeTracker visitedNodes;
    private void Awake()
    {
        FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
        ProjectionMatrix = Quaternion.Inverse(Quaternion.Euler(-35.26f, -45, 0));
    }
    public void Init(Generator gen, Container data) {
        generator = gen;
        enData = data;
        Redraw();
    }
    public void Redraw() {
        for (int i = 0; i < transform.childCount; i++)//Clear all signs.
            Destroy(transform.GetChild(i).gameObject);
        mapCode = generator.LastGenData;
        foreach (var room in mapCode.Features) {
            foreach (var y in room.OccupiedCells)
            {
                DrawRoom(room.Rotation * y + room.Position);
            }
            foreach (var cell in room.UsedEntrances)
            {
                DrawPath(room.Rotation * cell.Position + room.Position, room.Rotation * (cell.Position + PipelineDreams.Util.LHQToLHUnitVector(cell.Rotation)) + room.Position);
                DrawNode(room.Rotation * cell.Position + room.Position);
            }
        }
        foreach (var path in mapCode.Paths)
        {

            for (int i = 1; i < path.Cells.Count - 1; i++)
                DrawNode(path.Cells[i]);

            for (int i = 0; i < path.Cells.Count - 1; i++)

                DrawPath(path.Cells[i], path.Cells[i + 1]);
        }
    }
    Vector3 Projection(Vector3 vec) { var vec2 = new Vector3(-vec.x, vec.y * zscale, vec.z);  var vector = ProjectionMatrix * vec2 * scale; return new Vector3(vector.x, vector.y , 0);}
    void DrawRoom(Vector3 vec)
    {
        if (!visitedNodes.VisitedNodes.Contains(Vector3Int.RoundToInt(vec))) return;
        if (Vector3Int.RoundToInt(vec).y != targetZ) return;
        var inst = Instantiate(RoomPrefab, transform);
        inst.GetComponent<RectTransform>().anchoredPosition = Projection(vec);
        inst.color = color;
    }
    // Start is called before the first frame update
    void DrawPath(Vector3 i, Vector3 f) {
        if (!visitedNodes.VisitedNodes.Contains(Vector3Int.RoundToInt(i)) && !visitedNodes.VisitedNodes.Contains(Vector3Int.RoundToInt(f))) return;
        if (Vector3Int.RoundToInt(i).y != targetZ && Vector3Int.RoundToInt(f).y != targetZ) return;
        var fif = PipelineDreams.Util.LHUnitVectorToFace(Vector3Int.RoundToInt(i - f));
        if (!DrawUpDownArrows && (fif == 2 || fif == 3)) return;
        var vec = 0.5f * i + 0.5f * f;
        var inst = Instantiate(PathPrefab, transform);
        inst.color = color;
        RectTransform rec = inst.GetComponent<RectTransform>();
        rec.anchoredPosition = Projection(vec);
        var face = PipelineDreams.Util.LHUnitVectorToFace(Vector3Int.RoundToInt(f - i));
        float angle = 0;
        switch (face / 2)
        {
            case 0:
                angle = 120f;
                break;
            case 1:
                angle = 0f;
                var inst2 = Instantiate(ArrowPrefab, transform);
                var rec2 = inst2.GetComponent<RectTransform>();
                rec.localScale = new Vector3(1,zscale,1);
                rec2.localScale = new Vector3(1, zscale, 1);
                if (Vector3Int.RoundToInt(i).y != targetZ)
                {
                    var vec2 = (0.5f-UpDownArrowPosition) * i + (0.5f + UpDownArrowPosition) * f;
                    rec.anchoredPosition = Projection(vec2);

                    rec2.anchoredPosition = Projection(vec2);
                }
                else
                {

                    var vec2 = (0.5f + UpDownArrowPosition) * i + (0.5f - UpDownArrowPosition) * f;
                    rec.anchoredPosition = Projection(vec2);
                    rec2.anchoredPosition = Projection(vec2);
                }
                if (Vector3Int.RoundToInt(i).y > targetZ || Vector3Int.RoundToInt(f).y > targetZ ) {
                    inst.color = new Color(1, 1, 0.5f, UpDownArrowAlpha);
                    inst2.color = new Color(1, 1, 0.5f, UpDownArrowAlpha);
                    rec2.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else { inst.color = new Color(0.5f, 1, 1, UpDownArrowAlpha); inst2.color = new Color(0.5f, 1, 1, UpDownArrowAlpha); rec2.localRotation = Quaternion.Euler(0, 0, 180); }
                break;
            case 2:
                angle = 60f;
                break;
        }
        rec.localRotation = Quaternion.Euler(0, 0,angle);
        
    }
    void DrawNode(Vector3 vec) {
        if (!visitedNodes.VisitedNodes.Contains(Vector3Int.RoundToInt(vec))) return;
        if (Vector3Int.RoundToInt(vec).y != targetZ) return;
        var inst = Instantiate(NodePrefab, transform);
        inst.GetComponent<RectTransform>().anchoredPosition = Projection(vec);
        inst.color = color;
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
