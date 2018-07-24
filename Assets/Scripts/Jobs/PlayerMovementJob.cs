using Game.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Jobs
{
    public struct PlayerMovementJob : IJobProcessComponentData<Position, Rotation, PlayerInput>
    {
        public void Execute(ref Position position, ref Rotation rotation, ref PlayerInput input)
        {
            position.Value.x += input.Horizontal;
            position.Value.y += input.Vertical;

            rotation.Value.value.x = input.MousePosition.x;
            rotation.Value.value.y = input.MousePosition.y;
        }
    }
}