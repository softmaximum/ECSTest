//#define USE_JOB

using Unity.Entities;
using UnityEngine;

#if USE_JOB
using Jobs;
using Unity.Jobs;
#else
using Game.Components;
using Unity.Mathematics;
using Unity.Transforms;
#endif





namespace Game.Systems
{
#if USE_JOB
        public class FireSystem : JobComponentSystem
        {
            protected override JobHandle OnUpdate(JobHandle inputDeps)
            {
                var job = new FireJob
                {
                    Fire = Input.GetKeyUp(KeyCode.G)
                };

                return job.Schedule(this, 1, inputDeps);
            }
        }
#else
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
                        MakeFire(_group.Position[i].Value, math.forward(_group.Rotation[i].Value));
                    }
                }
            }

            private static void MakeFire(float3 position, float3 direction)
            {
                var entityManager = World.Active.GetOrCreateManager<EntityManager>();
                var fireEntityArchetype = entityManager.CreateArchetype
                (
                    typeof(Movement),
                    typeof(Position)
                );

                var entity = entityManager.CreateEntity(fireEntityArchetype);
                entityManager.SetComponentData(entity, new Position{Value = position});
                entityManager.SetComponentData(entity, new Movement(direction, 1.0f));
            }
        }
#endif
}