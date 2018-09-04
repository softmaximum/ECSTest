
using Game.Components;
using Game.Init;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UpdateGroups;

namespace Game.Systems
{
    [UpdateInGroup(typeof(ExecuteUpdateGroup))]
    public class BulletSystem : ComponentSystem
    {
        private struct BulletGroup
        {
            public ComponentDataArray<Bullet> Bullets;
            public ComponentDataArray<Position> Positions;
            public ComponentDataArray<DestroyEntity> Destroyed;
            public EntityArray Entites;
            public readonly int Length;
        }

        [Inject] private BulletGroup _group;

        protected override void OnUpdate()
        {
            for (var i = 0; i < _group.Length; i++)
            {
                var position = _group.Positions[i].Value;
                var entity = EntityManager.Instantiate(GetExplosionPrefab());
                EntityManager.AddComponentData(entity, new LifeTime{TimeLeft = 3.0f});
                EntityManager.AddComponentData(entity, new Explosion());
                EntityManager.SetComponentData(entity, new Position{Value = position});
            }
        }

        private static GameObject GetExplosionPrefab()
        {
            var main = Object.FindObjectOfType<Main>();
            return main.ExplosionPrefab;
        }
    }
}