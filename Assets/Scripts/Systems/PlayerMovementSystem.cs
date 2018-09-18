using Unity.Entities;
using Unity.Jobs;
using Jobs;

namespace Game.Systems
{
	public class PlayerMovementSystem : JobComponentSystem
	{
		protected override JobHandle OnUpdate(JobHandle inputDeps)
		{
			var job = new PlayerMovementJob();

			return job.Schedule(this, inputDeps);
		}
	}

}

