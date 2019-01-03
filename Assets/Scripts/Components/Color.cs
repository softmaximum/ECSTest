using System;
using Unity.Entities;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public struct CharacterColor : ISharedComponentData
    {
        public Color Color;
    }
}