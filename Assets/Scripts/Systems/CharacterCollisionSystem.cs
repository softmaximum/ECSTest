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
    public class CharacterCollisionSystem : ComponentSystem
    {
        private ComponentGroup _bombsComponentGroup;
        private ComponentGroup _characterComponentGroup;

        private struct ExplosionInfo
        {
            public int PlayerId;
            public Position Position;
        }

        protected override void OnCreateManager()
        {
            _bombsComponentGroup =
                GetComponentGroup(
                    typeof(Collision),
                    typeof(Position),
                    typeof(Bomb),
                    typeof(Damage));

            _characterComponentGroup =
                GetComponentGroup(
                    typeof(Collision),
                    typeof(Position),
                    typeof(Character)
                    );
        }

        protected override void OnUpdate()
        {
            var bombPositions = _bombsComponentGroup.GetComponentDataArray<Position>();
            var bombCollisions = _bombsComponentGroup.GetSharedComponentDataArray<Collision>();
            var bombEntities = _bombsComponentGroup.GetEntityArray();

            var characterPositions = _characterComponentGroup.GetComponentDataArray<Position>();
            var characterCollisions = _characterComponentGroup.GetSharedComponentDataArray<Collision>();
            var characters = _characterComponentGroup.GetSharedComponentDataArray<Character>();
            var characterEnities = _characterComponentGroup.GetEntityArray();

            var explosions = new NativeList<ExplosionInfo>(Allocator.Temp);

            for (var i = 0; i < characterPositions.Length; i++)
            {
                var characterPosition = characterPositions[i];
                var characterCollision = characterCollisions[i];
                var character = characters[i];

                if (CheckCharacterCollision(characterEnities[i], characterPosition, characterCollision,
                    bombPositions, bombCollisions, bombEntities))
                {
                    explosions.Add(new ExplosionInfo{PlayerId = character.Id, Position = characterPosition});
                    break;
                }
            }

            for (var i = 0; i < explosions.Length; i++)
            {
                var info = explosions[i];
                CreateExplosion(info.Position.Value);
            }

            explosions.Dispose();
        }

        private bool CheckCharacterCollision(Entity character, Position characterPosition, Collision characterCollision,
            ComponentDataArray<Position> collisionPositions,
            SharedComponentDataArray<Collision> collisions,
            EntityArray bombs)
        {
            for (var i = 0; i < collisionPositions.Length; i++)
            {
                var distance = math.length(characterPosition.Value - collisionPositions[i].Value);
                if (distance <= characterCollision.Radius + collisions[i].Radius)
                {
                    var damage = EntityManager.GetSharedComponentData<Damage>(bombs[i]);
                    PostUpdateCommands.AddSharedComponent(character, new Damage {Value = damage.Value});
                    PostUpdateCommands.AddComponent(bombs[i], new DestroyEntity());
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
            var main = Object.FindObjectOfType<PrefabHolder>();
            return main.ExplosionPrefab;
        }
    }
}