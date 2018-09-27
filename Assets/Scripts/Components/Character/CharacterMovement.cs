using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Components
{
    public struct CharacterMovement : IComponentData
    {
        public float3 Target;
        public float DistanceToTarget;
        public float Speed;

        public CharacterMovement(float3 target, float speed)
        {
            Target = target;
            Speed = speed;
            DistanceToTarget = float.MaxValue;
        }
    }
}