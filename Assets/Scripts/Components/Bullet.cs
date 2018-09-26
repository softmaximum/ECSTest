using Unity.Entities;
using UnityEngine;

namespace Game.Components
{
    public struct Bullet : IComponentData
    {
        public int PlayerId;
    }
}