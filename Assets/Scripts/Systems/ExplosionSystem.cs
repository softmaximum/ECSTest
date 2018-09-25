using Game.Components;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    public class ExplosionSystem : ComponentSystem
    {
        private struct ExplosionGroup
        {
            [ReadOnly]
            public ComponentDataArray<Explosion> Explosions;
            public EntityArray Entites;
            public readonly int Length;
        }

        [Inject] private ExplosionGroup _group;

        protected override void OnUpdate()
        {
            for (var i = 0; i < _group.Length; i++)
            {
                MakeDamage(_group.Explosions[i]);
            }
        }

        private void MakeDamage(Explosion explosion)
        {

        }
    }
}