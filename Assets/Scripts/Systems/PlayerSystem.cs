using Game.Components;
using Unity.Entities;
using UnityEngine;

namespace Game.Systems
{
    public class PlayerSystem : ComponentSystem
    {
        private struct PlayerGroup
        {
            public ComponentDataArray<Player> Players;
            public ComponentDataArray<Score> Scores;
            public ComponentDataArray<ScorePoint> Points;
            public EntityArray Entites;
            public readonly int Length;   
        }

        [Inject] private PlayerGroup _group; 

        protected override void OnUpdate()
        {
            for (var i = 0; i < _group.Length; i++)
            {
                var score = _group.Scores[i];
                score.TotalScore += _group.Points[i].Points;
                _group.Scores[i] = score;
                PostUpdateCommands.RemoveComponent<ScorePoint>(_group.Entites[i]);
            }
        }
    }
}