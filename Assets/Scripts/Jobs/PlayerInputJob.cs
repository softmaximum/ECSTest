using Game.Components;
using Unity.Entities;
using Unity.Mathematics;

namespace Jobs
{
    public struct PlayerInputJob : IJobProcessComponentData<PlayerInput>
    {
        public bool Left;
        public bool Right;
        public bool Up;
        public bool Down;
        public bool UpLeft;
        public bool UpRight;
        public bool DownLeft;
        public bool DownRight;
        public float2 MousePosition;
        public bool Fire;
        public bool RightMouseButton;

        public void Execute(ref PlayerInput input)
        {
            input.Horizontal = Left || UpLeft || DownLeft ? -1f : Right || UpRight || DownRight ? 1f : 0f;
            input.Vertical = Down || DownLeft || DownRight ? -1f : Up || UpRight || UpLeft ? 1f : 0f;
            input.MousePosition = MousePosition;
            input.Fire = Fire ? 1 : 0;
            input.RightMouseButton = RightMouseButton ? 1 : 0;
        }
    }
}