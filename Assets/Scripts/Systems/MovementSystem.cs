using Jobs;
using Unity.Entities;
using Unity.Jobs;

namespace Game.Systems
{
    public class MovementSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new MovementJob();

            return job.Schedule(this, 1, inputDeps);
        }
        
    }
}