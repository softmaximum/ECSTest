//#define USE_JOB

using Game.Init;
using Unity.Entities;
using Unity.Rendering;
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

            private EntityManager _entityManager;

            protected override void OnCreateManager(int capacity)
            {
                _entityManager = World.Active.GetExistingManager<EntityManager>();
            }

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

            private void MakeFire(float3 position, float3 direction)
            {
                var entity = _entityManager.Instantiate(GetBulletPrefab());
                _entityManager.AddComponentData(entity, new Movement(direction, 0.1f));
                _entityManager.AddComponentData(entity, new LifeTime{TimeLeft = 3.0f});
            }

            private static GameObject GetBulletPrefab()
            {
                var main = Object.FindObjectOfType<Main>();
                return main.BulletPrefab;
            }
        }
#endif
}