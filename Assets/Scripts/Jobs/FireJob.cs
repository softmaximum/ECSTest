using Game.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Jobs
{
    public struct FireJob : IJobProcessComponentData<Player, Position, Rotation>
    {
        public bool Fire;

        private static void MakeFire(float3 position, float3 direction)
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();
            var fireEntityArchetype = entityManager.CreateArchetype
            (
                typeof(Movement),
                typeof(Position)
            );

            var entity = entityManager.CreateEntity(fireEntityArchetype);
            entityManager.SetComponentData(entity, new Position{ Value = position});
            entityManager.SetComponentData(entity, new Movement(direction, 1.0f));
        }

        public void Execute(ref Player player, ref Position position, ref Rotation rotation)
        {
            if (Fire)
            {
                MakeFire(position.Value, math.forward(rotation.Value));
            }
        }
    }
}