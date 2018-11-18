using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Jobs
{
    public struct CharacterMovementJob : IJobParallelFor
    {
        private const float MinSqrDistance = 0.01f;

        public ComponentDataArray<Position> Positions;
        public ComponentDataArray<CharacterMovement> Movements;
        public EntityArray Entities;
        public float DeltaTime;
        [ReadOnly]
        public EntityCommandBuffer Buffer;

        public void Execute(int i)
        {
            var position = Positions[i];
            var movement = Movements[i];

            position.Value += DeltaTime * movement.Speed;

            var distanceSqr = math.lengthsq(position.Value - movement.Target);

            if (distanceSqr > movement.DistanceToTarget)
            {
                Buffer.RemoveComponent<CharacterMovement>(Entities[i]);
            }

            movement.DistanceToTarget = distanceSqr;
            Positions[i] = position;
            Movements[i] = movement;
        }
    }
}