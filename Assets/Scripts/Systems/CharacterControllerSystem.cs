using Game.Components;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Systems
{
    public class CharacterControllerSystem : ComponentSystem
    {
        private const float MovementSpeed = 10.0f;
        private struct MovementGroup
        {
            public ComponentDataArray<CharacterPlayerInput> Inputs;
            [ReadOnly]
            public SharedComponentDataArray<Character> Characters;
            [ReadOnly]
            public ComponentDataArray<Position> Positions;
            public EntityArray Entities;
            public readonly int Length;
        }

        [Inject]
        private MovementGroup _group;

        protected override void OnUpdate()
        {
            for (var i = 0; i < _group.Inputs.Length; i++)
            {
                var input = _group.Inputs[i];
                if (input.Clicked <= 0)
                    continue;

                input.Clicked = 0;
                _group.Inputs[i] = input;

                var position = _group.Positions[i];
                var direction = math.normalize(input.ClickPosition - position.Value);
                var speed = direction * MovementSpeed;
                var movement = new CharacterMovement(input.ClickPosition, speed);

                if (EntityManager.HasComponent<CharacterMovement>(_group.Entities[i]))
                {
                    PostUpdateCommands.SetComponent(_group.Entities[i], movement);
                }
                else
                {
                    PostUpdateCommands.AddComponent(_group.Entities[i], movement);
                }
            }
        }
    }
}