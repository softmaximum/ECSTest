using Game.Components;
using Jobs;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

namespace Game.Systems
{
    [UpdateBefore(typeof(UnityEngine.Experimental.PlayerLoop.FixedUpdate))]
    public class CharacterMovementSystem : JobComponentSystem
    {
        private struct MovementGroup
        {
            public ComponentDataArray<Position> Positons;
            public ComponentDataArray<CharacterMovement> Movements;
            public EntityArray Entities;
            public readonly int Length;
        }

        [Inject] private MovementGroup _group;
        [Inject] private CharacterMovementBarrier _barrier;

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new CharacterMovementJob
            {
                DeltaTime = Time.deltaTime,
                Buffer = _barrier.CreateCommandBuffer(),
                Positons = _group.Positons,
                Movements = _group.Movements,
                Entities = _group.Entities,
            };
            return job.Schedule(inputDeps);
        }
    }
}