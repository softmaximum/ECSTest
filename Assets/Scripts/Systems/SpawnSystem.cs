
using Game.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Game.Systems
{
    public class SpawnSystem : ComponentSystem
    {
        private struct SpawnGroup
        {
            [ReadOnly]
            public SharedComponentDataArray<Spawner> Spawner;
            public ComponentDataArray<Position> Position;
            public EntityArray Entity;
            public readonly int Length;
        }

        [Inject] private SpawnGroup _group;

        protected override void OnUpdate()
        {
            while (_group.Length != 0)
            {
                var spawner = _group.Spawner[0];
                var sourceEntity = _group.Entity[0];
                var center = _group.Position[0].Value;

                var entities = new NativeArray<Entity>(spawner.Count, Allocator.Temp);
                var positions = new NativeArray<float3>(spawner.Count, Allocator.Temp);

                EntityManager.Instantiate(spawner.Prefab, entities);

                RandomPoints(center, spawner.Radius, ref positions);
                for (var i = 0; i < spawner.Count; i++)
                {
                    var position = new Position
                    {
                        Value = positions[i]
                    };
                    EntityManager.SetComponentData(entities[i], position);
                }
                entities.Dispose();
                positions.Dispose();

                EntityManager.RemoveComponent<Spawner>(sourceEntity);

                // Instantiate & AddComponent & RemoveComponent calls invalidate the injected groups,
                // so before we get to the next spawner we have to reinject them
                UpdateInjectedComponentGroups();
            }
        }

        private static void RandomPoints(float3 center, float radius, ref NativeArray<float3> points)
        {
            var count = points.Length;
            for (int i = 0; i < count; i++)
            {
                var angle = Random.Range(0.0f, Mathf.PI * 2.0f);
                points[i] = center + new float3
                {
                    x = math.sin(angle) * radius,
                    y = 0,
                    z = math.cos(angle) * radius
                };
            }
        }
    }
}
