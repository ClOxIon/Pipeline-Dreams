using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class OrientationUI : MonoBehaviour
{
    [SerializeField] float widthMultiplier;

    [SerializeField] float armLength;
    Quaternion ProjectionMatrix;
    [SerializeField] PipelineDreams.Entity.Entity TargetEntity;
    [SerializeField] GameObject RotationMarkerContainer;
    Image[] RotationMarkers;
    float timePassed = 0;
    [SerializeField] float rotationTime;
    LineRenderer drawer;
    int i = 4;
    bool IsRotatingAnimationPlayed = false;
    Quaternion animatedQi;
    Quaternion animatedQf;
    Image CurrentRotationMarker;
    char[] Directions = { 'E', 'W', 'U', 'D', 'N', 'S' };
    private void Awake()
    {
        RotationMarkers = new Image[RotationMarkerContainer.transform.childCount];
        for (int i = 0; i < RotationMarkers.Length; i++)
        {
            RotationMarkers[i] = RotationMarkerContainer.transform.GetChild(i).GetComponent<Image>();
            RotationMarkers[i].color = new Color(1f, 1f, 1f, 0);
        }
        TargetEntity.GetComponent<PipelineDreams.Entity.Move>().SubscribeOnMove(OnEntityMove);

        TargetEntity.GetComponent<PipelineDreams.Entity.Move>().SubscribeOnRotate(OnEntityRotate);
        ProjectionMatrix = Quaternion.Inverse(Quaternion.Euler(-35.26f, -45, 0));
        drawer = GetComponent<LineRenderer>();
        drawer.positionCount = 3;
        drawer.widthMultiplier = widthMultiplier;
        drawer.useWorldSpace = false;
        drawer.SetPosition(0, Vector3.zero);
        TargetEntity.OnInit += (x, y) =>
        {
            drawer.SetPosition(1, Vector3.ProjectOnPlane(ProjectionMatrix * MirrorX(TargetEntity.IdealRotation * Vector3.forward * armLength), Vector3.forward));
            drawer.SetPosition(2, Vector3.ProjectOnPlane(ProjectionMatrix * MirrorX(TargetEntity.IdealRotation * Vector3.up * armLength/2), Vector3.forward));
        };
    }
    private Vector3 MirrorX(Vector3 x) {
        return new Vector3(-x.x, x.y, x.z);
    }

    private IEnumerator OnEntityMove(Vector3Int vi, Vector3Int vf) {
        if (false) yield return null;
    
    }
    private IEnumerator OnEntityRotate(Quaternion qi, Quaternion qf)
    {
        var rotationMarker = RotationMarkers.Where((x) => x.gameObject.name.Contains(Directions[PipelineDreams.Util.LHQToFace(qi)]) && x.gameObject.name.Contains(Directions[PipelineDreams.Util.LHQToFace(qf)])).FirstOrDefault();
        Debug.Log($"Orientation change is {rotationMarker.gameObject.name}");
        IsRotatingAnimationPlayed = true;
        if (CurrentRotationMarker != null) CurrentRotationMarker.color = new Color(0f, 1f, 0f, 0f);
        CurrentRotationMarker = rotationMarker;
        CurrentRotationMarker.color = new Color(0f, 1f, 0f, 0.5f);
        timePassed = 0;
        animatedQi = qi;
        animatedQf = qf;
        if (false) yield return null;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRotatingAnimationPlayed)
        {
            
            timePassed = Mathf.Clamp(timePassed+Time.deltaTime, 0, rotationTime);
            var qt = Quaternion.Slerp(animatedQi, animatedQf, timePassed / rotationTime);
            drawer.SetPosition(1, Vector3.ProjectOnPlane(ProjectionMatrix * MirrorX(qt * Vector3.forward * armLength), Vector3.forward));

            drawer.SetPosition(2, Vector3.ProjectOnPlane(ProjectionMatrix * MirrorX(qt * Vector3.up * armLength / 2), Vector3.forward));
        }
    }
}
