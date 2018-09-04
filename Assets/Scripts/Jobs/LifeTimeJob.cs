using Game.Components;
using Unity.Entities;
using Unity.Jobs;

namespace Jobs
{
    public struct LifeTimeJob : IJob
    {
        public ComponentDataArray<LifeTime> LifeTime;
        public EntityArray Entity;
        public float DeltaTime;
        public EntityCommandBuffer CommandBuffer;

        public void Execute()
        {
            for (var i = 0; i < Entity.Length; i++)
            {
                var lifeTime = LifeTime[i];
                lifeTime.TimeLeft -= DeltaTime;
                LifeTime[i] = lifeTime;
                if (lifeTime.TimeLeft <= 0)
                {
                    CommandBuffer.AddComponent(Entity[i], new DestroyEntity());
                }
            }
        }
    }
}