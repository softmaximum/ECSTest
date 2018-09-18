using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct Collision : ISharedComponentData
    {
        public float Radius;
    }
}