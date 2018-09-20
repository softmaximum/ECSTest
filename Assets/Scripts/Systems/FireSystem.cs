using Game.Init;
using Unity.Entities;
using UnityEngine;

using Game.Components;
using Unity.Mathematics;
using Unity.Transforms;
using UpdateGroups;

namespace Game.Systems
{
    [UpdateInGroup(typeof(ExecuteUpdateGroup))]
    public class FireSystem : ComponentSystem
    {
        private struct PlayerGroup
        {
            public ComponentDataArray<Player> Player;
            public ComponentDataArray<Position> Position;
            public ComponentDataArray<Rotation> Rotation;
            public EntityArray Entity;
            public readonly int Length;
        }

        [Inject] private PlayerGroup _group;

        protected override void OnUpdate()
        {
            for (var i = 0; i < _group.Length; i++)
            {
                if (Input.GetKeyUp(KeyCode.G))
                {
                    MakeFire(_group.Position[i].Value, math.up());
                }
            }
        }

        private void MakeFire(float3 position, float3 direction)
        {
            var entity = EntityManager.Instantiate(GetBulletPrefab());
            EntityManager.SetComponentData(entity, new Position{Value = position});
            EntityManager.AddComponentData(entity, new Movement(direction, 2f));
            EntityManager.AddComponentData(entity, new LifeTime{TimeLeft = 30.0f});
            EntityManager.AddComponentData(entity, new Bullet());
        }

        private static GameObject GetBulletPrefab()
        {
            var main = Object.FindObjectOfType<Main>();
            return main.BulletPrefab;
        }
    }
}