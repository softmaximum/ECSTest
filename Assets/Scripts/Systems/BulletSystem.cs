using System.Collections.Generic;
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
        private int PointPerExsplosion = 10;
        private ComponentGroup _targetsComponentGroup;
        private ComponentGroup _bulletsComponentGroup;
        private ComponentGroup _playerGroup;
        private Dictionary<int, int> _scoresForPlayer;

        private struct ExplosionInfo
        {
            public int PlayerId;
            public Position Position;
        }

        protected override void OnCreateManager()
        {
            _targetsComponentGroup =
                GetComponentGroup(typeof(Collision), typeof(Position), ComponentType.Subtractive<Bullet>());
            _bulletsComponentGroup = GetComponentGroup(typeof(Bullet), typeof(Position), typeof(Collision));
            _playerGroup = GetComponentGroup(typeof(Player));
            _scoresForPlayer = new Dictionary<int, int>();
        }

        protected override void OnUpdate()
        {
            var collisionPositions = _targetsComponentGroup.GetComponentDataArray<Position>();
            var collisions = _targetsComponentGroup.GetSharedComponentDataArray<Collision>();
            var entities = _targetsComponentGroup.GetEntityArray();

            var bulletPositions = _bulletsComponentGroup.GetComponentDataArray<Position>();
            var bulletCollisions = _bulletsComponentGroup.GetSharedComponentDataArray<Collision>();
            var bulletEnities = _bulletsComponentGroup.GetEntityArray();
            var bullets = _bulletsComponentGroup.GetComponentDataArray<Bullet>();

            var explosions = new NativeList<ExplosionInfo>(Allocator.Temp);
            var entitiesToDestroy = new NativeList<Entity>(Allocator.Temp);

            for (var i = 0; i < bulletPositions.Length; i++)
            {
                var bulletPosition = bulletPositions[i];
                var bulletCollision = bulletCollisions[i];
                var bullet = bullets[i];

                if (CheckBulletCollision(bulletEnities[i], bulletPosition, bulletCollision,
                    collisionPositions, collisions, entities, entitiesToDestroy))
                {
                    explosions.Add(new ExplosionInfo{PlayerId = bullet.PlayerId, Position = bulletPosition});
                }
            }

            for (var i = 0; i < explosions.Length; i++)
            {
                var info = explosions[i];
                CreateExplosion(info.Position.Value);
                if (_scoresForPlayer.ContainsKey(info.PlayerId))
                {
                    _scoresForPlayer[info.PlayerId] += PointPerExsplosion;
                }
                else
                {
                    _scoresForPlayer.Add(info.PlayerId, PointPerExsplosion);
                }
            }

            foreach (var player in _scoresForPlayer)
            {
                AddScore(player.Key, player.Value);
            }
            _scoresForPlayer.Clear();

            DestroyEntities(entitiesToDestroy);
            explosions.Dispose();
            entitiesToDestroy.Dispose();
        }

        private bool CheckBulletCollision(Entity bullet, Position bulletPosition, Collision bulletCollision,
            ComponentDataArray<Position> collisionPositions,
            SharedComponentDataArray<Collision> collisions,
            EntityArray entities, NativeList<Entity> entitiesToDestroy)
        {
            for (var i = 0; i < collisionPositions.Length; i++)
            {
                var distance = math.length(bulletPosition.Value - collisionPositions[i].Value);
                if (distance <= bulletCollision.Radius + collisions[i].Radius)
                {
                    if (!entitiesToDestroy.Contains(entities[i]))
                    {
                        entitiesToDestroy.Add(entities[i]);
                    }

                    if (!entitiesToDestroy.Contains(bullet))
                    {
                        entitiesToDestroy.Add(bullet);
                    }

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

        private void AddScore(int playerId, int points)
        {
            var entiites = _playerGroup.GetEntityArray();
            var players = _playerGroup.GetComponentDataArray<Player>();
            for (var i = 0; i < entiites.Length; i++)
            {
                if (players[i].Id == playerId)
                {
                    PostUpdateCommands.AddComponent(entiites[i], new ScorePoint {Points = points});
                }
            }
        }

        private void DestroyEntities(NativeList<Entity> entities)
        {
            for (var i = 0; i < entities.Length; i++)
            {
                EntityManager.AddComponent(entities[i], ComponentType.Create<DestroyEntity>());
            }
        }

        private static GameObject GetExplosionPrefab()
        {
            var main = Object.FindObjectOfType<PrefabHolder>();
            return main.ExplosionPrefab;
        }
    }
}