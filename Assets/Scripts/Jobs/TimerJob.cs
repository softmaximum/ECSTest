using Game.Components;
using Unity.Entities;
using Unity.Jobs;

namespace Jobs
{
    public class TimerJob : IJob
    {
        public ComponentDataArray<Timer> Timer;
        public EntityArray Entity;
        public float DeltaTime;
        public EntityCommandBuffer CommandBuffer;

        public void Execute()
        {
            for (var i = 0; i < Entity.Length; i++)
            {
                var lifeTime = Timer[i];
                lifeTime.Time += DeltaTime;
                if (lifeTime.Time >= lifeTime.Interval)
                {
                    CommandBuffer.AddComponent(Entity[i], new TimerElapsed());
                }
                Timer[i] = lifeTime;
            }
        }
    }
}