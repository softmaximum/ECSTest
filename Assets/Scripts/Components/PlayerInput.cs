using Unity.Entities;
using Unity.Mathematics;

namespace Game.Components
{
    public struct PlayerInput : IComponentData
    {
        public float Horizontal;
        public float Vertical;
        public float2 MousePosition;
        public int Fire;
        public int RightMouseButton;
    }

}