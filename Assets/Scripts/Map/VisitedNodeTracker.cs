using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PipelineDreams.Map{
public class VisitedNodeTracker : MonoBehaviour
{
    public List<Vector3Int> VisitedNodes = new List<Vector3Int>();
    [SerializeField] Entity.Entity TargetEntity;
        private void Awake()
        {
            TargetEntity.GetComponent<Entity.Move>().SubscribeOnMove(OnMove);
        }
        private void Start()
        {
            var v = TargetEntity.IdealPosition;
            OnMove(v, v);
        }
        IEnumerator OnMove(Vector3Int i, Vector3Int f) {
            if(!VisitedNodes.Contains(f))
            VisitedNodes.Add(f);
            return null;
        }
        // Update is called once per frame
        void Update()
    {
        
    }
}
}