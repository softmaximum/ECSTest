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
                typeof(Health));
            _labelText = Object.FindObjectOfType<PlayerLabelText>();
        }

        protected override void OnUpdate()
        {
            var healths = _playerGroup.GetSharedComponentDataArray<Health>();
            var players = _playerGroup.GetSharedComponentDataArray<Character>();

            for (var i = 0; i < players.Length; i++)
            {
                var health = healths[i].Value;
                var text = health <= 0 ? "<color=red>Dead</color>" : health.ToString();
                _labelText.Label.text = $"Player: {players[i].Id}\n<color=green>Health: {text}</color>";
                break;
            }
        }
    }
}