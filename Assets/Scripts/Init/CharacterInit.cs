using Game.Components;
using Unity.Entities;
using UnityEngine;

namespace Game.Init
{
    public class CharacterInit : MonoBehaviour
    {
        private const int FirstCharacterid = 1;
        private const int SecondCharacterid = 2;

        [SerializeField] private GameObject _firstCharacter;
        [SerializeField] private GameObject _secondCharacter;
        [SerializeField] private GameObject _bompPrefab;

        private void Start()
        {
            Application.targetFrameRate = -1;
            Init();
        }

        private void Init()
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
            CreateCharacterInput(entityManager);
            CreateCharacter(entityManager, _firstCharacter, FirstCharacterid, true);
            CreateCharacter(entityManager, _secondCharacter, SecondCharacterid, false);
            CreateSpawner(entityManager);
        }

        private static void CreateCharacterInput(EntityManager entityManager)
        {
            var entity = entityManager.CreateEntity();
            entityManager.AddComponentData(entity, new CharacterPlayerInput());
        }

        private static void CreateCharacter(EntityManager entityManager, GameObject prefab, int id, bool selected)
        {
            var character = entityManager.Instantiate(prefab);
            entityManager.AddSharedComponentData(character, new Character {Id = id});
            if (selected)
            {
                entityManager.AddComponentData(character, new Selection());
            }
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
                Count = 1
            });
            entityManager.SetComponentData(spawner, new Timer{RepeatCount = int.MaxValue, Interval = 10.0f});
        }
    }
}