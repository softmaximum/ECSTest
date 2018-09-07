using Jobs;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Game.Systems
{
    [UpdateInGroup(typeof(UpdateGroups.ExecuteUpdateGroup))]
    public class TimerSystem : JobComponentSystem
    {
        [Inject] private TimerSystemBarrier _timerSystemBarrier;

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new TimerJob
            {
                DeltaTime = Time.deltaTime,
                CommandBuffer = _timerSystemBarrier.CreateCommandBuffer(),
            };
            return base.OnUpdate(inputDeps);
        }
    }
}