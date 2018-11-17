using Game.Components;
using Unity.Entities;
using UnityEngine;

namespace Game.Init
{
    public class CharacterInit : MonoBehaviour
    {
        private const int Characterid = 1;
        [SerializeField] private GameObject _character;
        [SerializeField] private GameObject _bompPrefab;

        private void Start()
        {
            Application.targetFrameRate = -1;
            Init();
        }

        private void Init()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
            CreateCharacter(entityManager);
            CreateSpawner(entityManager);
        }

        private void CreateCharacter(EntityManager entityManager)
        {
            var character = entityManager.Instantiate(_character);
            entityManager.AddSharedComponentData(character, new Character {Id = Characterid});
            entityManager.AddComponentData(character, new CharacterPlayerInput());
            entityManager.AddComponentData(character, new Bullet {PlayerId = Characterid});
        }

        private void CreateSpawner(EntityManager entityManager)
        {
            var spawnerArchtype = entityManager.CreateArchetype
            (
                typeof(Spawner),
                typeof(Timer)
            );

            var spawner = entityManager.CreateEntity(spawnerArchtype);
            entityManager.SetSharedComponentData(spawner, new Spawner
            {
                Prefab = _bompPrefab,
                Count = 1,
                Radius = 5
            });
            entityManager.SetComponentData(spawner, new Timer{RepeatCount = 1, Interval = 3.0f});
        }
    }
}