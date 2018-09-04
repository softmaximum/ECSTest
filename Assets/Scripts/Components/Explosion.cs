using System;
using Unity.Entities;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public struct Explosion : ISharedComponentData
    {
        public GameObject Prefab;
    }
}