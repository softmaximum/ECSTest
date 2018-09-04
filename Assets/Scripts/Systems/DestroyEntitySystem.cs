using Game.Components;
using Unity.Entities;
using UnityEngine;
using UpdateGroups;

namespace Game.Systems
{
    [UpdateInGroup(typeof(CleanupUpdateGrpup))]
    public class DestroyEntitySystem : ComponentSystem
    {
        private struct DestroyGroup
        {
            public ComponentDataArray<DestroyEntity> DestroyEntity;
            public EntityArray Entity;
            public readonly int Length;
        }

        [Inject]
        private DestroyGroup _group;

        protected override void OnUpdate()
        {
            for (var i = 0; i < _group.Length; i++)
            {
                EntityManager.DestroyEntity(_group.Entity[i]);
            }
        }
    }
}