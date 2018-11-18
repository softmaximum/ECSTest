using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Components
{
    public struct CharacterMovement : IComponentData
    {
        public float3 Target;
        public float DistanceToTarget;
        public float3 Speed;

        public CharacterMovement(float3 target, float3 speed)
        {
            Speed = speed;
            Target = target;
            DistanceToTarget = float.MaxValue;
        }
    }
}