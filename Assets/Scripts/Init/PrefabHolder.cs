using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Game.Init
{
	public class PrefabHolder : MonoBehaviour
	{
		public GameObject ExplosionPrefab;
		public GameObject[] characterPrefabs;

		public static PrefabHolder Instance => Object.FindObjectOfType<PrefabHolder>();

		public GameObject GetRandomCharacterprefab()
		{
			if (characterPrefabs.Length > 0)
			{
				var index = Random.Range(0, characterPrefabs.Length);
				return characterPrefabs[index];
			}

			return null;
		}
	}
}
