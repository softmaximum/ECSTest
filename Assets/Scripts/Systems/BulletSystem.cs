using Unity.Collections;
using Game.Components;
using Game.Init;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UpdateGroups;
using Collision = Game.Components.Collision;

namespace Game.Systems
{
    [UpdateInGroup(typeof(ExecuteUpdateGroup))]
    public class BulletSystem : ComponentSystem
    {
        private struct BulletGroup
        {
            public ComponentDataArray<Bullet> Bullets;
            public ComponentDataArray<Position> Positions;
            [ReadOnly]
            public SharedComponentDataArray<Collision> Collisions;
            public EntityArray Entites;
            public readonly int Length;
        }

        [Inject] private BulletGroup _group;
        private ComponentGroup _targetsComponentGroup;

        protected override void OnCreateManager()
        {
            _targetsComponentGroup =
                GetComponentGroup(typeof(Collision), typeof(Position), ComponentType.Subtractive<Bullet>());
        }

        protected override void OnUpdate()
        {
            var collisionPositions = _targetsComponentGroup.GetComponentDataArray<Position>();
            var collisions = _targetsComponentGroup.GetSharedComponentDataArray<Collision>();
            var entities = _targetsComponentGroup.GetEntityArray();
            var explosions = new NativeList<Position>(Allocator.Temp);

            for (var i = 0; i < _group.Length; i++)
            {
                var bulletPosition = _group.Positions[i];
                var bulletCollision = _group.Collisions[i];
                if (CheckBulletCollision(_group.Entites[i], bulletPosition, bulletCollision,
                    collisionPositions, collisions, entities))
                {
                    explosions.Add(bulletPosition);
                }
            }

            for (var i = 0; i < explosions.Length; i++)
            {
                CreateExplosion(explosions[i].Value);
            }
            explosions.Dispose();
        }

        private bool CheckBulletCollision(Entity bullet, Position bulletPosition, Collision bulletCollision,
            ComponentDataArray<Position> collisionPositions,
            SharedComponentDataArray<Collision> collisions,
            EntityArray entities)
        {
            for (var i = 0; i < collisionPositions.Length; i++)
            {
                var distance = math.length(bulletPosition.Value - collisionPositions[i].Value);
                if (distance <= bulletCollision.Radius + collisions[i].Radius)
                {
                    PostUpdateCommands.AddComponent(entities[i],  new DestroyEntity());
                    PostUpdateCommands.AddComponent(bullet, new DestroyEntity());
                    return true;
                }
            }
            return false;
        }

        private void CreateExplosion(float3 position)
        {
            var entity = EntityManager.Instantiate(GetExplosionPrefab());
            EntityManager.AddComponentData(entity, new LifeTime {TimeLeft = 3.0f});
            EntityManager.AddComponentData(entity, new Explosion());
            EntityManager.SetComponentData(entity, new Position {Value = position});
        }

        private static GameObject GetExplosionPrefab()
        {
            var main = Object.FindObjectOfType<Main>();
            return main.ExplosionPrefab;
        }
    }
}