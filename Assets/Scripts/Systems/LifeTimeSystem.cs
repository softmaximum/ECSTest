using Game.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Game.Systems
{
    public class LifeTimeSystem : ComponentSystem
    {
        private struct LifeTimeGroup
        {
            public ComponentDataArray<LifeTime> LifeTime;
            public EntityArray Entity;
            public readonly int Length;
        }

        [Inject]
        private LifeTimeGroup _group;
        private EntityManager _entityManager;

        protected override void OnCreateManager(int capacity)
        {
            base.OnCreateManager(capacity);
            _entityManager = World.Active.GetExistingManager<EntityManager>();
        }

        protected override void OnUpdate()
        {
            var deltaTime = Time.deltaTime;
            var entityToDestroy = new NativeList<Entity>(Allocator.Temp);
            for (var i = 0; i < _group.Length; i++)
            {
                var lifeTime = _group.LifeTime[i];
                lifeTime.TimeLeft -= deltaTime;
                _group.LifeTime[i] = lifeTime;
                if (lifeTime.TimeLeft <= 0)
                {
                    entityToDestroy.Add(_group.Entity[i]);
                }
            }

            for (var i = 0; i < entityToDestroy.Length; i++)
            {
                _entityManager.DestroyEntity(entityToDestroy[i]);
            }
            entityToDestroy.Dispose();
        }
    }
}