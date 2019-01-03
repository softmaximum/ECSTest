using Game.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine.VR;
using UpdateGroups;

namespace Game.Systems
{
    [UpdateAfter(typeof(DamageSystem))]
    public class SelectionController : ComponentSystem
    {
        private ComponentGroup _deadCharactersGroup;
        private ComponentGroup _charactersGroup;
        protected override void OnCreateManager()
        {
            _deadCharactersGroup = GetComponentGroup(
                typeof(Character),
                ComponentType.Create<Dead>());

            _charactersGroup = GetComponentGroup(
                typeof(Character),
                ComponentType.Subtractive<Dead>(),
                ComponentType.Subtractive<Selection>());
        }

        protected override void OnUpdate()
        {
            var entities = _deadCharactersGroup.GetEntityArray();

            if (entities.Length > 0)
            {
                SelectNextCharacter();
            }
        }

        private void SelectNextCharacter()
        {
            var entities = _charactersGroup.GetEntityArray();
            if (entities.Length > 0)
            {
                EntityManager.AddComponentData(entities[0], new Selection());
            }
        }
    }
}