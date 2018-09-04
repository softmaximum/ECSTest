using Game.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Jobs
{
    public struct PlayerMovementJob : IJobProcessComponentData<Position, PlayerInput>
    {
        public void Execute(ref Position position, ref PlayerInput input)
        {
            position.Value.x += input.Horizontal;
            position.Value.y += input.Vertical;
        }
    }
}