using Game.Components;
using Unity.Entities;
using UnityEngine;

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
                var repeatCount = timer.RepeatCount;
                repeatCount--;
                timer.RepeatCount = repeatCount;

                if (timer.RepeatCount == 0)
                {
                    EntityManager.AddComponent(_grpup.Entity[i], ComponentType.Create<DestroyEntity>());
                }
                EntityManager.RemoveComponent<TimerElapsed>(_grpup.Entity[i]);
            }
        }
    }
}