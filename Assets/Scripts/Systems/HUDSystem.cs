using Game.Components;
using Game.Init;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Systems
{
    public class HUDSystem : ComponentSystem
    {
        private ComponentGroup _playerGroup;
        private Text _scoreText;

        protected override void OnCreateManager()
        {
            _playerGroup = GetComponentGroup(typeof(Player), typeof(Score));
            _scoreText = GameObject.FindObjectOfType<Text>();
        }

        protected override void OnUpdate()
        {
            var scores = _playerGroup.GetComponentDataArray<Score>();
            var players = _playerGroup.GetComponentDataArray<Player>();
            
            for (var i = 0; i < players.Length; i++)
            {
                if (players[i].Id == Main.MainPlayerId)
                {
                    _scoreText.text = string.Format("Score: {0}", scores[i].TotalScore);
                    break;
                }
            }
        }
    }
}