using UnityEngine;

namespace PipelineDreams
{
    public abstract class MapRenderer : ScriptableObject
    {
        [SerializeField] protected Entity Station;

        [SerializeField] protected Entity PipePath;

        [SerializeField] protected Entity PipeWall;

        [SerializeField] protected Entity RoomWall;

        [SerializeField] protected Entity RoomEntrance;

        [SerializeField] protected TaskManager TM;

        [SerializeField] protected EntityDataContainer enDataContainer;
        public abstract void RenderMap(MapFeatData data);
        public void Initialize(TaskManager tm) => TM = tm;

    }
}