using Game.Components;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Game.Init
{
	public class Main : MonoBehaviour
	{
		public const int FirstPlayerId = 1;
		private const int SecondPlayerId = 2;
		
		[SerializeField] private Mesh _playerMesh;
		[SerializeField] private Material _playerMaterial;
		[SerializeField] private GameObject _spawnPrefab;
		[SerializeField] private GameObject _bulletPrefab;
		[SerializeField] private GameObject _explosionPrefab;

		public GameObject BulletPrefab => _bulletPrefab;
		public GameObject ExplosionPrefab => _explosionPrefab;

		private void Start()
		{
			var entityManager = World.Active.GetOrCreateManager<EntityManager>();

			CreatePlayer(entityManager, FirstPlayerId);
			CreatePlayer(entityManager, SecondPlayerId);
			CreateSpawner(entityManager);
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
				Prefab = _spawnPrefab,
				Count = 3,
				Radius = 5
			});
			entityManager.SetComponentData(spawner, new Timer{RepeatCount = int.MaxValue, Interval = 3.0f});
		}

		private void CreatePlayer(EntityManager entityManager, int id)
		{
			var playerArchetype = entityManager.CreateArchetype
			(
				typeof(Position),
				typeof(Rotation),
				typeof(Scale),
				typeof(MeshInstanceRenderer),
				typeof(PlayerInput),
				typeof(Player),
				typeof(Score)
			);

			var player = entityManager.CreateEntity(playerArchetype);
			entityManager.SetComponentData(player, new Position{Value = new float3(0.0f, -8.0f, 0.0f)});
			entityManager.SetComponentData(player, new Rotation{Value = quaternion.identity});
			entityManager.SetComponentData(player, new Scale{Value = new float3(1.0f, 1.0f, 1.0f)});
			entityManager.SetComponentData(player, new Player {Id = id});
			entityManager.SetSharedComponentData(player, new MeshInstanceRenderer
			{
				mesh = _playerMesh,
				material = _playerMaterial
			});
		}
	}
}

