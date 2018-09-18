using Game.Components;
using Jobs;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;

namespace Game.Systems
{
    [UpdateInGroup(typeof(UpdateGroups.ExecuteUpdateGroup))]
    public class CollisionSystem : JobComponentSystem
    {
        private struct CollisionGroup
        {
            [ReadOnly]
            public SharedComponentDataArray<Collision> Collisions;
            public ComponentDataArray<Position> Positions;
            public EntityArray Entity;
            public readonly int Length;
        }

        [Inject] private CollisionGroup _group;
        [Inject] private CollisionSystemBarrier _barrier;
        
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new CollisionJob
            {
                Entity = _group.Entity,
                Collisions = _group.Collisions,
                Positions = _group.Positions,
                CommandBuffer = _barrier.CreateCommandBuffer(),
            };
            return job.Schedule(inputDeps);
        }
    }
}