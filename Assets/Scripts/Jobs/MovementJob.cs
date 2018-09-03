using Game.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Jobs
{
    public struct MovementJob : IJobProcessComponentData<Position, Movement>
    {
        public float DeltaTime;
        public void Execute(ref Position position, ref Movement movement)
        {
            position.Value += movement.direction * movement.speed * DeltaTime;
        }
    }
}