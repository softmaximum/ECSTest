using Unity.Entities;
using Unity.Mathematics;

namespace Game.Components
{
    public struct Movement : IComponentData
    {
        public float3 direction;
        public float speed;

        public Movement(float3 direction, float speed)
        {
            this.direction = math.normalize(direction);
            this.speed = speed;
        }
    }
}