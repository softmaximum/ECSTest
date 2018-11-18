using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct Bomb : ISharedComponentData
    {
        public int Id;
    }
}