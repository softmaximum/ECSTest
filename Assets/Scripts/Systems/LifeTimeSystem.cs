using Game.Components;
using Jobs;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Game.Systems
{
    public class LifeTimeSystem : JobComponentSystem
    {
        private struct LifeTimeGroup
        {
            public ComponentDataArray<LifeTime> LifeTime;
            public EntityArray Entity;
            public readonly int Length;
        }

        [Inject] private LifeTimeGroup _group;
        [Inject] private LifeTimeSystemBarrier _lifeTimeSystemBarrier;

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new LifeTimeJob
            {
                DeltaTime = Time.deltaTime,
                LifeTime = _group.LifeTime,
                Entity = _group.Entity,
                CommandBuffer = _lifeTimeSystemBarrier.CreateCommandBuffer()
            };

            return job.Schedule(inputDeps);
        }
    }
}