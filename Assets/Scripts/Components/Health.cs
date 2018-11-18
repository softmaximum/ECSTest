using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct Health : ISharedComponentData
    {
        public int Value;
    }
}