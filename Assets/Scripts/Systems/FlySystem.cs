using Game.Components;
using Jobs;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

namespace Game.Systems
{
    public class FlySystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new FlyJob
            {
                DeltaTime = Time.deltaTime,
                FlyAway = Input.GetKey(KeyCode.F)
            };
            return job.Schedule(this, 1, inputDeps);
        }
    }
}