using Game.Components;
using Unity.Entities;
using UnityEngine;

namespace Jobs
{
    public struct LifeTimeJob : IJobProcessComponentData<LifeTime>
    {
        public float DeltaTime;

        public void Execute(ref LifeTime data)
        {
            data.TimeLeft -= DeltaTime;
            if (data.TimeLeft <= 0)
            {
                Debug.Log("REMOVE");
//                var entityManager = World.Active.GetExistingManager<EntityManager>();
//                entityManager.DestroyEntity(data.Entity);
            }
        }
    }
}