using Game.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Jobs
{
    public struct CharacterMovementJob : IJob
    {
        private const float MinSqrDistance = 0.01f;
        
        public ComponentDataArray<Position> Positons;
        public ComponentDataArray<CharacterMovement> Movements;
        public EntityArray Entities;
        public float DeltaTime;
        public EntityCommandBuffer Buffer;

        public void Execute()
        {
            for (var i = 0; i < Entities.Length; i++)
            {
                var position = Positons[i];
                var movement = Movements[i];
                var direction = math.normalize(movement.Target - position.Value);
                
                position.Value += direction * movement.Speed * DeltaTime;
                
                var distanceSqr = math.lengthsq(position.Value - movement.Target);       
                
                if (distanceSqr > movement.DistanceToTarget)
                {
                    Buffer.RemoveComponent<CharacterMovement>(Entities[i]);                    
                }

                movement.DistanceToTarget = distanceSqr;
                Positons[i] = position;
                Movements[i] = movement;
            }
        }
    }
}