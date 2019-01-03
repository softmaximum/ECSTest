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
            public readonly int Length;
        }

        [Inject]
        private MovementGroup _group;

        private ComponentGroup _selectedCharactersGroup;

        protected override void OnCreateManager()
        {
            base.OnCreateManager();
            _selectedCharactersGroup = GetComponentGroup(
                typeof(Character),
                typeof(Position),
                typeof(Selection));
        }

        protected override void OnUpdate()
        {
            for (var i = 0; i < _group.Inputs.Length; i++)
            {
                var input = _group.Inputs[i];
                if (input.Clicked <= 0)
                    continue;

                input.Clicked = 0;
                _group.Inputs[i] = input;

                MoveCharacter(input);
            }
        }

        private void MoveCharacter(CharacterPlayerInput input)
        {
            var positions = _selectedCharactersGroup.GetComponentDataArray<Position>();
            var entities = _selectedCharactersGroup.GetEntityArray();

            if (positions.Length <= 0)
                return;

            var position = positions[0];
            var direction = math.normalize(input.ClickPosition - position.Value);
            var speed = direction * MovementSpeed;
            var movement = new CharacterMovement(input.ClickPosition, speed);

            if (EntityManager.HasComponent<CharacterMovement>(entities[0]))
            {
                PostUpdateCommands.SetComponent(entities[0], movement);
            }
            else
            {
                PostUpdateCommands.AddComponent(entities[0], movement);
            }
        }
    }
}