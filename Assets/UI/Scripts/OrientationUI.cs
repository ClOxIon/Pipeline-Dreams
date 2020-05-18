using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using PipelineDreams;
public class OrientationUI : MonoBehaviour
{
    [SerializeField] float widthMultiplier;
    [SerializeField] TaskManager CM;
    [SerializeField] float armLength;
    Quaternion ProjectionMatrix;
    [SerializeField] PipelineDreams.Entity.Entity TargetEntity;
    [SerializeField] PipelineDreams.Entity.Container EM;
    [SerializeField] GameObject RotationMarkerContainer;
    Image[] RotationMarkers;
    float timePassed = 0;
    [SerializeField] float rotationTime;
    LineRenderer drawer;
    int i = 4;
    bool IsRotatingAnimationPlayed = false;
    Quaternion animatedQi;
    Quaternion animatedQf;
    [SerializeField] Transform Arrow;
    [SerializeField] List<Transform> EntitySigns;
    [SerializeField] Sprite EntitySignEnemy;
    [SerializeField] Sprite EntitySignNPC;
    [SerializeField] Sprite EntitySignInteractionTile;
    [SerializeField] Sprite EntitySignNormalTile;
    [SerializeField] Sprite EntitySignObscured;
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

        TargetEntity.GetComponent<PipelineDreams.Entity.Move>().SubscribeOnRotate(OnEntityRotate);
        ProjectionMatrix = Quaternion.Inverse(Quaternion.Euler(-35.26f, -45, 0));
        drawer = GetComponent<LineRenderer>();
        drawer.enabled = false;
        drawer.positionCount = 3;
        drawer.widthMultiplier = widthMultiplier;
        drawer.useWorldSpace = false;
        drawer.SetPosition(0, Vector3.zero);
        TargetEntity.OnInit += (x, y) =>
        {
            Arrow.localRotation = ProjectionMatrix * MirrorX(TargetEntity.IdealRotation);
            drawer.SetPosition(1, Vector3.ProjectOnPlane(ProjectionMatrix * MirrorX(TargetEntity.IdealRotation * Vector3.forward * armLength), Vector3.forward));
            drawer.SetPosition(2, Vector3.ProjectOnPlane(ProjectionMatrix * MirrorX(TargetEntity.IdealRotation * Vector3.up * armLength/2), Vector3.forward));

        };
        CM.OnTaskEnd += ()=>UpdateEntitySigns(0);
    }
    private Vector3 MirrorX(Vector3 x) {
        return new Vector3(-x.x, x.y, x.z);
    }
    private Quaternion MirrorX(Quaternion q) {
        return new Quaternion(q.x, -q.y, -q.z, q.w);
    }

    private IEnumerator OnEntityRotate(Quaternion qi, Quaternion qf)
    {
        var rotationMarker = RotationMarkers.Where((x) => x.gameObject.name.Contains(Directions[Util.LHQToFace(qi)]) && x.gameObject.name.Contains(Directions[Util.LHQToFace(qf)])).FirstOrDefault();
        //Debug.Log($"Orientation change is {rotationMarker.gameObject.name}");
        IsRotatingAnimationPlayed = true;
        CurrentRotationMarker = rotationMarker;
        timePassed = 0;
        animatedQi = qi;
        animatedQf = qf;
        return null;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateEntitySigns(0);
        Arrow.localRotation = ProjectionMatrix * MirrorX(TargetEntity.IdealRotation);
    }
    void UpdateEntitySigns(int rings) {
        for (int f = 0; f < 6; f++)
        {
            EntitySigns[rings].GetChild(f).gameObject.SetActive(TargetEntity.GetComponent<PipelineDreams.Entity.Move>().CanMove(TargetEntity.IdealPosition + Util.FaceToLHVector(f)));
            var entity = EM.FindLineOfSightEntityOnAxis(f, TargetEntity);
            EntitySigns[rings + 1].GetChild(f).gameObject.SetActive(entity != null && entity.Data.Type == PipelineDreams.Entity.EntityType.ENEMY);
            EntitySigns[rings + 2].GetChild(f).gameObject.SetActive(entity != null && entity.Data.Type == PipelineDreams.Entity.EntityType.NPC);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (IsRotatingAnimationPlayed)
        {

            
            timePassed = Mathf.Clamp(timePassed+Time.deltaTime, 0, rotationTime);
            var ratio = timePassed / rotationTime;
            var qt = Quaternion.Slerp(animatedQi, animatedQf, ratio);
            CurrentRotationMarker.color = new Color(0f, 1f, 0f, ratio*(1-ratio)*4*0.5f);
            Arrow.localRotation = ProjectionMatrix * MirrorX(qt);
            drawer.SetPosition(1, Vector3.ProjectOnPlane(ProjectionMatrix * MirrorX(qt * Vector3.forward * armLength), Vector3.forward));

            drawer.SetPosition(2, Vector3.ProjectOnPlane(ProjectionMatrix * MirrorX(qt * Vector3.up * armLength / 2), Vector3.forward));
        }
    }
}
