using Game.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Jobs
{
    public struct CollisionJob : IJob
    {
        public SharedComponentDataArray<Collision> Collisions;
        public ComponentDataArray<Position> Positions;
        public EntityArray Entity;
        public EntityCommandBuffer CommandBuffer;
        
        public void Execute()
        {
            for (var i = 0; i < Entity.Length; i++)
            {
                for (var j = i+1; j < Entity.Length; j++)
                {                 
                    if (Entity[i] == Entity[j])
                        continue;
                    
                    var distance = math.length(Positions[i].Value - Positions[j].Value);
                    
                    if (!(distance <= Collisions[i].Radius + Collisions[j].Radius))
                        continue;
                    CommandBuffer.AddComponent(Entity[i], new DestroyEntity());
                    CommandBuffer.AddComponent(Entity[j], new DestroyEntity());
                }
            }
        }
    }
}