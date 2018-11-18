using System.Collections.Generic;
using Game.Components;
using UI;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Systems
{
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
                _labelText.Label.text = $"Player: {players[i].Id}\nHealth: {healths[i].Value}";
                break;
            }
        }
    }
}