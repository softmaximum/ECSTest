using Jobs;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Game.Systems
{
    public class PlayerInputSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new PlayerInputJob
            {
                Left = Input.GetKeyDown(KeyCode.A),
                Right = Input.GetKeyDown(KeyCode.D),
                Up = Input.GetKeyDown(KeyCode.W),
                Down = Input.GetKeyDown(KeyCode.S),
                UpLeft = Input.GetKeyDown(KeyCode.Q),
                UpRight = Input.GetKeyDown(KeyCode.E),
                DownLeft = Input.GetKeyDown(KeyCode.Z),
                DownRight = Input.GetKeyDown(KeyCode.X),
                Fire = Input.GetKeyDown(KeyCode.G),
                RightMouseButton = Input.GetMouseButton(1),
                MousePosition =
                {
                    x = Input.mousePosition.x,
                    y = Input.mousePosition.y
                }

            };
            return job.Schedule(this,inputDeps);
        }
    }
}