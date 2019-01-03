using Game.Components;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    public class DamageSystem : ComponentSystem
    {
        public struct HealthGroup
        {
            [ReadOnly]
            public SharedComponentDataArray<Health> Healts;
            [ReadOnly]
            public SharedComponentDataArray<Damage> Damages;
            public EntityArray Entity;
            public readonly int Length;
        }

        [Inject]
        private HealthGroup _group;

        protected override void OnUpdate()
        {
            for (var i = 0; i < _group.Length; i++)
            {
                var health = _group.Healts[i];
                var damage = _group.Damages[i];
                var newHealth = health.Value - damage.Value;
                var entity = _group.Entity[i];

                if (newHealth <= 0)
                {
                    newHealth = 0;
                    PostUpdateCommands.AddComponent(entity, new Dead());
                    PostUpdateCommands.AddComponent(entity, new DestroyEntity());
                }

                health.Value = newHealth;
                PostUpdateCommands.SetSharedComponent(entity, health);
                PostUpdateCommands.RemoveComponent<Damage>(entity);
            }
        }
    }
}