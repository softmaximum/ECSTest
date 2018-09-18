using Jobs;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Game.Systems
{
    public class MovementSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new MovementJob{DeltaTime = Time.deltaTime};

            return job.Schedule(this, inputDeps);
        }

    }
}