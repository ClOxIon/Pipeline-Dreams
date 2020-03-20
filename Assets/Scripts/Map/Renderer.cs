using UnityEngine;

namespace PipelineDreams.Map
{
    public abstract class Renderer : ScriptableObject
    {
        [SerializeField] protected Entity.Entity Station;

        [SerializeField] protected Entity.Entity PipePath;

        [SerializeField] protected Entity.Entity PipeWall;

        [SerializeField] protected Entity.Entity RoomWall;

        [SerializeField] protected Entity.Entity RoomEntrance;

        [SerializeField] protected TaskManager TM;

        [SerializeField] protected Entity.Container enDataContainer;
        public abstract void RenderMap(MapFeatData data);
        public void Initialize(TaskManager tm) => TM = tm;

    }
}