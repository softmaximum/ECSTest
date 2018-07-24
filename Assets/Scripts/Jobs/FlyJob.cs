using Game.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Jobs
{
    public struct FlyJob : IJobProcessComponentData<Position, Fly>
    {
        public float DeltaTime;
        public bool FlyAway;
        
        public void Execute(ref Position position, ref Fly fly)
        {
            if (!FlyAway)
                return;
            var xMove = Mathf.PerlinNoise(position.Value.x, position.Value.y) - 1.5f;
            var yMove = Mathf.PerlinNoise(position.Value.y, position.Value.x) + 0.5f;
            position.Value += DeltaTime * new float3(xMove, yMove, 0);
        }
    }
}