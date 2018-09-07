using Unity.Entities;

namespace Game.Components
{
    public struct Timer : IComponentData
    {
        public float Interval;
        public float Time;
        public bool Repeat;
    }
}