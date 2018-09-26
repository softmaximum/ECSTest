using System.Collections.Generic;
using Game.Components;
using UI;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Systems
{
    public class HUDSystem : ComponentSystem
    {
        private ComponentGroup _playerGroup;
        private PlayerScoreText[] _scores;
        private Dictionary<int, Text> _texts;

        protected override void OnCreateManager()
        {
            _playerGroup = GetComponentGroup(typeof(Player), typeof(Score));
            _scores = GameObject.FindObjectsOfType<PlayerScoreText>();
            _texts = new Dictionary<int, Text>();
            
            foreach (var scoreText in _scores)
            {
                _texts.Add(scoreText.PlayerId, scoreText._Text);
            }
        }

        protected override void OnUpdate()
        {
            var scores = _playerGroup.GetComponentDataArray<Score>();
            var players = _playerGroup.GetComponentDataArray<Player>();
            
            for (var i = 0; i < players.Length; i++)
            {
                if (_texts.TryGetValue(players[i].Id, out var scoreText))
                {
                    scoreText.text = $"Player: {players[i].Id}\nScore: {scores[i].TotalScore}";
                }
            }
        }
    }
}