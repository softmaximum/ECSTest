using Game.Components;
using Unity.Entities;
using Unity.Collections;

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
                PostUpdateCommands.AddComponent(_group.Entities[i], new CharacterMovement(input.ClickPosition, MovementSpeed));
            }
        }
    }
}