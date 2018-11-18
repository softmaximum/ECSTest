
using Game.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Systems
{
    public class SpawnSystem : ComponentSystem
    {
        private struct SpawnGroup
        {
            [ReadOnly]
            public SharedComponentDataArray<Spawner> Spawner;
            public ComponentDataArray<TimerElapsed> TimerElaped;
            public EntityArray Entity;
            public readonly int Length;
        }

        [Inject] private SpawnGroup _group;

        protected override void OnUpdate()
        {

            for (var i = 0; i < _group.Length; i++)
            {
                var spawner = _group.Spawner[i];
                PostUpdateCommands.RemoveComponent<TimerElapsed>(_group.Entity[i]);
                Spawn(spawner.Prefab);
            }
        }

        private void Spawn(GameObject prefab)
        {
            var entity = EntityManager.Instantiate(prefab);
            EntityManager.SetComponentData(entity, new Position {Value = GetRandomPosition()});
        }

        private static float3 GetRandomPosition()
        {
            const int randomRange = 16;
            var x = Random.Range(-randomRange, randomRange + 1);
            var z = Random.Range(-randomRange, randomRange + 1);
            return new float3(x, 0.0f, z);
        }
    }
}
