using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct Character : ISharedComponentData
    {
        public int Id;
    }
}