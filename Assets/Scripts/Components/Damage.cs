using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct Damage : ISharedComponentData
    {
		public int Value;
    }
}