using Game.Components;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(UpdateGroups.CleanupUpdateGrpup))]
    [UpdateBefore(typeof(DestroyEntitySystem))]
    public class TimerElapsedSystem : ComponentSystem
    {
        private struct TimeElapsedGrpup
        {
            public ComponentDataArray<TimerElapsed> TimerElapsed;
            public ComponentDataArray<Timer> Timer;
            public EntityArray Entity;
            public readonly int Length;
        }

        [Inject] private TimeElapsedGrpup _grpup;

        protected override void OnUpdate()
        {
            for (var i = 0; i < _grpup.Length; i++)
            {
                var timer = _grpup.Timer[i];
                if (!timer.Repeat)
                {
                    EntityManager.AddComponent(_grpup.Entity[i], ComponentType.Create<DestroyEntity>());
                }
                EntityManager.RemoveComponent<TimerElapsed>(_grpup.Entity[i]);
            }
        }
    }
}