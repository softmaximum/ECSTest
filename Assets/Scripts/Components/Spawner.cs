using System;
using Unity.Entities;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public struct Spawner : ISharedComponentData
    {
        public GameObject Prefab;
        public int Count;
    }}