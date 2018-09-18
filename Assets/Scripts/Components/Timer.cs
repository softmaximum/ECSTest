using Unity.Entities;

namespace Game.Components
{
    public struct Timer : IComponentData
    {
        public float Interval;
        public float Time;
        public int RepeatCount;
    }
}