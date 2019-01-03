using Game.Components;
using UI;
using Unity.Entities;
using UnityEngine;

namespace Game.Systems
{
	[UpdateAfter(typeof(CharacterCollisionSystem))]
    public class CharacterHUDSystem : ComponentSystem
    {
        private ComponentGroup _playerGroup;
        private PlayerLabelText _labelText;

        protected override void OnCreateManager()
        {
            _playerGroup = GetComponentGroup(
                typeof(Character),
                typeof(Health),
                typeof(Selection));
            _labelText = Object.FindObjectOfType<PlayerLabelText>();
        }

        protected override void OnUpdate()
        {
            var healths = _playerGroup.GetSharedComponentDataArray<Health>();
            var players = _playerGroup.GetSharedComponentDataArray<Character>();

            if (players.Length > 0)
            {
                var health = healths[0].Value;
                var text = health <= 0 ? "<color=red>Dead</color>" : health.ToString();
                _labelText.Label.text = $"Character: {players[0].Id}\n<color=green>Health: {text}</color>";
            }
            else
            {
                _labelText.Label.text = $"Character: None";
            }
        }
    }
}