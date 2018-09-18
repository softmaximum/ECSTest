using Game.Components;
using Jobs;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Game.Systems
{
    [UpdateInGroup(typeof(UpdateGroups.ExecuteUpdateGroup))]
    public class TimerSystem : JobComponentSystem
    {
        private struct TimerGroup
        {
            public ComponentDataArray<Timer> Timer;
            public EntityArray Entity;
            public readonly int Length;
        }

        [Inject] private TimerSystemBarrier _timerSystemBarrier;
        [Inject] private TimerGroup _group;


        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new TimerJob
            {
                DeltaTime = Time.deltaTime,
                Entity = _group.Entity,
                Timer = _group.Timer,
                CommandBuffer = _timerSystemBarrier.CreateCommandBuffer(),
            };
            
            return job.Schedule(inputDeps);
        }
    }
}