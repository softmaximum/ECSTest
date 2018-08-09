using Game.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Systems
{
    public class DeleteBlocksSystem : ComponentSystem
    {
        public struct PlayerGroup
        {
            public ComponentDataArray<Position> PlayerPositiopns;
            public ComponentDataArray<PlayerInput> PlayerInputs;
            public readonly int Length;
        }

        public struct BlocksGroup
        {
            public ComponentDataArray<Block> Blocks;
            public ComponentDataArray<Position> Positions;
            public EntityArray EntityArray;
            public readonly int Length;
        }

        [Inject] private PlayerGroup _playerGroup;
        [Inject] private BlocksGroup _blocksGroup;

        protected override void OnUpdate()
        {
            for (int i = 0; i < _blocksGroup.Length; i++)
            {
                var dist = math.distance(_playerGroup.PlayerPositiopns[0].Value, _blocksGroup.Positions[i].Value);
                if (dist < 1)
                {
                    PostUpdateCommands.DestroyEntity(_blocksGroup.EntityArray[i]);
                }
            }
        }
    }
}