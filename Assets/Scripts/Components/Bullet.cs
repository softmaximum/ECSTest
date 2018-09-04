using Unity.Entities;
using UnityEngine;

namespace Game.Components
{
    public struct Bullet : ISharedComponentData
    {
        public GameObject Prefab;
    }
}