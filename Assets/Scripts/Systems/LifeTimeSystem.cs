using Game.Components;
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
            for (var i = 0; i < _group.Length; i++)
            {
                var lifeTime = _group.LifeTime[i];
                lifeTime.TimeLeft -= deltaTime;
                _group.LifeTime[i] = lifeTime;
                if (lifeTime.TimeLeft <= 0)
                {
                    _entityManager.DestroyEntity(_group.Entity[i]);
                }
            }
        }
    }
}