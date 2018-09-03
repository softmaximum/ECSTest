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
		private const int BlocksCount = 10;
		[SerializeField] private Mesh _playerMesh;
		[SerializeField] private Material _playerMaterial;
		[SerializeField] private Mesh _blockMesh;
		[SerializeField] private Material _blockMaterial;
		[SerializeField] private GameObject _spawnPrefab;
		[SerializeField] private GameObject _bulletPrefab;
		[SerializeField] private GameObject _playerPrefab;

		public GameObject BulletPrefab
		{
			get { return _bulletPrefab; }
			set { _bulletPrefab = value; }
		}

		private void Start()
		{
			var entityManager = World.Active.GetOrCreateManager<EntityManager>();

			CreatePlayer(entityManager);
//			CreateSpawner(entityManager);
//			CreateBlocks(entityManager);
		}

		private void CreateSpawner(EntityManager entityManager)
		{
			var spawnerArchtype = entityManager.CreateArchetype
			(
				typeof(Spawner),
				typeof(Position)
			);

			var spawner = entityManager.CreateEntity(spawnerArchtype);
			entityManager.SetSharedComponentData(spawner, new Spawner
			{
				Prefab = _spawnPrefab,
				Count = 3,
				Radius = 5
			});
		}

		private void CreatePlayer(EntityManager entityManager)
		{
			var playerArchetype = entityManager.CreateArchetype
			(
				typeof(Position),
				typeof(Rotation),
				typeof(Scale),
				typeof(MeshInstanceRenderer),
				typeof(PlayerInput),
				typeof(Player)
			);

			var player = entityManager.CreateEntity(playerArchetype);
			entityManager.SetComponentData(player, new Rotation{Value = quaternion.eulerXYZ(90.0f, 0.0f,0.0f)});
			entityManager.SetComponentData(player, new Scale{Value = new float3(1.0f, 3.0f, 1.0f)});
			entityManager.SetSharedComponentData(player, new MeshInstanceRenderer
			{
				mesh = _playerMesh,
				material = _playerMaterial
			});
		}

		private void CreateBlocks(EntityManager entityManager)
		{
			var blockAcrhetype = entityManager.CreateArchetype
			(
				typeof(Rotation),
				typeof(Position),
				typeof(MeshInstanceRenderer),
				typeof(Block)
			);

			for (var i = -BlocksCount; i < BlocksCount; i++)
			{
				for (var j = -BlocksCount; j < BlocksCount; j++)
				{
					var block = entityManager.CreateEntity(blockAcrhetype);
					entityManager.SetSharedComponentData(block, new MeshInstanceRenderer
					{
						mesh = _blockMesh,
						material = _blockMaterial
					});

					entityManager.SetComponentData(block, new Position {Value = new float3(i, j, 0)});
					entityManager.SetComponentData(block, new Rotation {Value = quaternion.identity});
				}
			}
		}
	}
}

