using Game.Components;
using Unity.Entities;
using UnityEngine;

namespace Game.Init
{
    public class CharacterInit : MonoBehaviour
    {
        private const int Characterid = 1;
        [SerializeField] private GameObject _character;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
            var character = entityManager.Instantiate(_character);
            entityManager.AddSharedComponentData(character, new Character{Id = Characterid});
            entityManager.AddComponentData(character, new CharacterPlayerInput());
        }
    }
}